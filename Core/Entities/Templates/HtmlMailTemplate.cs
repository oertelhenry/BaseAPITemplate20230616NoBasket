using Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Odyssey.Entities.Documents
{
    [Table("HtmlMailTemplate", Schema = "templ")]
    public class HtmlMailTemplate : CoreAuditEntity
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; } = "";
        public string? FileName { get; set; } = "";
        public string? Location { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? FileSize { get; set; } = "";
        public string? FileType { get; set; } = "";
        public string? LastModifiedDate { get; set; } = "";
        public string? LastModifiedTime { get; set; } = "";
        public string? TemplateName { get; set; } = "";

    }
}
