using P013KatmanliBlog.Core.Entities;
using System.Linq.Expressions;

namespace P013KatmanliBlog.Data.Abstract
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetPostByIncludeAsync(int id);
        Task<List<Post>> GetPostsByIncludeAsync();
        Task<List<Post>> GetPostsByIncludeAsync(Expression<Func<Post, bool>>expression);


    }
}
