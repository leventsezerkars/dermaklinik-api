using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public FileUploadService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UploadImageAsync(IFormFile file, string folderName = "gallery")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya boş olamaz");

            if (!IsValidImageFile(file))
                throw new ArgumentException("Geçersiz dosya formatı");

            if (file.Length > MaxFileSize)
                throw new ArgumentException($"Dosya boyutu {MaxFileSize / (1024 * 1024)}MB'dan büyük olamaz");

            // Klasör yolu oluştur
            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot", "uploads", folderName);
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Benzersiz dosya adı oluştur
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Dosyayı kaydet
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

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
