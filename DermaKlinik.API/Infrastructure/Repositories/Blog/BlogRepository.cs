using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Core.Models;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<Blog?> GetByIdAsync(Guid id)
        {
            return await _context.Blog
                .Include(b => b.Category)
                .Include(b => b.Translations)
                .ThenInclude(t => t.Language)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Blog>> GetAllAsync(PagingRequestModel request, Guid? categoryId = null, Guid? languageId = null)
        {
            var query = _context.Blog
                .Include(b => b.Category)
                    .ThenInclude(b => b.Translations)
                .Include(b => b.Translations)
                .ThenInclude(t => t.Language)
                .Where(b => b.IsActive);

            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }

            if (languageId.HasValue)
            {
                query = query.Where(b => b.Translations.Any(t => t.LanguageId == languageId.Value));
            }

            if (request.Page > 0 && request.Take > 0)
            {
                query = query.Skip((request.Page - 1) * request.Take).Take(request.Take);
            }

            return await query.ToListAsync();
        }
    }
}