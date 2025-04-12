using Bridge_server.Data;
using Bridge_server.Entities;
using Bridge_server.Interfaces;
using Bridge_server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bridge_server.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TenantDto>> GetAllAsync()
        {
            return await _context.MsTenant
                .Where(t => t.isActive == 1)
                .Select(t => new TenantDto
                {
                    Id = t.Id,
                    TenantName = t.TenantName,
                    Description = t.Description,
                    IsActive = t.isActive
                }).ToListAsync();
        }

        public async Task<TenantDto?> GetByIdAsync(Guid id)
        {
            var t = await _context.MsTenant.FindAsync(id);
            if (t == null || t.isActive == 0) return null;

            return new TenantDto
            {
                Id = t.Id,
                TenantName = t.TenantName,
                Description = t.Description,
                IsActive = t.isActive
            };
        }

        public async Task<TenantDto> CreateAsync(CreateTenantDto dto, Guid createdBy)
        {
            var tenant = new MsTenant
            {
                Id = Guid.NewGuid(),
                TenantName = dto.TenantName,
                Description = dto.Description,
                isActive = 1,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            _context.MsTenant.Add(tenant);
            await _context.SaveChangesAsync();

            return new TenantDto
            {
                Id = tenant.Id,
                TenantName = tenant.TenantName,
                Description = tenant.Description,
                IsActive = tenant.isActive
            };
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTenantDto dto, Guid updatedBy)
        {
            var tenant = await _context.MsTenant.FindAsync(id);
            if (tenant == null || tenant.isActive == 0) return false;

            tenant.TenantName = dto.TenantName;
            tenant.Description = dto.Description;
            tenant.isActive = dto.IsActive;
            tenant.UpdatedAt = DateTime.UtcNow;
            tenant.UpdatedBy = updatedBy;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id, Guid deletedBy)
        {
            var tenant = await _context.MsTenant.FindAsync(id);
            if (tenant == null || tenant.isActive == 0) return false;

            tenant.isActive = 0;
            tenant.UpdatedAt = DateTime.UtcNow;
            tenant.UpdatedBy = deletedBy;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
