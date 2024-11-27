using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Models;
using CarRentalSystem.Services;
using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // POST /users/register
        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = _userService.RegisterUser(user);
            if (!success)
                return BadRequest("User with this email already exists.");

            return Ok("User registered successfully.");
        }

        // POST /users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var token = _userService.AuthenticateUser(request.Email, request.Password);
            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
