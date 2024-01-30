using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TheKnife.Entities.Efos;
using TheKnife.Services.Services;
using System.ComponentModel.DataAnnotations;

namespace TheKnife.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterUsersController : ControllerBase
    {
        private readonly IRegisterUsersService _registerUsersService;

        public RegisterUsersController(IRegisterUsersService registerUsersService)
        {
            _registerUsersService = registerUsersService;
        }

        // GET registerusers
        [HttpGet]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<RegisterUsersEfo>), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<List<RegisterUsersEfo>>> GetAllRegisterUsers()
        {
            List<RegisterUsersEfo> registerUsers = await _registerUsersService.GetAllRegisterUsers();

            if (registerUsers == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(registerUsers);
        }

        // GET registerusers/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetRegisterUserByIdAsync(int id)
        {
            RegisterUsersEfo registerUser = await _registerUsersService.GetRegisterUserById(id);

            if (registerUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(registerUser);
        }

        // POST registerusers
        [HttpPost]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<RegisterUsersEfo>> SendRegisterUser([FromBody, Required] RegisterUsersEfo registerUser)
        {
            if (ModelState.IsValid)
            {
                RegisterUsersEfo registoUser = await _registerUsersService.SendRegisterUser(registerUser);

                if (registoUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return StatusCode(StatusCodes.Status201Created, registoUser);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        // PUT registerusers/{email}/updatepassword
        [HttpPut("{email}/updatepassword")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateUserPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                RegisterUsersEfo password = await _registerUsersService.UpdateUserPassword(email, newPassword, confirmPassword);

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

        // PUT registerusers/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterUsersEfo), StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> UpdateRegisterUser(int id, [FromBody] RegisterUsersEfo updateRegisterUser)
        {
            try
            {
                RegisterUsersEfo newRegisterUser = await _registerUsersService.UpdateRegisterUser(id, updateRegisterUser);

                if (newRegisterUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }

                return Ok(newRegisterUser);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE registerusers/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> DeleteRegisterUser(int id)
        {
            try
            {
                await _registerUsersService.DeleteRegisterUser(id);

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
