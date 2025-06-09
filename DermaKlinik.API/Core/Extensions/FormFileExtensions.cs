using Microsoft.AspNetCore.Http;

namespace DermaKlinik.API.Core.Extensions
{
    public static class FormFileExtensions
    {
        public static void Save(this IFormFile file, string path)
        {
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}
