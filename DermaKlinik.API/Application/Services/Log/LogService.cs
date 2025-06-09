using DermaKlinik.API.Infrastructure.Repositories;

namespace DermaKlinik.API.Application.Services
{
    public class LogService(
        ILogRepository logRepository,
        IHttpContextAccessor httpContextAccessor,
        ILogger<LogService> logger) : ILogService
    {
        public async Task LogInformationAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Information", message, null, source, userId, userName, additionalData);
            await logRepository.AddAsync(log);
            logger.LogInformation(message);
        }

        public async Task LogWarningAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Warning", message, null, source, userId, userName, additionalData);
            await logRepository.AddAsync(log);
            logger.LogWarning(message);
        }

        public async Task LogErrorAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Error", message, ex, source, userId, userName, additionalData);
            await logRepository.AddAsync(log);
            logger.LogError(ex, message);
        }

        public async Task LogCriticalAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Critical", message, ex, source, userId, userName, additionalData);
            await logRepository.AddAsync(log);
            logger.LogCritical(ex, message);
        }

        private Core.Entities.Log CreateLog(string level, string message, Exception? ex, string source, string? userId, string? userName, string? additionalData)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var request = httpContext?.Request;

            return new Core.Entities.Log
            {
                Level = level,
                Message = message,
                Exception = ex?.Message,
                StackTrace = ex?.StackTrace,
                Source = source,
                UserId = userId,
                UserName = userName,
                RequestPath = request?.Path.Value,
                RequestMethod = request?.Method,
                RequestBody = request?.Body.ToString(),
                StatusCode = httpContext?.Response?.StatusCode,
                IpAddress = request?.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                Timestamp = DateTime.UtcNow,
                AdditionalData = additionalData
            };
        }
    }
}