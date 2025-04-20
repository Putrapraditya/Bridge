// Controllers/MsMenuController.cs
using Bridge_server.Interfaces;
using Bridge_server.Entities;
using Bridge_server.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bridge_server.DTOs.MsMenu;
using Bridge_server.Entities;

namespace Bridge.Controllers
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
                var result = menus.Select(m => new MsMenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Url = m.Url,
                    Icon = m.Icon,
                    ParentId = m.ParentId,
                    ProjectId = m.ProjectId,
                    IsShow = m.IsShow
                });
                return Ok(result);
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

                var result = new MsMenuDto
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    Url = menu.Url,
                    Icon = menu.Icon,
                    ParentId = menu.ParentId,
                    ProjectId = menu.ProjectId,
                    IsShow = menu.IsShow
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving menu by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMsMenuDto dto)
        {
            try
            {
                var menu = new MsMenu
                {
                    Id = Guid.NewGuid(),
                    Name = dto.Name,
                    Url = dto.Url,
                    Icon = dto.Icon,
                    ParentId = dto.ParentId,
                    ProjectId = dto.ProjectId,
                    IsShow = dto.IsShow,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = dto.CreatedBy
                };

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
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMsMenuDto dto)
        {
            try
            {
                // Get existing menu
                var existingMenu = await _menuRepository.GetByIdAsync(id);
                if (existingMenu == null) return NotFound();

                // Map DTO to entity
                existingMenu.Name = dto.Title; // Note the property name difference
                existingMenu.Url = dto.Url;
                existingMenu.Icon = dto.Icon;
                existingMenu.ParentId = dto.ParentId;
                existingMenu.IsShow = dto.IsShow;
                existingMenu.UpdatedAt = DateTime.UtcNow;
                existingMenu.UpdatedBy = dto.UpdatedBy;

                // Update
                var updatedMenu = await _menuRepository.UpdateAsync(existingMenu);
                return Ok(updatedMenu); // or return NoContent() if you prefer
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
