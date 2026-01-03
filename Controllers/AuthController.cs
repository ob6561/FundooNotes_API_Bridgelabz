using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ModelLayer.DTOs;

namespace FundooNotes.API.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _service;

        public AuthController(UserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _service.Register(dto);
            if (!result)
                return BadRequest("User already exists");

            return Ok("User registered successfully");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = _service.Login(dto);
            if (token == null)
                return Unauthorized("Invalid email or password");

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("secure-test")]
        public IActionResult SecureTest()
        {
            return Ok("You accessed a protected API!");
        }
    }
}
