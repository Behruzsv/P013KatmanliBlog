using P013KatmanliBlog.Data;
using P013KatmanliBlog.Data.Concrete;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.Service.Concrete
{
    public class CategoryService : CategoryRepository, ICategoryService
    {
        public CategoryService(DatabaseContext context) : base(context)
        {
        }
    }
}