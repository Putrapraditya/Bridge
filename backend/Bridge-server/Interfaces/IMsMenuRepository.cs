using Bridge_server.DTOs.MsMenu;
using Bridge_server.Entities;

namespace Bridge_server.Interfaces
{
    public interface IMsMenuRepository
    {
        Task<IEnumerable<MsMenu>> GetAllAsync();
        Task<MsMenu?> GetByIdAsync(Guid id);
        Task<IEnumerable<MsMenu>> GetByProjectIdAsync(Guid projectId);
        Task<MsMenu> CreateAsync(MsMenu menu);
        Task<MsMenu> UpdateAsync(MsMenu menu);
        Task<bool> SoftDeleteAsync(Guid id);
    }
}
