
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs.OrderDto;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrderById(int id)
        {
            return Ok( await _orderService.GetOrderById(id));
        }

        // POST: api/Order/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            await _orderService.CreateOrder(orderDto);
               return Created() ;
        }

        // PUT: api/Order/update/{id}
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            await _orderService.UpdateOrder(id, orderDto);
            return NoContent();
        }

        // PUT: api/Order/update-status
        [HttpPut("update-status")]
        public async Task<ActionResult> UpdateOrderStatus([FromBody] UpdateStatus updateStatus)
        {
             await _orderService.UpdateOrderStatus(updateStatus);
            return NoContent();
        }

        // DELETE: api/Order/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
             await _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
