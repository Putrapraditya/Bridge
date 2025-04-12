
namespace Bridge_server.Interfaces
{
    public interface ITenantRepository
    {
        Task<IEnumerable<TenantDto>> GetAllAsync();
        Task<TenantDto?> GetByIdAsync(Guid id);
        Task<TenantDto> CreateAsync(CreateTenantDto dto, Guid createdBy);
        Task<bool> UpdateAsync(Guid id, UpdateTenantDto dto, Guid updatedBy);
        Task<bool> DeleteAsync(Guid id, Guid deletedBy);
    }
}
