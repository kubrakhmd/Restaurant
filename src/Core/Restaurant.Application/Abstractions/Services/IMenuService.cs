
using Restaurant.Application.DTOs;
using Restaurant.Application.DTOs.Menu;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IMenuService
    {

        Task<IEnumerable<MenuItem>> GetAllMenuItem(int page, int take);
        Task<MenuItem> GetMenuItemById(int id);

        Task<MenuItem> GetMenuItemByName(string name);
        Task CreateMenuItem(MenuItem menuItemDto);
        Task UpdateMenuItem(int id, MenuItem menuItemDto);
        Task  DeleteMenuItem(int id);
    }
}
