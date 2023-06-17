using Mobalyz.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobalyz.Domain.Odyssey.Models
{
    public class PodiumRequestDto
    {
        public string? Category { get; set; } = "podium";
        public string DealStatus { get; set; } = "";
        public string DealAccountNr { get; set; } = "";
        public string DealerName { get; set; } = "";
        public string FandIName { get; set; } = "";
        public string DicAmount { get; set; } = "";
        public string ClientName { get; set; } = "";
        public string ClientIdNumber { get; set; } = "";
        public string NewOrUsed { get; set; } = "";
        public string MakeAndModel { get; set; } = "";
        public string VehicleYear { get; set; } = "";
        public string MMCode { get; set; } = "";
        public string AgreementType { get; set; } = "";
        public string VehiclePrice { get; set; } = "";
        public string RequestedRate { get; set; } = "";
        public string RateType { get; set; } = "";
        public string GtiResidualAmount { get; set; } = "";
        public string ComPVehicleInsurance { get; set; } = "";
        public string CostOfInsuranceCreditL { get; set; } = "";
        public string TotalMonthlyInstalment { get; set; } = "";
        public string AgreementTerm { get; set; } = "";
        public string ExtrasAmount { get; set; } = "";
        public string ApprovedDeposit { get; set; } = "";
        public string ApprovedRate { get; set; } = "";
        public string KilometerLimit { get; set; } = "";
        public string InstalmentAmount { get; set; } = "";
        public string CreditLife { get; set; } = "";
        public string ServiceFee { get; set; } = "";
        public string PrincipalDept { get; set; } = "";

        public List<PodCreditRequirements> CreditRequirements { get; set; }

        //[JsonIgnore]
        public int PodiumNumber { get; set; }
        
        [JsonIgnore]
        public string DateAndTime { get; set; } = "";
    }

    public class PodCreditRequirements
    {
        public string creditRequirement { get; set; } = "";
    }
}
