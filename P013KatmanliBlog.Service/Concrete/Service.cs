using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data;
using P013KatmanliBlog.Data.Concrete;
using P013KatmanliBlog.Service.Abstract;

namespace P013KatmanliBlog.Service.Concrete
{
	public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {
        }
    }
}
