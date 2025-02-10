

using Restaurant.Application.DTOs;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreItemDto>> GetAllAsync(int page, int take);

        Task<GetGenreDto> GetByIdAsync(int id);

        Task CreateAsync(CreateGenreDto genreDto);

        Task UpdateAsync(int id, UpdateGenreDto genroDto);

        Task DeleteAsync(int id);
    }
}
