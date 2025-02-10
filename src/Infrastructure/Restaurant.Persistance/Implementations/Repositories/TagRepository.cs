
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Implementations.Repostories.Generic;

namespace Restaurant.Persistence.Implementations.Repositories
{
    internal class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext context) : base(context) { }
    }
}
