using P013KatmanliBlog.Core.Entities;
using P013KatmanliBlog.Data.Abstract;

namespace P013KatmanliBlog.Service.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {

    }
}
