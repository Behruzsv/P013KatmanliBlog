using Microsoft.EntityFrameworkCore;
using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;
using System.Linq.Expressions;

namespace P013KatmanliBlog.Data.Concrete
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Post> GetPostByIncludeAsync(int id)
        {
            return await _context.Posts.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetPostsByIncludeAsync()
        {
            return await _context.Posts.Include(p => p.Category).ToListAsync();
        }

        public async Task<List<Post>> GetPostsByIncludeAsync(Expression<Func<Post, bool>> expression)
        {
            return await _context.Posts.Where(expression).Include(p => p.Category).ToListAsync();
        }
    }
}
