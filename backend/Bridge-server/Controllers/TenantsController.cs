using Bridge_server.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bridge_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantRepository _service;
        private readonly ILogger<TenantsController> _logger;

        public TenantsController(ITenantRepository service, ILogger<TenantsController> logger)
        {
            _service = service;
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
                var data = await _service.GetAllAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting tenants.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);
                if (data == null) return NotFound();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting tenant by id.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantDto dto)
        {
            try
            {
                var data = await _service.CreateAsync(dto, GetCurrentUserId());
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating tenant.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTenantDto dto)
        {
            try
            {
                var success = await _service.UpdateAsync(id, dto, GetCurrentUserId());
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating tenant.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id, GetCurrentUserId());
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting tenant.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
