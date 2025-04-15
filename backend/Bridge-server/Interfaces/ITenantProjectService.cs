using Bridge_server.DTOs.TenantProjects;
using Bridge_server.DTOs.TenantProjects;

namespace Bridge_server.Interfaces
{
    public interface ITenantProjectService
    {
        Task<IEnumerable<TenantProjectDto>> GetAllAsync();
        Task<TenantProjectDto> CreateAsync(CreateTenantProjectDto dto, Guid createdBy);
        Task<bool> DeleteAsync(Guid id);
    }
}
