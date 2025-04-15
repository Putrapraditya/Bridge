using Bridge_server.DTOs.TenantProjects;
using Bridge_server.Interfaces;
using Bridge_server.DTOs.TenantProjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bridge_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TenantProjectsController : ControllerBase
    {
        private readonly ITenantProjectService _service;
        private readonly ILogger<TenantProjectsController> _logger;

        public TenantProjectsController(ITenantProjectService service, ILogger<TenantProjectsController> logger)
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
                _logger.LogError(ex, "Error while getting tenant-project mappings");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTenantProjectDto dto)
        {
            try
            {
                var data = await _service.CreateAsync(dto, GetCurrentUserId());
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating tenant-project mapping");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting tenant-project mapping");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
