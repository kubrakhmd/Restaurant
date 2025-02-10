
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Abstractions.Services;
using Restaurant.Application.DTOs;


namespace  Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly IAuthorService _service;
        private readonly IValidator<CreateAuthorDto> _validator;
        public AuthorsController(IAuthorService service, IValidator<CreateAuthorDto> validator)
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
              GetAuthorDto authorDto = await _service.GetByIdAsync(id);
                if (authorDto == null) return NotFound();

                return Ok(authorDto);
            }
            [HttpPost]
            public async Task<IActionResult> Create([FromForm] CreateAuthorDto authorDto)
            {
                await _service.CreateAsync(authorDto);
                return Created();
            }


            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromForm] UpdateAuthorDto authorDto)
            {
                if (id < 1) return BadRequest();
                await _service.UpdateAsync(id, authorDto);
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

