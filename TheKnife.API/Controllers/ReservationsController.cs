using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;

namespace TheKnife.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        // GET reservations
        [HttpGet]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<ReservationsEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<ReservationsEfo>>> GetAllReservations()
        {
            List<ReservationsEfo> reservations = await _reservationsService
                .GetAllReservations();

            if (reservations == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(reservations);
        }

        // GET reservations/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetReservationById(int id)
        {
            ReservationsEfo reservation = await _reservationsService
                .GetReservationById(id);

            if (reservation == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(reservation);
        }

        // POST reservations
        [HttpPost]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<ReservationsEfo>> SendReservation([FromForm, Required] ReservationsEfo reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationsEfo reservationPost = await _reservationsService
                    .SendReservation(reservation);

                if (reservationPost == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, reservationPost);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT reservations/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ReservationsEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateReservation(int id, [FromForm] ReservationsEfo updateReservation)
        {
            try
            {
                ReservationsEfo newReservation = await _reservationsService
                    .UpdateReservation(id, updateReservation);

                if (newReservation == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newReservation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE reservations/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            try
            {
                await _reservationsService.DeleteReservation(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
