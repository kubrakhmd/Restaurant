
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs.TableDto;
using Restaurant.Application.Interfaces;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTables()
        {
       await _tableService.GetAllTables();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTableById(int id)
        {
            await _tableService.GetTableById(id);
            return Ok();
        }
        [HttpPost("Create")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateTable([FromBody] TableDto tableDto)
        {
           await _tableService.CreateTable(tableDto);
            return Ok();
        }
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateTable(int id, [FromBody] TableDto tableDto)
        {
          await _tableService.UpdateTable(id, tableDto);
            return NoContent();
        }
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteTable(int id)
        {
          await _tableService.DeleteTable(id);
            return NoContent();
        }
    }
}
