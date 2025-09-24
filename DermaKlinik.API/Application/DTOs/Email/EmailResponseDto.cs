namespace DermaKlinik.API.Application.DTOs.Email
{
    public class EmailResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public string? ErrorDetails { get; set; }
    }
}
