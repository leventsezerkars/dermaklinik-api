using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing.Processors.Quantization;

namespace DermaKlinik.API.Application.Services
{
    public class ImageResizeService : IImageResizeService
    {
        public async Task<byte[]> ResizeImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            
            return await ResizeImageAsync(memoryStream.ToArray(), maxWidth, maxHeight, quality);
        }

        public async Task<byte[]> ResizeImageAsync(byte[] imageBytes, int maxWidth = 1920, int maxHeight = 1080, int quality = 85)
        {
            using var image = Image.Load(imageBytes);
            using var outputStream = new MemoryStream();

            // Resmi yeniden boyutlandır
            var resizeOptions = new ResizeOptions
            {
                Size = new Size(maxWidth, maxHeight),
                Mode = ResizeMode.Max
            };

            image.Mutate(x => x.Resize(resizeOptions));

            // Kalite ayarları ile kaydet
            var encoder = GetEncoder(image.Metadata.DecodedImageFormat, quality);
            await image.SaveAsync(outputStream, encoder);

            return outputStream.ToArray();
        }

        public async Task<byte[]> ResizeImageAggressivelyAsync(byte[] imageBytes, int maxWidth = 1920, int maxHeight = 1080, int quality = 85)
        {
            using var image = Image.Load(imageBytes);
            using var outputStream = new MemoryStream();

            // Resmi yeniden boyutlandır
            var resizeOptions = new ResizeOptions
            {
                Size = new Size(maxWidth, maxHeight),
                Mode = ResizeMode.Max
            };

            image.Mutate(x => x.Resize(resizeOptions));

            // Agresif sıkıştırma için özel encoder
            var encoder = GetAggressiveEncoder(image.Metadata.DecodedImageFormat, quality);
            await image.SaveAsync(outputStream, encoder);

            return outputStream.ToArray();
        }

        public bool IsImageTooLarge(byte[] imageBytes, int maxSizeInKB = 1000)
        {
            var sizeInKB = imageBytes.Length / 1024.0;
            return sizeInKB > maxSizeInKB;
        }

        public async Task<ImageResizeResult> ProcessImageAsync(IFormFile file, int maxWidth = 1920, int maxHeight = 1080, int quality = 85, int maxSizeInKB = 1000)
        {
            var result = new ImageResizeResult
            {
                OriginalSize = file.Length
            };

            try
            {
                // Önce resmi yükle
                using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var originalBytes = memoryStream.ToArray();

                // İlk resize işlemi
                var resizedBytes = await ResizeImageAsync(originalBytes, maxWidth, maxHeight, quality);
                var currentSizeInKB = resizedBytes.Length / 1024.0;
                
                // Eğer hala çok büyükse, daha agresif sıkıştırma yap
                if (currentSizeInKB > maxSizeInKB)
                {
                    // Kaliteyi düşür
                    var aggressiveQuality = Math.Max(30, quality - 30);
                    resizedBytes = await ResizeImageAggressivelyAsync(originalBytes, maxWidth, maxHeight, aggressiveQuality);
                    currentSizeInKB = resizedBytes.Length / 1024.0;
                    
                    // Hala büyükse, boyutu daha da küçült
                    if (currentSizeInKB > maxSizeInKB)
                    {
                        var scaleFactor = Math.Sqrt(maxSizeInKB / currentSizeInKB);
                        var newWidth = (int)(maxWidth * scaleFactor);
                        var newHeight = (int)(maxHeight * scaleFactor);
                        
                        resizedBytes = await ResizeImageAggressivelyAsync(originalBytes, newWidth, newHeight, aggressiveQuality);
                        currentSizeInKB = resizedBytes.Length / 1024.0;
                        
                        // Son çare: çok düşük kalite
                        if (currentSizeInKB > maxSizeInKB)
                        {
                            resizedBytes = await ResizeImageAggressivelyAsync(originalBytes, newWidth, newHeight, 20);
                            currentSizeInKB = resizedBytes.Length / 1024.0;
                            
                            // Hala büyükse hata ver
                            if (currentSizeInKB > maxSizeInKB)
                            {
                                result.Message = "Resim webde gösterilemeyecek kadar büyük boyutta. Lütfen daha küçük bir resim seçin.";
                                result.ImageBytes = originalBytes;
                                return result;
                            }
                        }
                    }
                }

                result.ImageBytes = resizedBytes;
                result.FinalSize = resizedBytes.Length;
                result.WasResized = resizedBytes.Length != originalBytes.Length;
                result.WasCompressed = true;
                result.Message = $"Resim başarıyla işlendi. Boyut: {originalBytes.Length / 1024:F1}KB → {resizedBytes.Length / 1024:F1}KB";

                return result;
            }
            catch (Exception ex)
            {
                result.Message = $"Resim işlenirken hata oluştu: {ex.Message}";
                result.ImageBytes = null;
                return result;
            }
        }

        private IImageEncoder GetEncoder(IImageFormat format, int quality)
        {
            return format switch
            {
                var f when f == JpegFormat.Instance => new JpegEncoder { Quality = quality },
                var f when f == PngFormat.Instance => new PngEncoder 
                { 
                    CompressionLevel = PngCompressionLevel.BestCompression,
                    Quantizer = new WebSafePaletteQuantizer()
                },
                var f when f == WebpFormat.Instance => new WebpEncoder { Quality = quality },
                _ => new JpegEncoder { Quality = quality } // Varsayılan olarak JPEG
            };
        }

        private IImageEncoder GetAggressiveEncoder(IImageFormat format, int quality)
        {
            return format switch
            {
                var f when f == JpegFormat.Instance => new JpegEncoder { Quality = quality },
                var f when f == PngFormat.Instance => new PngEncoder 
                { 
                    CompressionLevel = PngCompressionLevel.BestCompression,
                    Quantizer = new WebSafePaletteQuantizer()
                },
                var f when f == WebpFormat.Instance => new WebpEncoder { Quality = quality },
                _ => new JpegEncoder { Quality = quality } // Varsayılan olarak JPEG
            };
        }
    }
}
