public class TenantDto
{
    public Guid Id { get; set; }
    public string TenantName { get; set; }
    public string? Description { get; set; }
    public byte IsActive { get; set; }
}