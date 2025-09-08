namespace DermaKlinik.API.Application.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadImageAsync(IFormFile file, string folderName = "gallery");
        Task<bool> DeleteImageAsync(string filePath);
        Task<string> GetImageUrlAsync(string filePath);
        bool IsValidImageFile(IFormFile file);
        long GetMaxFileSize();
        string[] GetAllowedExtensions();
    }
}
