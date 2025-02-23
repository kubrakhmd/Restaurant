using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs.OrderDto;
using Restaurant.Domain.Enums;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.Persistence.Implementations.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
      

        public OrderService(AppDbContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
            
        }
        public async Task <IEnumerable<ShowOrder>> GetAllOrders()
        {
           
          
              var orders = await _context.Orders.Include(o => o.OrderItems).Include(o => o.AppUser).ToListAsync();
                if (orders == null)
                {
                    throw new Exception("Not Found");
                }

                return   _mapper.Map< IEnumerable < ShowOrder >>(orders);

         
        }
        public async Task<IEnumerable<OrderItemDto>> GetOrderById(int id)
        {
            
         
                var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
                if (order == null)
                
                    throw new Exception("Not Found");
                
                return _mapper.Map<IEnumerable<OrderItemDto>>(order);
            


       }
        public async Task CreateOrder(OrderDto orderDto)
        {
            if (orderDto == null)
            {
              throw new Exception("Order data is not found");
            }
            try
            {
                var userExists = await _context.Users.AnyAsync(u=>u.Id==orderDto.UserId);
                if (!userExists)
                {
                  throw new Exception("AppUser is not found");
                }

                var order = _mapper.Map<Order>(orderDto);
                order.OrderItems = new List<OrderItem>();
                order.TotalAmount = 0;

                foreach (var itemDto in orderDto.OrderItems)
                {
                    var menuItem = await _context.Foods.FindAsync(itemDto.FoodId);
                    if (menuItem == null)
                    {
                      throw new Exception("Menu is not found");
                    }

                    var orderItem = _mapper.Map<OrderItem>(itemDto);
                    orderItem.UnitPrice = menuItem.Price;

                    order.OrderItems.Add(orderItem);
                    order.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;
                }

                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
             
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
        public async Task UpdateOrder(int orderid, OrderDto orderDto)
        {
            if (orderDto == null)
            {
               throw new Exception("Order data is not found");
            }
            try
            {
                var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderid);
                if (order == null)
                {
                 throw new Exception("Order is not found");
                }
                //if (order.AppUserId != orderDto.UserId)
                //{
                //    throw new Exception ("user not found");
                //}
                if (order.Status == OrderStatus.InProgress || order.Status == OrderStatus.Completed)
                {
                  throw new Exception("");
                }
                _context.OrderItems.RemoveRange(order.OrderItems);
                _mapper.Map(orderDto, order);
                order.TotalAmount = 0;


                foreach (var itemDto in orderDto.OrderItems)
                {
                    var menuItem = await _context.Foods.FindAsync(itemDto.FoodId);
                    if (menuItem == null)
                    {
                      throw new Exception("Menu is not found"); 
                    }
                    var orderItem = _mapper.Map<OrderItem>(itemDto);
                    orderItem.UnitPrice = menuItem.Price;

                    order.OrderItems.Add(orderItem);
                    order.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;
                }

                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

              
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
        public async Task UpdateOrderStatus(UpdateStatus updateStatus)
        {
            try
            {
                var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == updateStatus.Id);
                if (order == null)
                {
               throw new Exception ("Order is not found");
                }

                order.Status = updateStatus.Status;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();

           
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
        public async Task DeleteOrder(int id)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order == null)
                {
                   throw new Exception("Order is not found");
                }

                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();

              
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
