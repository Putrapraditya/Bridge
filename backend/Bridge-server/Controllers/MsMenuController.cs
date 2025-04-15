// Controllers/MsMenuController.cs
using Bridge_server.Interfaces;
using Bridge_server.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Bridge_server.Entities;

namespace Bridge_server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MsMenuController : ControllerBase
    {
        private readonly IMsMenuRepository _menuRepository;
        private readonly ILogger<MsMenuController> _logger;

        public MsMenuController(IMsMenuRepository menuRepository, ILogger<MsMenuController> logger)
        {
            _menuRepository = menuRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var menus = await _menuRepository.GetAllAsync();
                return Ok(menus);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving menus");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var menu = await _menuRepository.GetByIdAsync(id);
                if (menu == null) return NotFound();
                return Ok(menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving menu by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MsMenu menu)
        {
            try
            {
                await _menuRepository.CreateAsync(menu);
                return CreatedAtAction(nameof(GetById), new { id = menu.Id }, menu);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating menu");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] MsMenu menu)
        {
            try
            {
                var updated = await _menuRepository.UpdateAsync(id, menu);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating menu");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _menuRepository.SoftDeleteAsync(id);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting menu");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
