using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Database
{
    [Table("Audit", Schema = "db")]
    public class Audit
    {

        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        public string UserId { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string TableName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        //[Column(TypeName = "varchar(max)")]
        public string OldValues { get; set; }

        //[Column(TypeName = "varchar(max)")]
        public string NewValues { get; set; }

        //[Column(TypeName = "varchar(max)")]
        public string AffectedColumns { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string PrimaryKey { get; set; }
    }

}