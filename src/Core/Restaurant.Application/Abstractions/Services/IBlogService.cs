using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.DTOs;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogItemDto>> GetAllAsync(int page, int take);

        Task<GetBlogDto> GetByIdAsync(int id);

        Task CreateAsync(CreateBlogDto blogDto);

        Task UpdateAsync(int id, UpdateBlogDto blogDto);

        Task DeleteAsync(int id);
    }
}
