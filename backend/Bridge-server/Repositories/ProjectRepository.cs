using Bridge_server.Data;
using Bridge_server.Entities;
using Bridge_server.Interfaces;
using Bridge_server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge_server.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            return await _context.MsProject
                .Where(p => p.isActive == 1)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Url = p.Url,
                    isActive = p.isActive
                }).ToListAsync();
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var project = await _context.MsProject.FindAsync(id);
            if (project == null || project.isActive == 0) return null;

            return new ProjectDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Url = project.Url,
                isActive = project.isActive
            };
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, Guid createdBy)
        {
            var entity = new MsProject
            {
                Id = Guid.NewGuid(),
                ProjectName = dto.ProjectName,
                Url = dto.Url,
                isActive = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.MsProject.Add(entity);
            await _context.SaveChangesAsync();

            return new ProjectDto
            {
                Id = entity.Id,
                ProjectName = entity.ProjectName,
                Url = entity.Url,
                isActive = entity.isActive
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProjectDto dto, Guid updatedBy)
        {
            var entity = await _context.MsProject.FindAsync(id);
            if (entity == null || entity.isActive == 0) return false;

            entity.ProjectName = dto.ProjectName;
            entity.Url = dto.Url;
            entity.isActive = dto.isActive;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid deletedBy)
        {
            var entity = await _context.MsProject.FindAsync(id);
            if (entity == null || entity.isActive == 0) return false;

            entity.isActive = 0;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = deletedBy;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
