using Bridge_server.Data;
using Bridge_server.DTOs.TenantProjects;
using Bridge_server.Entities;
using Bridge_server.Interfaces;
using Bridge_server.DTOs.TenantProjects;
using Bridge_server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge_server.Repositories
{
    public class TenantProjectService : ITenantProjectService
    {
        private readonly AppDbContext _context;

        public TenantProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TenantProjectDto>> GetAllAsync()
        {
            return await _context.MsTenantProject
                .Select(mp => new TenantProjectDto
                {
                    Id = mp.Id,
                    TenantId = mp.TenantId,
                    ProjectId = mp.ProjectId
                })
                .ToListAsync();
        }

        public async Task<TenantProjectDto> CreateAsync(CreateTenantProjectDto dto, Guid createdBy)
        {
            var mapping = new MsTenantProject
            {
                TenantId = dto.TenantId,
                ProjectId = dto.ProjectId,
                CreatedBy = createdBy,
                CreatedAt = DateTime.UtcNow
            };

            _context.MsTenantProject.Add(mapping);
            await _context.SaveChangesAsync();

            return new TenantProjectDto
            {
                Id = mapping.Id,
                TenantId = mapping.TenantId,
                ProjectId = mapping.ProjectId
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.MsTenantProject.FindAsync(id);
            if (entity == null) return false;

            _context.MsTenantProject.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
