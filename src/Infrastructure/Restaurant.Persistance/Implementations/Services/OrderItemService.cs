

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Implementations.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
    

        public OrderItemService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }

        public async Task  GetAllOrderItems()
        {
            try
            {
                var orderItems = await _context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Food).ToListAsync();
            
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Food).FirstOrDefaultAsync(oi => oi.Id == id);
                if (orderItem == null)
                {
                    throw new Exception("Not Found");
                }
      
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task GetOrderItemsByOrderId(int orderId)
        {
            try
            {
                var orderItems = await _context.OrderItems.Where(oi => oi.OrderId == orderId).Include(oi => oi.Food).ToListAsync();
                if (orderItems == null)
                {
                    throw new Exception("Not Found");
                }
           
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }


}
