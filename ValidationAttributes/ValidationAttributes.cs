using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace  ViralLinks.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize * 1024000;
        }
        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
            if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is { _maxFileSize / 1024000 } mb.";
        }
    }

    public class AllowedExtensionsAttribute:ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }
    
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
        
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            var extensions = "";
            var count = _extensions.Count();
            for(var x = 0; x < count; x ++)
            {
                 if(x == count - 1)
                    extensions += _extensions[x] + " ";
                else
                    extensions += $"{_extensions[x]}, ";
            }
            return $"Allowed extensions {extensions}";
        }
    }

}