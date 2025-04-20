using Bridge_server.Data;
using Bridge_server.Entities;
using Bridge_server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bridge_server.Repositories
{
    public class MsMenuRepository : IMsMenuRepository
    {
        private readonly AppDbContext _context;

        public MsMenuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MsMenu>> GetAllAsync()
        {
            return await _context.MsMenus
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<MsMenu?> GetByIdAsync(Guid id)
        {
            return await _context.MsMenus
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<MsMenu>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.MsMenus
                .Where(x => x.ProjectId == projectId && !x.IsDeleted)
                .OrderBy(x => x.Order)
                .ToListAsync();
        }

        public async Task<MsMenu> CreateAsync(MsMenu menu)
        {
            _context.MsMenus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<MsMenu> UpdateAsync(MsMenu menu)
        {
            _context.MsMenus.Update(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            var menu = await _context.MsMenus.FindAsync(id);
            if (menu == null) return false;

            menu.IsDeleted = true;
            menu.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
