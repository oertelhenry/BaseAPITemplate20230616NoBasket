using Core.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Database
{
    [Table("NetworkTrace", Schema = "db")]
    public class NetworkTrace : CoreEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }

        [StringLength(60)]
        public string CorrelationId { get; set; }

        [StringLength(600)]
        public string Endpoint { get; set; }

        [StringLength(20)]
        public string HttpMethod { get; set; }

        public int? StatusCode { get; set; }

        public long? ElapsedMilliseconds { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string RequestBody { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string ResponseBody { get; set; }

        [StringLength(60)]
        public string Service { get; set; }
    }

}