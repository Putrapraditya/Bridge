using System.ComponentModel.DataAnnotations;

namespace Bridge_server.Entities
{
    public class MsMenu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Url { get; set; }

        [Required]
        public Guid ProjectId { get; set; } // Relasi ke Project (Web Bisnisflow)

        public bool IsActive { get; set; } = true;

        public int Order { get; set; } = 0;

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
