using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;

namespace P013KatmanliBlog.Data.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryByIncludeAsync(int id)
        {
            return await _dbSet.Where(c => c.Id == id).AsNoTracking().Include(c => c.Posts).FirstOrDefaultAsync();
        }
    }
}
