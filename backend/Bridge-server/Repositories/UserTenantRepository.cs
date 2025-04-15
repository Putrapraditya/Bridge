using Boilerplate.Entities;
using Boilerplate.Interfaces;
using Boilerplate.Entities;
using Bridge_server.Data;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Repositories
{
    public class UserTenantRepository : IUserTenantRepository
    {
        private readonly AppDbContext _context;
        public UserTenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MsUserTenant>> GetAllAsync()
        {
            return await _context.MsUserTenant.Include(x => x.User).Include(x => x.Tenant).ToListAsync();
        }

        public async Task<IEnumerable<MsUserTenant>> GetByUserIdAsync(Guid userId)
        {
            return await _context.MsUserTenant
                .Include(x => x.Tenant)
                .Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<MsUserTenant?> AddAsync(MsUserTenant data)
        {
            _context.MsUserTenant.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.MsUserTenant.FindAsync(id);
            if (entity == null) return false;
            _context.MsUserTenant.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}