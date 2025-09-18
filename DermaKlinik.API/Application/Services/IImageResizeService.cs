using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Application.Services
{
    public interface IImageResizeService
    {
        Task<byte[]> ResizeImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85);
        Task<byte[]> ResizeImageAsync(byte[] imageBytes, int maxWidth = 1920, int maxHeight = 1080, int quality = 85);
        bool IsImageTooLarge(byte[] imageBytes, int maxSizeInKB = 1000);
        Task<ImageResizeResult> ProcessImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85, int maxSizeInKB = 1000);
    }

    public class ImageResizeResult
    {
        public byte[] ImageBytes { get; set; }
        public bool WasResized { get; set; }
        public bool WasCompressed { get; set; }
        public string Message { get; set; }
        public long OriginalSize { get; set; }
        public long FinalSize { get; set; }
    }
}
