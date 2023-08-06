using Microsoft.AspNetCore.Mvc;
using System.Net;
using TastingClubBLL.DTOs.ApplicationUserDTOs;
using TastingClubBLL.Exceptions;
using TastingClubBLL.Interfaces.IServices;

namespace TastingClubPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login(ApplicationUserDtoForLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Model");
            }
            try
            {
                return Ok(await _authService.LoginAsync(user));
            }
            catch (HttpStatusException httpStatusException)
            {
                return httpStatusException.Status switch
                {
                    HttpStatusCode.NotFound => NotFound(httpStatusException.Message),
                    _ => BadRequest()
                };
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(ApplicationUserDtoForRegister user)
        {
            try
            {
                if (await _authService.RegisterUserAsync(user))
                {
                    return Ok("Successful registration");
                }

                return BadRequest("smth went wrong");
            }
            catch (HttpStatusException httpStatusException)
            {
                return httpStatusException.Status switch
                {
                    HttpStatusCode.NotFound => NotFound(httpStatusException.Message),
                    _ => BadRequest()
                };
            }
        }
    }
}
