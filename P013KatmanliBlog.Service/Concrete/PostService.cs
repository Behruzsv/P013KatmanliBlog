using P013KatmanliBlog.Data;
using P013KatmanliBlog.Data.Concrete;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.Service.Concrete
{
    public class PostService : PostRepository, IPostService
    {
        public PostService(DatabaseContext context) : base(context)
        {
        }
    }
}
