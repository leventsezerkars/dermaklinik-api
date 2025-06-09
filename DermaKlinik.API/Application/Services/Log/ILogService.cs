namespace DermaKlinik.API.Application.Services
{
    public interface ILogService
    {
        Task LogInformationAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null);
        Task LogWarningAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null);
        Task LogErrorAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null);
        Task LogCriticalAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null);
    }
}