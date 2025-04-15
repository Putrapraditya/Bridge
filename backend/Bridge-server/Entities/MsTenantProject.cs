using Bridge_server.Entities;

namespace Bridge_server.Entities
{
    public class MsTenantProject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TenantId { get; set; }
        public Guid ProjectId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }

        // Navigation (optional)
        public MsTenant? Tenant { get; set; }
        public MsProject? Project { get; set; }
    }
}
