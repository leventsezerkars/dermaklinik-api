using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageResizeService _imageResizeService;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public FileUploadService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor, IImageResizeService imageResizeService)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
            _imageResizeService = imageResizeService;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName = "gallery")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya boş olamaz");

            if (!IsValidImageFile(file))
                throw new ArgumentException("Geçersiz dosya formatı");

            if (file.Length > MaxFileSize)
                throw new ArgumentException($"Dosya boyutu {MaxFileSize / (1024 * 1024)}MB'dan büyük olamaz");

            // Resmi işle (boyutlandır ve sıkıştır)
            var resizeResult = await _imageResizeService.ProcessImageAsync(file, maxWidth: 1920, maxHeight: 1080, quality: 85, maxSizeInKB: 1000);
            
            if (resizeResult.ImageBytes == null)
                throw new ArgumentException(resizeResult.Message);

            // Klasör yolu oluştur
            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Benzersiz dosya adı oluştur
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // İşlenmiş resmi kaydet
            await File.WriteAllBytesAsync(filePath, resizeResult.ImageBytes);

            // Web erişilebilir yol döndür
            return Path.Combine("uploads", folderName, fileName).Replace("\\", "/");
        }

        public async Task<string> UploadImageWithResizeAsync(IFormFile file, string folderName = "gallery", int maxWidth = 1920, int maxHeight = 1080, int quality = 85, int maxSizeInKB = 1000)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya boş olamaz");

            if (!IsValidImageFile(file))
                throw new ArgumentException("Geçersiz dosya formatı");

            if (file.Length > MaxFileSize)
                throw new ArgumentException($"Dosya boyutu {MaxFileSize / (1024 * 1024)}MB'dan büyük olamaz");

            // Resmi işle (boyutlandır ve sıkıştır)
            var resizeResult = await _imageResizeService.ProcessImageAsync(file, maxWidth, maxHeight, quality, maxSizeInKB);
            
            if (resizeResult.ImageBytes == null)
                throw new ArgumentException(resizeResult.Message);

            // Klasör yolu oluştur
            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Benzersiz dosya adı oluştur
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // İşlenmiş resmi kaydet
            await File.WriteAllBytesAsync(filePath, resizeResult.ImageBytes);

            // Web erişilebilir yol döndür
            return Path.Combine("uploads", folderName, fileName).Replace("\\", "/");
        }

        public async Task<bool> DeleteImageAsync(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    return false;

                var fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot", filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> GetImageUrlAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return filePath;

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/{filePath}";
        }

        public bool IsValidImageFile(IFormFile file)
        {
            if (file == null)
                return false;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }

        public long GetMaxFileSize()
        {
            return MaxFileSize;
        }

        public string[] GetAllowedExtensions()
        {
            return _allowedExtensions;
        }
    }
}
