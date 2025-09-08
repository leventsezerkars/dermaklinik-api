namespace DermaKlinik.API.Core.Models
{
    public class ApiKeySettings
    {
        public string SecretKey { get; set; } = null!;
        public string HeaderName { get; set; } = "X-API-Key";
        public bool RequireHttps { get; set; } = false;
    }
}
