using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Application.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName = "gallery");
        Task<string> UploadImageWithResizeAsync(IFormFile file, string folderName = "gallery", int maxWidth = 1920, int maxHeight = 1080, int quality = 85, int maxSizeInKB = 1000);
        Task<bool> DeleteImageAsync(string filePath);
        Task<string> GetImageUrlAsync(string filePath);
        bool IsValidImageFile(IFormFile file);
        long GetMaxFileSize();
        string[] GetAllowedExtensions();
    }
}
