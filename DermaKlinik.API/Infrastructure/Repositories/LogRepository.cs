using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Interfaces;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        private readonly ApplicationDbContext _context;

        public LogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Log>> GetLogsByLevelAsync(string level)
        {
            return await _context.Logs
                .Where(l => l.Level == level)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Logs
                .Where(l => l.Timestamp >= startDate && l.Timestamp <= endDate)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetLogsByUserIdAsync(string userId)
        {
            return await _context.Logs
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetErrorLogsAsync()
        {
            return await _context.Logs
                .Where(l => l.Level == "Error" || l.Level == "Critical")
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetLogsBySourceAsync(string source)
        {
            return await _context.Logs
                .Where(l => l.Source == source)
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }

        public async Task ClearLogsAsync(DateTime beforeDate)
        {
            var logsToDelete = await _context.Logs
                .Where(l => l.Timestamp < beforeDate)
                .ToListAsync();

            _context.Logs.RemoveRange(logsToDelete);
            await _context.SaveChangesAsync();
        }
    }
} 