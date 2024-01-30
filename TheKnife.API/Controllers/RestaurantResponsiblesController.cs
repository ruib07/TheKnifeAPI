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
    public class RestaurantResponsiblesController : ControllerBase
    {
        private readonly IRestaurantResponsiblesService _restaurantResponsiblesService;

        public RestaurantResponsiblesController(IRestaurantResponsiblesService restaurantResponsiblesService)
        {
            _restaurantResponsiblesService = restaurantResponsiblesService;
        }

        // GET restaurantresponsibles
        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RestaurantResponsiblesEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RestaurantResponsiblesEfo>>> GetAllRestaurantResponsibles()
        {
            List<RestaurantResponsiblesEfo> restaurantResponsibles = await _restaurantResponsiblesService
                .GetAllRestaurantResponsibles();

            if (restaurantResponsibles == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurantResponsibles);
        }

        // GET restaurantresponsibles/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRestaurantResponsibleById(int id)
        {
            RestaurantResponsiblesEfo restaurantResponsible = await _restaurantResponsiblesService
                .GetRestaurantResponsibleById(id);

            if (restaurantResponsible == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(restaurantResponsible);
        }

        // POST restaurantresponsibles
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RestaurantResponsiblesEfo>> SendRestaurantResponsible(
            [FromBody, Required] RestaurantResponsiblesEfo restaurantResponsible)
        {
            if (ModelState.IsValid)
            {
                RestaurantResponsiblesEfo restaurantResponsiblePost = await _restaurantResponsiblesService
                    .SendRestaurantResponsible(restaurantResponsible);

                if (restaurantResponsiblePost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, restaurantResponsiblePost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT restaurantresponsibles/{email}/updatepassword
        [HttpPut("{email}/updatepassword")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateResponsiblePassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                RestaurantResponsiblesEfo password = await _restaurantResponsiblesService
                    .UpdateRestaurantResponsiblePassword(email, newPassword, confirmPassword);

                if (password == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar password: {ex.Message}");
            }
        }

        // PUT restaurantresponsibles/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RestaurantResponsiblesEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRestaurantResponsible(int id, [FromBody] RestaurantResponsiblesEfo updateResponsible)
        {
            try
            {
                RestaurantResponsiblesEfo newResponsible = await _restaurantResponsiblesService
                    .UpdateRestaurantResponsible(id, updateResponsible);

                if (newResponsible == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newResponsible);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE restaurantresponsibles/{id}
        [Authorize(Policy = "AdminAndResponsible")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRestaurantResponsible(int id)
        {
            try
            {
                await _restaurantResponsiblesService.DeleteRestaurantResponsible(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
