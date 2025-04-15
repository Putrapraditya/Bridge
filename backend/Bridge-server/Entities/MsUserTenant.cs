using System;
using Bridge_server.Entities;

namespace Boilerplate.Entities
{
    public class MsUserTenant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }

        // Navigation Properties
        public MsUsersC? User { get; set; }
        public MsTenant? Tenant { get; set; }
    }
}