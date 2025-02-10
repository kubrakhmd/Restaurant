using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Application.DTOs;

namespace Restaurant.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagItemDto>> GetAllAsync(int page, int take);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto tagDto);
        Task UpdateAsync(int id, UpdateTagDto tagDto);
        Task DeleteAsync(int id);
    }
}
