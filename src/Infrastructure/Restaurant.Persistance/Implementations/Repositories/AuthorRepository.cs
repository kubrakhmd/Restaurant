using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.Abstractions.Repositories;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;
using Restaurant.Persistence.Implementations.Repostories.Generic;

namespace Restaurant.Persistence.Implementations.Repositories
{
   
        internal class AuthorRepository : Repository<Author>, IAuthorRepository
        {
            public AuthorRepository(AppDbContext context) : base(context) { }

        }
    
}
