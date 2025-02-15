
using Restaurant.Application.DTOs.OrderDto;

namespace Restaurant.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<ShowOrder>> GetAllOrders();
        Task<IEnumerable<OrderItemDto>> GetOrderById(int id);
        Task CreateOrder(OrderDto orderDto);
        Task UpdateOrder(int id, OrderDto orderDto);
        Task UpdateOrderStatus(UpdateStatus updateStatus);
        Task DeleteOrder(int id);
    }
}
