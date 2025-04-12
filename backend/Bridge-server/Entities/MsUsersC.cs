using System.ComponentModel.DataAnnotations;

namespace Bridge_server.Entities
{
    public class MsUsersC
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Phonenumber { get; set; }
        public string? Email { get; set; }
        public byte isActive { get; set; }
        public string? Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
