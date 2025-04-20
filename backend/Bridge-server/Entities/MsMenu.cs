using System.ComponentModel.DataAnnotations;

namespace Bridge_server.Entities
{
    public class MsMenu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public int Order { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsShow { get; set; } = true;

        // Audit
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
