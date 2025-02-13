

using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Implementations.Repostories.Generic;

namespace Restaurant.Persistence.Implementations.Repositories
{
    internal class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext context) : base(context) { }
    }
}
