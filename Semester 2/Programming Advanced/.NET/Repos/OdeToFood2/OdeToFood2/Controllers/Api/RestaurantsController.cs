using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood2.Data.DomainClasses;
using OdeToFood2.Data.IRepositories;

namespace OdeToFood2.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;


        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        // GET: api/Restaurants
        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(_restaurantRepository.GetAllAsync());
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}", Name = "GetRestaurant")]
        public IActionResult GetRestaurant(int id)
        {
            var restaurant = _restaurantRepository.GetByIdAsync(id);

            if (restaurant == null) return NotFound();

            return Ok(restaurant);
        }

        // POST: api/Restaurants
        [HttpPost]
        public IActionResult Post([FromBody] Restaurant newRestaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var createdRestaurant = _restaurantRepository.AddAsync(newRestaurant);

            return CreatedAtRoute("DefaultApi", new { controller = "Restaurants", id = createdRestaurant.Id }, createdRestaurant);
        }

        // PUT: api/Restaurants/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            if (_restaurantRepository.GetByIdAsync(id) == null)
            {
                return NotFound();
            }

            _restaurantRepository.UpdateAsync(id,restaurant);

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant== null)
            {
                return NotFound();
            }

            await _restaurantRepository.DeleteAsync(restaurant);

            return Ok();
        }
    }
}
