using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        
            private readonly IOrderItemService _orderItemService;

            public OrderItemsController(IOrderItemService orderItemService)
            {
                _orderItemService = orderItemService;
            }

            // GET: api/OrderItem
            [HttpGet]
            public async Task<ActionResult> GetAllOrderItems()
            {
            await _orderItemService.GetAllOrderItems();
            return Ok();
            }

            // GET: api/OrderItem/{id}
            [HttpGet("{id}")]
            public async Task<ActionResult> GetOrderItemById(int id)
            {
                 await _orderItemService.GetOrderItemById(id);
            return Ok();
        }

            // GET: api/OrderItem/order/{orderId}
            [HttpGet("order/{orderId}")]
            public async Task<ActionResult> GetOrderItemsByOrderId(int orderId)
            {
               await _orderItemService.GetOrderItemsByOrderId(orderId);
            return Ok();
        }

        }
    }
