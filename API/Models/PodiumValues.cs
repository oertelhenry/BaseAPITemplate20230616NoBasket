using Mobalyz.Domain.Odyssey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobalyz.Odyssey.Resources.Data.Models.Integration
{
    public class PodiumValues
    {
        public PodiumValues(PodiumRequestDto request)
        {
            DealStatus = request.DealStatus;
            DealAccountNr = request.DealAccountNr;
            DealerName = request.DealerName;
            FandIName = request.FandIName;
            DicAmount = request.DicAmount;
            ClientName = request.ClientName;
            ClientIdNumber = request.ClientIdNumber;
            NewOrUsed = request.NewOrUsed;
            MakeAndModel = request.MakeAndModel;
            VehicleYear = request.VehicleYear;
            MMCode = request.MMCode;
            AgrmntType = request.AgreementType;
            VehiclePrice = request.VehiclePrice;
            PrincDept = request.PrincipalDept;
            RequestedRate = request.RequestedRate;
            LinkedOrFixed = request.RateType;
            ResidualAmt = request.GtiResidualAmount;
            InsurancePmt = request.ComPVehicleInsurance;
            InsCredLife = request.CostOfInsuranceCreditL;
            TtlMthlyInslmt = request.TotalMonthlyInstalment;
            AgreementTerm = request.AgreementTerm;
            ExtrasAmount = request.ExtrasAmount;
            ApprovedDeposit = request.ApprovedDeposit;
            ApprovedRate = request.ApprovedRate;
            KMLimit = request.KilometerLimit;
            InstalmentAmount = request.InstalmentAmount;
            CreditLife = request.CreditLife;
            ServiceFee = request.ServiceFee;
            PodiumNumber = request.PodiumNumber.ToString();
            DateAndTime = request.DateAndTime;
        }
        public string DealStatus { get; set; }
        public string DealAccountNr { get; set; }
        public string DealerName { get; set; }
        public string FandIName { get; set; }
        public string DicAmount { get; set; }
        public string ClientName { get; set; }
        public string ClientIdNumber { get; set; }
        public string NewOrUsed { get; set; }
        public string MakeAndModel { get; set; }
        public string VehicleYear { get; set; }
        public string MMCode { get; set; }
        public string AgrmntType { get; set; }
        public string VehiclePrice { get; set; }
        public string PrincDept { get; set; }
        public string RequestedRate { get; set; }
        public string LinkedOrFixed { get; set; }
        public string ResidualAmt { get; set; }
        public string InsurancePmt { get; set; }
        public string InsCredLife { get; set; }
        public string TtlMthlyInslmt { get; set; }
        public string AgreementTerm { get; set; }
        public string ExtrasAmount { get; set; }
        public string ApprovedDeposit { get; set; }
        public string ApprovedRate { get; set; }
        public string KMLimit { get; set; }
        public string InstalmentAmount { get; set; }
        public string CreditLife { get; set; }
        public string ServiceFee { get; set; }
        public string PodiumNumber { get; set; }
        public string DateAndTime { get; set; }
    }
}
