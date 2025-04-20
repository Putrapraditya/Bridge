using System.ComponentModel.DataAnnotations;

namespace Bridge_server.DTOs.MsMenu
{
    public class UpdateMsMenuDto
    {
        public string Title { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
        public Guid? ParentId { get; set; }
        public bool IsShow { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
