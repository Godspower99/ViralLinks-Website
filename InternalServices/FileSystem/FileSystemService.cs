using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ViralLinks.Data;

namespace ViralLinks.InternalServices
{
    public class FileSystemService 
    {
        private AzureStorageConfigOptions AzureStorageConfig;
        private ApplicationDbContext dbContext;

        public FileSystemService(IOptions<AzureStorageConfigOptions> azureStorageConfig,
            ApplicationDbContext dbContext)
        {
            this.AzureStorageConfig = azureStorageConfig?.Value ?? 
                throw new ArgumentNullException(nameof(azureStorageConfig));    
            this.dbContext = dbContext ?? 
                throw new ArgumentNullException(nameof(dbContext));
        }

        public bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsVideo(IFormFile file)
        {
            if(file.ContentType.Contains("video"))
                return true;
            string[] formats = new string[] { ".mp4", ".mov", ".mp3"};
            return formats.Any(item => file.FileName.EndsWith(item,StringComparison.OrdinalIgnoreCase));
        }

        public async Task<string> UploadBlob(Stream stream, string containerName, string fileName)
        {
            Uri blobUri = new Uri("https://" +
                                  AzureStorageConfig.AccountName +
                                  ".blob.core.windows.net/" +
                                  containerName +
                                  "/" + fileName);
            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(AzureStorageConfig.AccountName, AzureStorageConfig.AccountKey);
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);
            stream.Position = 0;
            await blobClient.UploadAsync(stream,true);
            return blobUri.AbsoluteUri;
        }

        public async Task<string> GetProfilePictureAsync(string userId)
        {
            var profilePicture = await dbContext.ProfilePictures.FirstOrDefaultAsync(p => p.UserID == userId);
            if(profilePicture != null)
            {
                // GET FROM AZURE BLOB STORAGE
                return $"https://{AzureStorageConfig.AccountName}.blob.core.windows.net/{AzureStorageConfig.ProfilePicturesContainer}/{profilePicture.UserID}{profilePicture.Extension}";
            }
            return "https://localhost:5001/avatar.png";        
        }

        public async Task UpdateProfilePicture(ApplicationUser user, IFormFile pictureFile)
        {
            var fileExtension = Path.GetExtension(pictureFile.FileName);
            using var stream = new MemoryStream();
            await pictureFile.CopyToAsync(stream);
            await this.UploadBlob(stream, AzureStorageConfig.ProfilePicturesContainer,user.Id);
            var profilePicture = await dbContext.ProfilePictures.FirstOrDefaultAsync(p => p.UserID == user.Id);
            if(profilePicture != null)
            {
                profilePicture.Extension = fileExtension;
                dbContext.ProfilePictures.Update(profilePicture);
            }
            else
            {
                await dbContext.ProfilePictures.AddAsync(new ProfilePicture{
                    UserID = user.Id,
                    Extension = fileExtension,
                });
            }
            await dbContext.SaveChangesAsync();
        }

    }
}