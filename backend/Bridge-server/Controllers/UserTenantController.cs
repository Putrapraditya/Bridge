using Boilerplate.DTOs.UserTenant;
using Boilerplate.Entities;
using Boilerplate.Interfaces;
using Boilerplate.DTOs.UserTenant;
using Boilerplate.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTenantController : ControllerBase
    {
        private readonly IUserTenantRepository _repository;
        private readonly ILogger<UserTenantController> _logger;

        public UserTenantController(IUserTenantRepository repository, ILogger<UserTenantController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUserId([FromQuery] Guid userId)
        {
            var result = await _repository.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserTenantRequest request)
        {
            var newMap = new MsUserTenant
            {
                UserId = request.UserId,
                TenantId = request.TenantId,
                CreatedBy = request.UserId
            };
            var result = await _repository.AddAsync(newMap);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
