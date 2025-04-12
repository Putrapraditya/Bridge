using Bridge_server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bridge_server.Controllers
{
   [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        private Guid GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(userId, out var id) ? id : Guid.Empty;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting users.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting user by ID.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto dto)
        {
            try
            {
                var user = await _userService.CreateUserAsync(dto, GetCurrentUserId());
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserUpdateDto dto)
        {
            try
            {
                var success = await _userService.UpdateUserAsync(id, dto, GetCurrentUserId());
                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _userService.DeleteUserAsync(id, GetCurrentUserId());
                if (!success) return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
