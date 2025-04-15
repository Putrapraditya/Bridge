namespace Bridge_server.DTOs.TenantProjects
{
    public class CreateTenantProjectDto
    {
        public Guid TenantId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
