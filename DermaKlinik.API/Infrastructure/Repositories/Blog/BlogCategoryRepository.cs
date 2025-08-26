using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<BlogCategory?> GetByIdAsync(Guid id)
        {
            return await _context.BlogCategory
                .Include(bc => bc.Translations)
                .ThenInclude(t => t.Language)
                .FirstOrDefaultAsync(bc => bc.Id == id);
        }

        public async Task<List<BlogCategory>> GetAllAsync(PagingRequestModel request)
        {
            var query = _context.BlogCategory
                .Include(bc => bc.Translations)
                .ThenInclude(t => t.Language)
                .Where(bc => bc.IsActive);

            if (request.Page > 0 && request.Take > 0)
            {
                query = query.Skip((request.Page - 1) * request.Take).Take(request.Take);
            }

            return await query.ToListAsync();
        }
    }
}