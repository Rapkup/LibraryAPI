using LibraryApi.Application.Interfaces.Services;
using LibraryApi.Application.Models.DTO_s;
using LibraryApi.Application.Models.DTO_s.Requests;
using LibraryApi.Infrastructure.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Server.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var loginResult = await _authService.Login(userRequest);

            if (loginResult.IsLogedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successfully done");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost("RefreshToken")]
        [Authorize(Policy = Policies.AdminOrUser)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var loginResult = await _authService.RefreshToken(model);
            if (loginResult.IsLogedIn)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }
}
