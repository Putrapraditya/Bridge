public class CreateMenuGroupTenantDto
{
    public Guid TenantId { get; set; }
    public Guid MenuId { get; set; }
    public bool IsShow { get; set; }
    public Guid CreatedBy { get; set; }
}