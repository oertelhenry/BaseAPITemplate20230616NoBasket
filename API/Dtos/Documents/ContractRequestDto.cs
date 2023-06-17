using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobalyz.Domain.Odyssey.Models
{
    public class ContractRequestDto
    {
        public string? Category { get; set; } = "cata";

        //[JsonIgnore]
        public int ContractNumber { get; set; }

        [JsonIgnore]
        public string? DateAndTime { get; set; } = "";

        public Page1 page1 { get; set; }
        public Page2 page2 { get; set; }
        public Page7 page7 { get; set; }
        public Page9 page9 { get; set; }

        public string? AodSupplierName { get; set; } = "";
        public string? AodSupplierAddress { get; set; } = "";
        public string? AodSupplierContactNumber { get; set; } = "";
        public string? AodSupplierContactPerson { get; set; } = "";

        public List<ContractVaps> vaps { get; set; }

        public ContractApplication contractPack { get; set; }
        public IncomeAndExpenses IncomeAndExpenses { get; set; }
    }

    public class Page1
    {
        public string? ClientName { get; set; } = "";
        public string? ClientIdNumber { get; set; } = "";
        public string? DealAccountNumber { get; set; } = "";
        public string? ClientStreetAddress { get; set; } = "";
        public string? ClientStreetAddress1 { get; set; } = "";
        public string? ClientEmailAddress { get; set; } = "";
        public string? VatNumber { get; set; } = "";
        public string? ContractStartDate { get; set; } = "";
        public string? MakeAndModel { get; set; } = "";
        public string? YearOfFirstReg { get; set; } = "";
        public string? DealerName { get; set; } = "";
        public string? VinNumber { get; set; } = "";
        public string? EngineNumber { get; set; } = "";
        public string? RegNumber { get; set; } = "";
        public string? PBCreditAdvanced { get; set; } = "";
        public string? PBInitiationFee { get; set; } = "";
        public string? PBTotalAddCharg { get; set; } = "";
        public string? PBDeductDeposit { get; set; } = "";
        public string? PBAmountAdvanc { get; set; } = "";
        public string? PCPrincipalDebtInst { get; set; } = "";
        public string? PCInitiationFee { get; set; } = "";
        public string? PCNumberOfInst { get; set; } = "";
        public string? PCAccFrequency { get; set; } = "";
        public string? PCInsuranceP { get; set; } = "";
        public string? PCInsuranceDesc { get; set; } = "";
        public string? PCCurSymb { get; set; } = "";
        public string? PCInstalmentPay { get; set; } = "";
        public string? PCFinalBalloon { get; set; } = "";
        public string? PDPrincipalDebt { get; set; } = "";
        public string? PDInterestOverT { get; set; } = "";
        public string? PDTotalServiceF { get; set; } = "";
        public string? PDTotalCost { get; set; } = "";
        public string? PETrackingUnit { get; set; } = "";
        public string? PEExtendedWar { get; set; } = "";
        public string? PETotalAddition { get; set; } = "";
    }

    public class Page2
    {
        public string? PFPaymentMet { get; set; } = "";
        public string? PFFstInstDate { get; set; } = "";
        public string? PFPmtDate { get; set; } = "";
        public string? PFLstInstDate { get; set; } = "";
        public string? PFBalLstDate { get; set; } = "";
        public string? AnualIntRate { get; set; } = "";
        public string? LinkedToPrime { get; set; } = "";
        public string? MonthlyAmt { get; set; } = "";
        public string? TelemarketConsent { get; set; } = "";
        public string? ThirdPConsent { get; set; } = "";
        public string? MediaComsConsent { get; set; } = "";
        public string? ClientTitle { get; set; } = "";
        public string? FinManIdNumber { get; set; } = "";
        public string? FspNumber { get; set; } = "";

        public string? AppRatioTotCostPrinDebt { get; set; } = "";
        public string? CatBIntRateBefRw { get; set; } = "";
        public string? CatBIntRateAftRw { get; set; } = "";
    }

    public class ContractVaps
    {
        public string? VapS { get; set; } = "";
        public string? VapC { get; set; } = "";
        public string? VapsAmt { get; set; } = "";

        [JsonIgnore]
        public int ContractId { get; set; } = 0;
    }



    public class Page7
    {
        public string? ComencementDate { get; set; } = "";
        public string? CreditAgreementNumber { get; set; } = "";
        public string? CleintBank { get; set; } = "";
        public string? BankBranch { get; set; } = "";
        public string? BankCode { get; set; } = "";
        public string? AccountNumber { get; set; } = "";
        public string? AccountType { get; set; } = "";
        public string? XDebit { get; set; } = "";
    }

    public class Page9
    {
        public string? IdNumber { get; set; } = "";
        public string? HomeAddress1 { get; set; } = "";
        public string? HomeAddress2 { get; set; } = "";
        public string? ContactNumber { get; set; } = "";
        public string? CreditProvider { get; set; } = "";
        public string? DealAccountNr { get; set; } = "";
        public string? InsuranceCompany { get; set; } = "";
        public string? InsAddress1 { get; set; } = "";
        public string? InsAddress2 { get; set; } = "";
        public string? InsAddress3 { get; set; } = "";
        public string? InsAddress4 { get; set; } = "";
        public string? InsAddress5 { get; set; } = "";
        public string? InsAddress6 { get; set; } = "";
        public string? PostalCode { get; set; } = "";
        public string? TelNumber { get; set; } = "";
        public string? BrokerName { get; set; } = "";
        public string? PolRefNumber { get; set; } = "";
        public string? mvi { get; set; } = "";
        public string? MviStartDate { get; set; } = "";
        public string? MviEndDate { get; set; } = "";
        public string? ew { get; set; } = "";
        public string? EwStartDate { get; set; } = "";
        public string? EwEndDate { get; set; } = "";
        public string? cl { get; set; } = "";
        public string? ClStartDate { get; set; } = "";
        public string? ClEndDate { get; set; } = "";
        public string? os { get; set; } = "";
        public string? OsStartDate { get; set; } = "";
        public string? OsEndDate { get; set; } = "";
    }

    public class ContractApplication
    {
        public string? FinInitFee { get; set; } = "";
        public string? NewOrUsed { get; set; } = "";
        public string? DealerCode { get; set; } = "";
        public string? SuplierCode { get; set; } = "";
        public string? SellingPrice { get; set; } = "";
        public string? MandMCode { get; set; } = "";
        public string? DateOfBirth { get; set; } = "";
        public string? ClientGender { get; set; } = "";
        public string? ClientFirstName { get; set; } = "";
        public string? ClientSurName { get; set; } = "";
        public string? ClientCellNumber { get; set; } = "";
        public string? ClientHomeNumber { get; set; } = "";
        public string? ClientWorkNumber { get; set; } = "";
        public string? ClientStreetAddressApp { get; set; } = "";
        public string? ClientSuburb { get; set; } = "";
        public string? ClientPoCode { get; set; } = "";
        public string? ClientProvince { get; set; } = "";
        public string? ResidYear { get; set; } = "";
        public string? ResidMonth { get; set; } = "";
        public string? PoNumber { get; set; } = "";
        public string? PvtBagNumber { get; set; } = "";
        public string? PoSuburb { get; set; } = "";
        public string? HouseNr { get; set; } = "";
        public string? PoStreet { get; set; } = "";
        public string? PoPostCode { get; set; } = "";
        public string? ClientMaritalStatus { get; set; } = "";
        public string? KinName { get; set; } = "";
        public string? KinRelation { get; set; } = "";
        public string? KinCellNumber { get; set; } = "";
        public string? KinHomeNumber { get; set; } = "";
        public string? KinAddress { get; set; } = "";
        public string? KinWorkNumber { get; set; } = "";
        public string? PresentEmployer { get; set; } = "";
        public string? PresOccupation { get; set; } = "";
        public string? YearsEmploy { get; set; } = "";
        public string? PipOfficial { get; set; } = "";
        public string? PipRelated { get; set; } = "";
        public string? PipRelationship { get; set; } = "";
        public string? PipName { get; set; } = "";
        public string? PipSurname { get; set; } = "";
    }

    public class IncomeAndExpenses
    {
        public string? BscSal { get; set; } = "";
        public string? CarAll { get; set; } = "";
        public string? ComSal { get; set; } = "";
        public string? OverSal { get; set; } = "";
        public string? OthSal { get; set; } = "";
        public string? SrcOthIn { get; set; } = "";
        public string? TotSal { get; set; } = "";
        public string? NetSal { get; set; } = "";
        public string? TotMthI { get; set; } = "";
        public string? BasSalSp { get; set; } = "";
        public string? CarAllSp { get; set; } = "";
        public string? ComSalSp { get; set; } = "";
        public string? OverSalSp { get; set; } = "";
        public string? OthSalSp { get; set; } = "";
        public string? SrcOthInSp { get; set; } = "";
        public string? TotSalSp { get; set; } = "";
        public string? NetSalSp { get; set; } = "";
        public string? TotMthISp { get; set; } = "";


        public string? BndPaym { get; set; } = "";
        public string? VehInst { get; set; } = "";
        public string? CredCrd { get; set; } = "";
        public string? CloAccn { get; set; } = "";
        public string? PolInsPre { get; set; } = "";
        public string? TraCost { get; set; } = "";
        public string? EduCost { get; set; } = "";
        public string? PropRent { get; set; } = "";
        public string? RatElect { get; set; } = "";
        public string? PersLoaR { get; set; } = "";
        public string? FurnAcco { get; set; } = "";
        public string? OverdPay { get; set; } = "";
        public string? TelePay { get; set; } = "";
        public string? FoodEnt { get; set; } = "";
        public string? MaintAmt { get; set; } = "";
        public string? OthAmt { get; set; } = "";
        public string? TotHsExp { get; set; } = "";
        public string? TotMonInc { get; set; } = "";
        public string? LesTotMont { get; set; } = "";
        public string? TotMthDis { get; set; } = "";
        public string? C1Y { get; set; } = "";
        public string? C1N { get; set; } = "";
        public string? C2Y { get; set; } = "";
        public string? C2N { get; set; } = "";
        public string? C3Y { get; set; } = "";
        public string? C3N { get; set; } = "";
    }
}