using System.Net;
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

        private async Task<string> UploadBlob(Stream stream, string containerName, string fileName)
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

        public string GetProfilePictureAsync(string userId)
        {
            // var metaData = await dbContext.FileMetaDatas.FirstOrDefaultAsync(f => f.Id == userId);
            // GET FROM AZURE BLOB STORAGE
            return $"https://{AzureStorageConfig.AccountName}.blob.core.windows.net/{AzureStorageConfig.ProfilePicturesContainer}/{userId}";      
        }

        public string GetPostImageAsync(string postid)
        {
            // GET FROM AZURE BLOB STORAGE
            return $"https://{AzureStorageConfig.AccountName}.blob.core.windows.net/{AzureStorageConfig.PostImagesContainer}/{postid}";
        }

        public async Task UpdateProfilePicture(ApplicationUser user, IFormFile pictureFile)
        {
            var fileExtension = Path.GetExtension(pictureFile.FileName);
            var fileName = Path.GetFileName(pictureFile.Name);
            using var stream = new MemoryStream();
            await pictureFile.CopyToAsync(stream);
            var uri = await this.UploadBlob(stream, AzureStorageConfig.ProfilePicturesContainer,user.Id);
            var metaData = await dbContext.FileMetaDatas.FirstOrDefaultAsync(fm => fm.Id == user.Id);
            if(metaData != null)
            {
                metaData.Extension = fileExtension;
                metaData.Name = fileName;
                dbContext.FileMetaDatas.Update(metaData);
            }
            else
            {
                await dbContext.FileMetaDatas.AddAsync(new FileMetaData {
                    Id = user.Id,
                    Extension = fileExtension,
                    LastUpdate = DateTime.Now,
                    URI = uri,
                    Name = fileName
                });
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task UploadPostImage(CreatePostModel postModel, Post post)
        {
            if(postModel.Image != null)
            {
                var fileExtension = Path.GetExtension(postModel.Image.FileName);
                var fileName = Path.GetFileName(postModel.Image.Name);
                using var stream = new MemoryStream();
                await postModel.Image.CopyToAsync(stream);
                var uri = await this.UploadBlob(stream,AzureStorageConfig.PostImagesContainer,post.PostId);
               await dbContext.AddAsync(new FileMetaData{
                   Id = post.PostId,
                   Extension = fileExtension,
                   LastUpdate = DateTime.Now,
                   URI = uri,
                   Name = fileName
               });
                await dbContext.SaveChangesAsync();
            }
            // upload default image
            else{
                using var fileStream = new FileStream("./wwwroot/virallinks-text.png",FileMode.Open);
                var fileName = Path.GetFileName(fileStream.Name);
                var fileExtension = Path.GetExtension(fileStream.Name);
                var uri = await this.UploadBlob(fileStream,AzureStorageConfig.PostImagesContainer,post.PostId);
                await dbContext.AddAsync(new FileMetaData{
                    Id = post.PostId,
                    Extension = fileExtension,
                    LastUpdate = DateTime.Now,
                    URI = uri,
                    Name = fileName
                });
            }
        }

        public async Task UploadPostImage(EditPostModel postModel, Post post)
        {
            if(postModel.Image != null)
            {
                var fileExtension = Path.GetExtension(postModel.Image.FileName);
                var fileName = Path.GetFileName(postModel.Image.Name);
                using var stream = new MemoryStream();
                await postModel.Image.CopyToAsync(stream);
                var uri = await this.UploadBlob(stream,AzureStorageConfig.PostImagesContainer,post.PostId);
               dbContext.Update(new FileMetaData{
                   Id = post.PostId,
                   Extension = fileExtension,
                   LastUpdate = DateTime.Now,
                   URI = uri,
                   Name = fileName
               });
                await dbContext.SaveChangesAsync();
            }
        }


        public async Task UpdateProfilePicture(ApplicationUser user, Stream fileStream, string name, string extension)
        {
            var uri = await this.UploadBlob(fileStream, AzureStorageConfig.ProfilePicturesContainer,user.Id);
            var metaData = await dbContext.FileMetaDatas.FirstOrDefaultAsync(fm => fm.Id == user.Id);
            if(metaData != null)
            {
                metaData.Extension = extension;
                metaData.Name = name;
                dbContext.FileMetaDatas.Update(metaData);
            }
            else
            {
                await dbContext.FileMetaDatas.AddAsync(new FileMetaData {
                    Id = user.Id,
                    Extension = extension,
                    LastUpdate = DateTime.Now,
                    URI = uri,
                    Name = name
                });
            }
            await dbContext.SaveChangesAsync();
        }
    }
}