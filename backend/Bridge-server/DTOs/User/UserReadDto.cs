public class UserReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public byte isActive { get; set; }
}
