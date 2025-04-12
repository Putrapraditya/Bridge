namespace Bridge_server.Entities
{
    public class MsProject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ProjectName { get; set; }
        public string? Url { get; set; }
        public byte isActive { get; set; } = 1;

        // Audit
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
