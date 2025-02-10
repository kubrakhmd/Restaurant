
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;



namespace Restaurant.API.Controllerss
{
    [Route("[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IValidator<CreateBlogDto> _validator;
        public BlogsController(IBlogService service, IValidator<CreateBlogDto> validator)
        {
            _validator = validator;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            return Ok(await _service.GetAllAsync(page, take));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();
            GetBlogDto blogDto = await _service.GetByIdAsync(id);
            if (blogDto == null) return NotFound();
            return Ok(blogDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBlogDto blogDto)
        {
            await _service.CreateAsync(blogDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateBlogDto blogDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id,blogDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
