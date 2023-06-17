namespace Mobalyz.Domain.Odyssey.Models
{
    public class TemplateDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; } = "";
        public string? FileName { get; set; } = "";
        public string? Location { get; set; } = "";
        public string? Description { get; set; } = "";
        public Int64? FileSize { get; set; }
        public string? FileType { get; set; } = "";
        public string? FileAsBase64 { get; set; } = "";
        public string? lastModifiedDate { get; set; } = "";
        public string? lastModifiedTime { get; set; } = "";
        public string? templateName { get; set; } = "";
        public string? fileCategory { get; set; } = "";
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
