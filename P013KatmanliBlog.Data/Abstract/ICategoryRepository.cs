using P013KatmanliBlog.Core.Entities;

namespace P013KatmanliBlog.Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetCategoryByIncludeAsync(int id);
    }
}
