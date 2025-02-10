
using Restaurant.Application.DTOs;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorItemDto>> GetAllAsync(int page, int take);

        Task<GetAuthorDto> GetByIdAsync(int id);

        Task CreateAsync(CreateAuthorDto authorDto);

        Task UpdateAsync(int id, UpdateAuthorDto authorDto);

        Task DeleteAsync(int id);
    }
}