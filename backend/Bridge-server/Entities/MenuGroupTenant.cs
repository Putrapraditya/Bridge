namespace Bridge_server.Entities
{
    public class MenuGroupTenant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TenantId { get; set; }
        public Guid MenuId { get; set; }
        public bool IsShow { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public MsTenant Tenant { get; set; }
        public MsMenu Menu { get; set; }
    }
}
