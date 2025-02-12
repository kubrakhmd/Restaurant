using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Models;

namespace Restaurant.Application.Abstractions.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
    }
}
