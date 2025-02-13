
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs.Menu;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<IActionResult> GetMenuItems(int page,int take)
        {
            return Ok(await _menuService.GetAllMenuItem(page, take));
        }

        // GET: api/MenuItems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItem(int id)
        {
            return Ok (await _menuService.GetMenuItemById(id));
        }

        // GET: api/MenuItems/name/{name}
        [HttpGet("name/{name}")]
        public async Task<IActionResult> SearchMenuItemByName(string name)
        {
            return Ok (await _menuService.GetMenuItemByName(name));
        }

        // POST: api/MenuItems
        [HttpPost("create")]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItem menuItemDto)
        {
            await _menuService.CreateMenuItem(menuItemDto);
            return Created() ;
        }

        // PUT: api/MenuItems/update/{id}
        [HttpPut("update/{id}")]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateMenuItem(int id, [FromBody] MenuItem menuItemDto)
        {
            if (id < 1) return BadRequest();
            await _menuService.UpdateMenuItem(id, menuItemDto);
            return NoContent();
        }

        // DELETE: api/MenuItems/delete/{id}
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuService.DeleteMenuItem(id);
            return NoContent();
        }
    }
}

