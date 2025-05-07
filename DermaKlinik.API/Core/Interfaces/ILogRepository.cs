using DermaKlinik.API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DermaKlinik.API.Core.Interfaces
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task<IEnumerable<Log>> GetLogsByLevelAsync(string level);
        Task<IEnumerable<Log>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Log>> GetLogsByUserIdAsync(string userId);
        Task<IEnumerable<Log>> GetErrorLogsAsync();
        Task<IEnumerable<Log>> GetLogsBySourceAsync(string source);
        Task ClearLogsAsync(DateTime beforeDate);
    }
} 