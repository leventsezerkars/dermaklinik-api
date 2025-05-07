using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DermaKlinik.API.Application.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<LogService> _logger;

        public LogService(
            ILogRepository logRepository,
            IHttpContextAccessor httpContextAccessor,
            ILogger<LogService> logger)
        {
            _logRepository = logRepository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task LogInformationAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Information", message, null, source, userId, userName, additionalData);
            await _logRepository.AddAsync(log);
            _logger.LogInformation(message);
        }

        public async Task LogWarningAsync(string message, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Warning", message, null, source, userId, userName, additionalData);
            await _logRepository.AddAsync(log);
            _logger.LogWarning(message);
        }

        public async Task LogErrorAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Error", message, ex, source, userId, userName, additionalData);
            await _logRepository.AddAsync(log);
            _logger.LogError(ex, message);
        }

        public async Task LogCriticalAsync(string message, Exception ex, string source, string? userId = null, string? userName = null, string? additionalData = null)
        {
            var log = CreateLog("Critical", message, ex, source, userId, userName, additionalData);
            await _logRepository.AddAsync(log);
            _logger.LogCritical(ex, message);
        }

        public async Task<IEnumerable<Log>> GetLogsByLevelAsync(string level)
        {
            return await _logRepository.GetLogsByLevelAsync(level);
        }

        public async Task<IEnumerable<Log>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _logRepository.GetLogsByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Log>> GetLogsByUserIdAsync(string userId)
        {
            return await _logRepository.GetLogsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Log>> GetErrorLogsAsync()
        {
            return await _logRepository.GetErrorLogsAsync();
        }

        public async Task<IEnumerable<Log>> GetLogsBySourceAsync(string source)
        {
            return await _logRepository.GetLogsBySourceAsync(source);
        }

        public async Task ClearLogsAsync(DateTime beforeDate)
        {
            await _logRepository.ClearLogsAsync(beforeDate);
        }

        private Log CreateLog(string level, string message, Exception? ex, string source, string? userId, string? userName, string? additionalData)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var request = httpContext?.Request;

            return new Log
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