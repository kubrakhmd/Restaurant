
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Implementations.Repostories.Generic;

namespace Restaurant.Persistence.Implementations.Repositories
{
    internal class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(AppDbContext context) : base(context) { }
        public async Task<IEnumerable<T>> GetManyToManyEntities<T>(ICollection<int> ids) where T : BaseEntity
        {
            return await _context.Set<T>()
           .Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
