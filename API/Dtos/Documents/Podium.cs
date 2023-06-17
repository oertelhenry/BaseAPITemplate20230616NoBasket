using Data.Entities;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobalyz.Domain.Odyssey.Entities
{
    [Table("Podium", Schema = "doc")]
    public class Podium : CoreAuditEntity
    {
        public string? DealStatus { get; set; }
        public string? DealAccountNr { get; set; }
        public string? DealerName { get; set; }
        public string? FandIName { get; set; }
        public string? DicAmount { get; set; }
        public string? ClientName { get; set; }
        public string? ClientIdNumber { get; set; }
        public string? NewOrUsed { get; set; }
        public string? VehicleYear { get; set; }
        public string? MMCode { get; set; }
        public string? AgreementType { get; set; }
        public string? VehiclePrice { get; set; }
        public string? RequestedRate { get; set; }
        public string? RateType { get; set; }
        public string? GtiResidualAmount { get; set; }
        public string? ComPVehicleInsurance { get; set; }
        public string? CostOfInsuranceCreditL { get; set; }
        public string? TotalMonthlyInstalment { get; set; }
        public string? AgreementTerm { get; set; }
        public string? ExtrasAmount { get; set; }
        public string? ApprovedDeposit { get; set; }
        public string? ApprovedRate { get; set; }
        public string? KilometerLimit { get; set; }
        public string? InstalmentAmount { get; set; }
        public string? CreditLife { get; set; }
        public string? ServiceFee { get; set; }


        [JsonIgnore]
        public int PodiumNumber { get; set; }
        public string? DateAndTime { get; set; }

        [JsonIgnore]
        public virtual List<CreditRequirements> CreditRequirements { get; set; }
    }

    public class credReq
    {
        public string? Cr { get; set; }
        public string? CreditRequirement { get; set; }
    }
}
