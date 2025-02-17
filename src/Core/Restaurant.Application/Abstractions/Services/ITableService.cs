

using Restaurant.Application.DTOs.TableDto;

namespace Restaurant.Application.Interfaces
{
    public interface ITableService
    {
        Task GetAllTables();
        Task GetTableById(int id);
        Task CreateTable(TableDto tableDto);
        Task UpdateTable(int id, TableDto tableDto);
        Task DeleteTable(int id);
    }
}
