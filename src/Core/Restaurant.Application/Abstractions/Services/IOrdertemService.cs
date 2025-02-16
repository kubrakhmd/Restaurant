

namespace Restaurant.Application.Abstractions.Services
{
    public interface IOrderItemService
    {
        Task  GetAllOrderItems();
        Task GetOrderItemById(int id);
        Task GetOrderItemsByOrderId(int orderId);
    }
}
