using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheKnife.API.Constants;
using TheKnife.API.Models.Authentication;
using TheKnife.API.Models.Settings;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;
using static TheKnife.Entities.Efos.RestaurantResponsiblesEfo;


namespace TheKnife.API.Controllers
{
    [ApiExplorerSettings(GroupName = ApiConstants.DocumentationGroupAuthentication)]
    public class AuthenticationController : ControllerBase
    {
        private readonly TheKnifeDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationController(TheKnifeDbContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        [AllowAnonymous]
        [HttpPost("/userlogin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginUser([FromBody, Required] LoginUserRequest loginUserRequest)
        {
            if (loginUserRequest == null)
            {
                return BadRequest("Email e Password são obrigatórios.");
            }

            RegisterUsersEfo user = await _context.RegisterUsers.FirstOrDefaultAsync(
                u => u.Email == loginUserRequest.Email && u.Password == loginUserRequest.Password);

            if (user == null)
            {
                return Unauthorized("Dados inválidos.");
            }

            var userIssuer = _jwtSettings.Issuer;
            var userAudience = _jwtSettings.Audience;
            var userKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var userTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.Sub, loginUserRequest.Email),
                    new Claim(JwtRegisteredClaimNames.Email, "a@a.pt"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Issuer = userIssuer,
                Audience = userAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(userKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var userTokenHandler = new JwtSecurityTokenHandler();
            var userToken = userTokenHandler.CreateToken(userTokenDescriptor);
            var userJwtToken = userTokenHandler.WriteToken(userToken);

            return Ok(new LoginUserResponse(userJwtToken));
        }

        [AllowAnonymous]
        [HttpPost("/responsiblelogin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginResponsibleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginResponsible([FromBody, Required] LoginResponsibleRequest loginResponsibleRequest)
        {
            if (loginResponsibleRequest == null)
            {
                return BadRequest("Email e Password são obrigatórios.");
            }

            RestaurantRegistrationsEfo responsible = await _context.RestaurantRegistrations.FirstOrDefaultAsync(
                r => r.Email == loginResponsibleRequest.Email && r.Password == loginResponsibleRequest.Password);

            if (responsible == null)
            {
                return Unauthorized("Dados inválidos.");
            }

            var responsibleIssuer = _jwtSettings.Issuer;
            var responsibleAudience = _jwtSettings.Audience;
            var responsibleKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var responsibleTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Responsible"),
                    new Claim(JwtRegisteredClaimNames.Sub, loginResponsibleRequest.Email),
                    new Claim(JwtRegisteredClaimNames.Email, "a@a.pt"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Issuer = responsibleIssuer,
                Audience = responsibleAudience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(responsibleKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var responsibleTokenHandler = new JwtSecurityTokenHandler();
            var responsibleToken = responsibleTokenHandler.CreateToken(responsibleTokenDescriptor);
            var responsibleJwtToken = responsibleTokenHandler.WriteToken(responsibleToken);

            return Ok(new LoginResponsibleResponse(responsibleJwtToken));
        }

        [AllowAnonymous]
        [HttpPost("/adminlogin")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginAdminResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginAdmin([FromBody, Required] LoginAdminRequest loginRequest)
        {
            if (loginRequest.UserName == "RuiBarreto" && loginRequest.Password == "Rui@Barreto-123")
            {
                var adminIssuer = _jwtSettings.Issuer;
                var adminAudience = _jwtSettings.Audience;
                var adminKey = Encoding.ASCII.GetBytes(_jwtSettings.Key);
                var adminTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                        new Claim(JwtRegisteredClaimNames.Sub, loginRequest.UserName),
                        new Claim(JwtRegisteredClaimNames.Email, "a@a.pt"),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Issuer = adminIssuer,
                    Audience = adminAudience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(adminKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var adminTokenHandler = new JwtSecurityTokenHandler();
                var adminToken = adminTokenHandler.CreateToken(adminTokenDescriptor);
                var adminJwtToken = adminTokenHandler.WriteToken(adminToken);

                return Ok(new LoginAdminResponse(adminJwtToken));
            }
            return Unauthorized();
        }
    }
}
