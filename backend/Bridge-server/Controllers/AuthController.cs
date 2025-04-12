using Bridge_server.DTOs.Auth;
using Bridge_server.Interfaces;
using Bridge_server.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Bridge_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthRepository authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                if (result == null)
                    return Unauthorized("Invalid username or password");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
