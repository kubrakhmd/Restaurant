
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;


namespace Restaurant.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;
        private readonly IValidator<CreateGenreDto> _validator;
        public GenresController(IGenreService service, IValidator<CreateGenreDto> validator)
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
            GetGenreDto genreDto = await _service.GetByIdAsync(id);
            if (genreDto == null) return NotFound();
            return Ok(genreDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGenreDto genreDto)
        {
            await _service.CreateAsync(genreDto);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateGenreDto genreDto)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id, genreDto);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
