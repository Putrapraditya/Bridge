namespace Bridge_server.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(Guid id);
        Task<ProjectDto> CreateAsync(CreateProjectDto dto, Guid createdBy);
        Task<bool> UpdateAsync(Guid id, UpdateProjectDto dto, Guid updatedBy);
        Task<bool> DeleteAsync(Guid id, Guid deletedBy);
    }
}
