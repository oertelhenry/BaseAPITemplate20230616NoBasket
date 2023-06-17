using Core.Data.Annotations;
using Data.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Entities
{
    public abstract class CoreAuditEntity : CoreEntity, IAuditEntity
    {
        [JsonIgnore]
        [Column(TypeName = "datetime2")]
        [PopulationJsonIgnore]
        public DateTime CreatedDate { get; set; }

        [JsonIgnore]
        [Column(TypeName = "datetime2")]
        [PopulationJsonIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
