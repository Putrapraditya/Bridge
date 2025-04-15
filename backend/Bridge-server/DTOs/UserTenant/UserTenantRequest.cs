namespace Boilerplate.DTOs.UserTenant
{
    public class UserTenantRequest
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}