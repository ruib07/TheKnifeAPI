using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;

        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;
        }

        // GET restaurants
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantsEfo>>> GetAllRestaurants()
        {
            List<RestaurantsEfo> restaurants = await _restaurantsService
                .GetAllRestaurants();

            if (restaurants == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurants);
        }

        // GET restaurants/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            RestaurantsEfo restaurant = await _restaurantsService
                .GetRestaurantById(id);

            if (restaurant == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurant);
        }

        // POST restaurants
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantsEfo>> SendRestaurant([FromForm, Required] RestaurantsEfo restaurant)
        {
            if (ModelState.IsValid)
            {
                RestaurantsEfo restaurantPost = await _restaurantsService
                    .SendRestaurant(restaurant);

                if (restaurantPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restaurantPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT restaurants/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantsEfo updateRestaurant)
        {
            try
            {
                RestaurantsEfo newRestaurant = await _restaurantsService
                    .UpdateRestaurant(id, updateRestaurant);

                if (newRestaurant == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newRestaurant);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE restaurants/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            try
            {
                await _restaurantsService.DeleteRestaurant(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
