namespace Bridge_server.DTOs.MsMenu
{
    public class MsMenuDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsShow { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
