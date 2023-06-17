using Data.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;


namespace Mobalyz.Domain.Odyssey.Entities
{
    [Table("CreditRequirements", Schema = "doc")]
    public class CreditRequirements : CoreAuditEntity
    {
        [JsonIgnore]
        public virtual Podium Podium { get; set; }

        public string? CreditRequirement { get; set; }

    }
}
