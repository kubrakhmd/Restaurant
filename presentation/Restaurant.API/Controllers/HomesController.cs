using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models;
using Restaurant.Persistence.Context;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantHome>>> GetRestaurants()
        {
            if (_context.RestaurantHomes == null)
            {
                return NotFound();
            }
            return await _context.RestaurantHomes.ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantHome>> GetRestaurant(int id)
        {
            if (_context.RestaurantHomes == null)
            {
                return NotFound();
            }
            var restaurant = await _context.RestaurantHomes.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Person")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, RestaurantHome restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,Person")]
        [HttpPost]
        public async Task<ActionResult<RestaurantHome>> PostRestaurant(RestaurantHome restaurant)
        {
            if (_context.RestaurantHomes == null)
            {
                return Problem("Entity set 'AppDbContext.Restaurants'  is null.");
            }
            _context.RestaurantHomes.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }


        // DELETE: api/Restaurants/5
        [Authorize(Roles = "Admin,Person")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            if (_context.RestaurantHomes == null)
            {
                return NotFound();
            }
            var restaurant = await _context.RestaurantHomes.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.RestaurantHomes.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return (_context.RestaurantHomes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
