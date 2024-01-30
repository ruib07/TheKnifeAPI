using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestaurantRegistrationsController : ControllerBase
    {
        private readonly IRestaurantRegistrationsService _restaurantRegistrationsService;

        public RestaurantRegistrationsController(IRestaurantRegistrationsService restaurantRegistrationsService)
        {
            _restaurantRegistrationsService = restaurantRegistrationsService;
        }

        // GET restaurantregistrations
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantRegistrationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantRegistrationsEfo>>> GetAllRestaurantRegistrations()
        {
            List<RestaurantRegistrationsEfo> restaurantRegistrations = await _restaurantRegistrationsService
                .GetAllRestaurantRegistrations();

            if (restaurantRegistrations == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurantRegistrations);
        }

        // GET restaurantregistrations/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantRegistrationById(int id)
        {
            RestaurantRegistrationsEfo restaurantRegistration = await _restaurantRegistrationsService
                .GetRestaurantRegistrationById(id);

            if (restaurantRegistration == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurantRegistration);
        }

        // POST restaurantregistrations
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantRegistrationsEfo>> SendRestaurantRegistration(
            [FromBody, Required] RestaurantRegistrationsEfo restaurantRegistration)
        {
            if (ModelState.IsValid)
            {
                RestaurantRegistrationsEfo restauratResPost = await _restaurantRegistrationsService
                    .SendRestaurantRegistration(restaurantRegistration);

                if (restauratResPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restauratResPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT restaurantregistrations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantRegistrationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurantRegistration(int id, 
            [FromBody] RestaurantRegistrationsEfo updateRestaurantRegistration)
        {
            try
            {
                RestaurantRegistrationsEfo newRestaurantRes = await _restaurantRegistrationsService
                    .UpdateRestaurantRegistration(id, updateRestaurantRegistration);

                if (newRestaurantRes == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newRestaurantRes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE restaurantregistrations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurantRegistration(int id)
        {
            try
            {
                await _restaurantRegistrationsService.DeleteRestaurantRegistration(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
