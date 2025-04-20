using Bridge_server.Entities;

namespace Bridge_server.Interfaces
{
    public interface IMenuGroupTenantRepository
    {
        Task<IEnumerable<MenuGroupTenant>> GetAllAsync();
        Task<IEnumerable<MenuGroupTenant>> GetByTenantIdAsync(Guid tenantId);
        Task<MenuGroupTenant?> GetByIdAsync(Guid id);
        Task<MenuGroupTenant> CreateAsync(MenuGroupTenant entity);
        Task<MenuGroupTenant> UpdateAsync(Guid id, MenuGroupTenant entity);
        Task<bool> SoftDeleteAsync(Guid id);
    }
}
