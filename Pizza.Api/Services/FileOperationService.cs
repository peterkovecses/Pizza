using Pizza.Api.Helpers;
using Pizza.Api.Interfaces;
using Pizza.Bll.Dtos;
using Pizza.Bll.Helpers;

namespace Pizza.Api.Services
{
    public class FileOperationService : IFileOperationService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly long _maxFileSize;
        private readonly List<string> _allowedExtensions;

        public FileOperationService(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _maxFileSize = configuration.GetValue<long>("MaxFileSize");
            _allowedExtensions = configuration.GetSection("AllowedExtensions").Get<List<string>>();
        }

        public async Task<(string, FileErrorType?)> SaveProductPhotoAsync(ProductDto product)
        {
            var fileName = product.Photo.FileName;

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !_allowedExtensions.Contains(ext))
            {
                return (null, FileErrorType.NotAllowedExtension);
            }

            if (product.Photo.Length > _maxFileSize)
            {
                return (null, FileErrorType.Size);
            }

            var photoPath = $"ProductImages/{product.Name.ToLower()}{DateTime.Now}".RemoveStrings(new string[] { " ", ".", "-", ":" }).RemoveAccents();
            photoPath = $"{photoPath}{ext}";

            var filePath = Path.Combine(_environment.WebRootPath, photoPath);

            using (var stream = System.IO.File.Create(filePath))
            {
                await product.Photo.CopyToAsync(stream);
            }

            return (photoPath, null);
        }
    }
}
