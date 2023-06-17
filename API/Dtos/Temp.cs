namespace Mobalyz.Domain.Models
{
    public class Temp
    {
        public string idNumber { get; set; }
        public string NLR_ActiveAccounts { get; set; }
        public string NLR_BalanceExposure { get; set; }
        public string NLR_MonthlyInstallment { get; set; }

        public string NLR_WorstMonthsArrears { get; set; }
        public string NLR_CumulativeArrears { get; set; }
        public string NLR_ClosedAccounts { get; set; }


        public decimal disposableIncome { get; set; }
        public decimal totalVehicleInstallment { get; set; }
        public string applicantName { get; set; }
        public long availableIncome { get; set; }
        public bool isAvailableIncomeLess { get; set; }
        public decimal shortfall { get; set; }
        public long incidentNo { get; set; }
        public DateTime paidOutDate { get; set; }




        public long PayOutIncidentId { get; set; }
        public string ncaMinimumExpense { get; set; }
        public string ncaMaximumIncomeBand { get; set; }
        public string ncaMinimumIncomeBand { get; set; }
        public string ncaMinimumMonthlyFactor { get; set; }
        public string ncaFixedFactorPercentage { get; set; }
        public long BlazeIncidentId { get; set; } = 0;

        public string BlazeRequestXml { get; set; }



        public string CompuInstalment_Amount { get; set; }
        public string CompuAccount_Type { get; set; }
        public string CompuAccount_Type_Desc { get; set; }
    }
}
