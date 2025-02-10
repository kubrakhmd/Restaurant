using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Repositories
{
   public interface IBlogRepository:IRepository<Blog>
    {
        Task<IEnumerable<T>> GetManyToManyEntities<T>
          (ICollection<int> ids) where T : BaseEntity;
    }
}
