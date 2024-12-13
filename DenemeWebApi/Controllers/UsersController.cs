using DenemeFileOrbis.library.Responses;
using DenemeFileOrbis.library.Models;
using DenemeWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DenemeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateUser([FromBody] UserLoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new ServiceResponse
                {
                    Message = "Invalid username or password",
                    Success = false
                });
            }

            var response = await userService.ValidateUserAsync(request.Username, request.Password);

            if (!response.Success)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }

    public class UserLoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}

