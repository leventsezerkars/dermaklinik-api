using DermaKlinik.API.Core.Entities;
using DermaKlinik.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DermaKlinik.API.Infrastructure.Repositories
{
    public class BlogTranslationRepository(ApplicationDbContext context) : GenericRepository<BlogTranslation>(context), IBlogTranslationRepository
    {
        public new async Task<BlogTranslation?> GetByIdAsync(Guid id)
        {
            return await _context.BlogTranslation
                .Include(bt => bt.Language)
                .FirstOrDefaultAsync(bt => bt.Id == id);
        }
    }
}