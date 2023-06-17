namespace Mobalyz.Odyssey.Data.DTOs.Templates
{
    public class PdfTemplateUploadDto
    {
        public string? FileName { get; set; }
        public Int64 FileSize { get; set; }
        public string? FileType { get; set; }
        public string? FileAsBase64 { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; } = "";
    }
}
