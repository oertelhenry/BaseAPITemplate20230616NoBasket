//using Microsoft.VisualBasic;
//using Mobalyz.Domain.Odyssey.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Mobalyz.Odyssey.Resources.Data.Models
//{
//    public class ContractValues
//    {
//        public ContractValues(ContractRequestDto request)
//        {
//            DateTime nowDate = DateTime.Now;
//            var signedDay = nowDate.Day.ToString();
//            var signedMonth = nowDate.ToString("MMMM");
//            var signedYear = nowDate.Year.ToString();

//            AodSupplierName = request.AodSupplierName == null ? "" : request.AodSupplierName;
//            AodSupplierAddress = request.AodSupplierAddress == null ? "" : request.AodSupplierAddress;
//            AodSupplierContactNumber = request.AodSupplierContactNumber == null ? "" : request.AodSupplierContactNumber;
//            AodSupplierContactPerson = request.AodSupplierContactPerson == null ? "" : request.AodSupplierContactPerson;
//            ContractNumber = request.ContractNumber.ToString() == null ? "" : request.ContractNumber.ToString();
//            Category = request.Category == null ? "" : request.Category;
//            SignedDate = signedDay + " " + signedMonth;
//            SignedYear = signedYear;
//            DateAndTime = DateTime.Now.ToString();

//            ClientName = request.page1.ClientName == null ? "" : request.page1.ClientName;
//            ClientIdNumber = request.page1.ClientIdNumber == null ? "" : request.page1.ClientIdNumber;
//            DealAccountNumber = request.page1.DealAccountNumber == null ? "" : request.page1.DealAccountNumber;
//            ClientStreetAddress1 = request.page1.ClientStreetAddress1 == null ? "" : request.page1.ClientStreetAddress1;
//            ClientStreetAddress = request.page1.ClientStreetAddress == null ? "" : request.page1.ClientStreetAddress;
//            ClientEmailAddress = request.page1.ClientEmailAddress == null ? "" : request.page1.ClientEmailAddress;
//            VatNumber = request.page1.VatNumber == null ? "" : request.page1.VatNumber;
//            ContractStartDate = request.page1.ContractStartDate == null ? "" : request.page1.ContractStartDate;
//            MakeAndModel = request.page1.MakeAndModel == null ? "" : request.page1.MakeAndModel;
//            YearOfFirstReg = request.page1.YearOfFirstReg == null ? "" : request.page1.YearOfFirstReg;
//            DealerName = request.page1.DealerName == null ? "" : request.page1.DealerName;
//            VinNumber = request.page1.VinNumber == null ? "" : request.page1.VinNumber;
//            EngineNumber = request.page1.EngineNumber == null ? "" : request.page1.EngineNumber;
//            RegNumber = request.page1.RegNumber == null ? "" : request.page1.RegNumber;
//            PBCreditAdv = request.page1.PBCreditAdvanced == null ? "" : request.page1.PBCreditAdvanced;
//            PBInitiatiFee = request.page1.PBInitiationFee == null ? "" : request.page1.PBInitiationFee;
//            PBToAddCha = request.page1.PBTotalAddCharg == null ? "" : request.page1.PBTotalAddCharg;
//            PBDeductDep = request.page1.PBDeductDeposit == null ? "" : request.page1.PBDeductDeposit;
//            PBAmntAdv = request.page1.PBAmountAdvanc == null ? "" : request.page1.PBAmountAdvanc;
//            PCPrincipalDeb = request.page1.PCPrincipalDebtInst == null ? "" : request.page1.PCPrincipalDebtInst;
//            PCInitiationFee = request.page1.PCInitiationFee == null ? "" : request.page1.PCInitiationFee;
//            PCTerm = request.page1.PCNumberOfInst == null ? "" : request.page1.PCNumberOfInst;
//            PCAccFreque = request.page1.PCAccFrequency == null ? "" : request.page1.PCAccFrequency;
//            PCInstalmentP = request.page1.PCInstalmentPay == null ? "" : request.page1.PCInstalmentPay;
//            PCInsuranceDesc = request.page1.PCInsuranceDesc == null ? " " : request.page1.PCInsuranceDesc;
//            PCCurSymb = request.page1.PCCurSymb == null ? "" : request.page1.PCCurSymb;
//            PCInsuranceP = request.page1.PCInsuranceP == null ? "" : request.page1.PCInsuranceP;
//            PCFinalBalloon = request.page1.PCFinalBalloon == null ? "" : request.page1.PCFinalBalloon;
//            PDPrincipDebt = request.page1.PDPrincipalDebt == null ? "" : request.page1.PDPrincipalDebt;
//            PDInterOverT = request.page1.PDInterestOverT == null ? "" : request.page1.PDInterestOverT;
//            PDTotServF = request.page1.PDTotalServiceF == null ? "" : request.page1.PDTotalServiceF;
//            PDTotalCost = request.page1.PDTotalCost == null ? "" : request.page1.PDTotalCost;
//            PETrackingUnit = request.page1.PETrackingUnit == null ? "" : request.page1.PETrackingUnit;
//            PEExtendedWar = request.page1.PEExtendedWar == null ? "" : request.page1.PEExtendedWar;
//            PETotalAddition = request.page1.PETotalAddition == null ? "" : request.page1.PETotalAddition;

//            PFPaymentMet = request.page2.PFPaymentMet == null ? "" : request.page2.PFPaymentMet;
//            PFFstInstDate = request.page2.PFFstInstDate == null ? "" : request.page2.PFFstInstDate;
//            PFPmtDate = request.page2.PFPmtDate == null ? "" : request.page2.PFPmtDate;
//            PFLstInstDate = request.page2.PFLstInstDate == null ? "" : request.page2.PFLstInstDate;
//            PFBalLstDate = request.page2.PFBalLstDate == null ? "" : request.page2.PFBalLstDate;
//            AnualIntRate = request.page2.AnualIntRate == null ? "" : request.page2.AnualIntRate;
//            LinkedToPrime = request.page2.LinkedToPrime == null ? "" : request.page2.LinkedToPrime;
//            MonthlyAmt = request.page2.MonthlyAmt == null ? "" : request.page2.MonthlyAmt;
//            TX = request.page2.TelemarketConsent == null ? "" : request.page2.TelemarketConsent;
//            DX = request.page2.ThirdPConsent == null ? "" : request.page2.ThirdPConsent;
//            CX = request.page2.MediaComsConsent == null ? "" : request.page2.MediaComsConsent;
//            ClientTitle = request.page2.ClientTitle == null ? "" : request.page2.ClientTitle;
//            FinManIdNumber = request.page2.FinManIdNumber == null ? "" : request.page2.FinManIdNumber;
//            FspNumber = request.page2.FspNumber == null ? "" : request.page2.FspNumber;
//            CatBIntRateBefRw = request.page2.CatBIntRateBefRw == null ? "" : request.page2.CatBIntRateBefRw;
//            CatBIntRateAftRw = request.page2.CatBIntRateAftRw == null ? "" : request.page2.CatBIntRateAftRw;
//            AppRatioTotCostPrinDebt = request.page2.AppRatioTotCostPrinDebt == null ? "" : request.page2.AppRatioTotCostPrinDebt;


//            PipOfficial = request.contractPack.PipOfficial == null ? "" : request.contractPack.PipOfficial;
//            PipRelated = request.contractPack.PipRelated == null ? "" : request.contractPack.PipRelated;
//            PipRelationship = request.contractPack.PipRelationship == null ? "" : request.contractPack.PipRelationship;
//            PipName = request.contractPack.PipName == null ? "" : request.contractPack.PipName;
//            PipSurname = request.contractPack.PipSurname == null ? "" : request.contractPack.PipSurname;

//            ComencementDate = request.page7.ComencementDate == null ? "" : request.page7.ComencementDate;
//            CreditAgreementNumber = request.page7.CreditAgreementNumber == null ? "" : request.page7.CreditAgreementNumber;
//            ClientBank = request.page7.CleintBank == null ? "" : request.page7.CleintBank;
//            BankBranch = request.page7.BankBranch == null ? "" : request.page7.BankBranch;
//            BankCode = request.page7.BankCode == null ? "" : request.page7.BankCode;
//            AccountNumber = request.page7.AccountNumber == null ? "" : request.page7.AccountNumber;
//            AccountType = request.page7.AccountType == null ? "" : request.page7.AccountType;
//            XDebit = request.page7.XDebit == null ? "" : request.page7.XDebit;

//            IdNumber = request.page9.IdNumber == null ? "" : request.page9.IdNumber;
//            HomeAddress1 = request.page9.HomeAddress1 == null ? "" : request.page9.HomeAddress1;
//            HomeAddress2 = request.page9.HomeAddress2 == null ? "" : request.page9.HomeAddress2;
//            ContactNumber = request.page9.ContactNumber == null ? "" : request.page9.ContactNumber;
//            CreditProvider = request.page9.CreditProvider == null ? "" : request.page9.CreditProvider;
//            DealAccountNr = request.page9.DealAccountNr == null ? "" : request.page9.DealAccountNr;
//            InsuranceCompany = request.page9.InsuranceCompany == null ? "" : request.page9.InsuranceCompany;
//            InsAddress1 = request.page9.InsAddress1 == null ? "" : request.page9.InsAddress1;
//            InsAddress2 = request.page9.InsAddress2 == null ? "" : request.page9.InsAddress2;
//            InsAddress3 = request.page9.InsAddress3 == null ? "" : request.page9.InsAddress3;
//            InsAddress4 = request.page9.InsAddress4 == null ? "" : request.page9.InsAddress4;
//            InsAddress5 = request.page9.InsAddress5 == null ? "" : request.page9.InsAddress5;
//            InsAddress6 = request.page9.InsAddress6 == null ? "" : request.page9.InsAddress6;
//            PostalCode = request.page9.PostalCode == null ? "" : request.page9.PostalCode;
//            BrokerName = request.page9.BrokerName == null ? "" : request.page9.BrokerName;
//            TelNumber = request.page9.TelNumber == null ? "" : request.page9.TelNumber;
//            PolRefNumber = request.page9.PolRefNumber == null ? "" : request.page9.PolRefNumber;
//            mvi = request.page9.mvi == null ? "" : request.page9.mvi;
//            MviStartDate = request.page9.MviStartDate == null ? "" : request.page9.MviStartDate;
//            MviEndDate = request.page9.MviEndDate == null ? "" : request.page9.MviEndDate;
//            ew = request.page9.ew == null ? "" : request.page9.ew;
//            EwStartDate = request.page9.EwStartDate == null ? "" : request.page9.EwStartDate;
//            EwEndDate = request.page9.EwEndDate == null ? "" : request.page9.EwEndDate;
//            cl = request.page9.cl == null ? "" : request.page9.cl;
//            ClStartDate = request.page9.ClStartDate == null ? "" : request.page9.ClStartDate;
//            ClEndDate = request.page9.ClEndDate == null ? "" : request.page9.ClEndDate;
//            os = request.page9.os == null ? "" : request.page9.os;
//            OsStartDate = request.page9.OsStartDate == null ? "" : request.page9.OsStartDate;
//            OsEndDate = request.page9.OsEndDate == null ? "" : request.page9.OsEndDate;

//            FinInitFee = request.contractPack?.FinInitFee == null ? "" : request.contractPack?.FinInitFee;
//            NewOrUsed = request.contractPack?.NewOrUsed == null ? "" : request.contractPack?.NewOrUsed;
//            DealerCode = request.contractPack?.DealerCode == null ? "" : request.contractPack?.DealerCode;
//            SuplierCode = request.contractPack?.SuplierCode == null ? "" : request.contractPack?.SuplierCode;
//            SellingPrice = request.contractPack?.SellingPrice == null ? "" : request.contractPack?.SellingPrice;
//            MandMCode = request.contractPack?.MandMCode == null ? "" : request.contractPack?.MandMCode;
//            DateOfBirth = request.contractPack?.DateOfBirth == null ? "" : request.contractPack?.DateOfBirth;
//            ClientGender = request.contractPack?.ClientGender == null ? "" : request.contractPack?.ClientGender;
//            ClientFirstName = request.contractPack?.ClientFirstName == null ? "" : request.contractPack?.ClientFirstName;
//            ClientSurName = request.contractPack?.ClientSurName == null ? "" : request.contractPack?.ClientSurName;
//            ClientCellNumber = request.contractPack?.ClientCellNumber == null ? "" : request.contractPack?.ClientCellNumber;
//            ClientHomeNumber = request.contractPack?.ClientHomeNumber == null ? "" : request.contractPack?.ClientHomeNumber;
//            ClientWorkNumber = request.contractPack?.ClientWorkNumber == null ? "" : request.contractPack?.ClientWorkNumber;
//            ClientStreetAddressApp = request.contractPack?.ClientStreetAddressApp == null ? "" : request.contractPack?.ClientStreetAddressApp;
//            ClientSuburb = request.contractPack?.ClientSuburb == null ? "" : request.contractPack?.ClientSuburb;
//            ClientPoCode = request.contractPack?.ClientPoCode == null ? "" : request.contractPack?.ClientPoCode;
//            ClientProvince = request.contractPack?.ClientProvince == null ? "" : request.contractPack?.ClientProvince;
//            ResidYear = request.contractPack?.ResidYear == null ? "" : request.contractPack?.ResidYear;
//            ResidMonth = request.contractPack?.ResidMonth == null ? "" : request.contractPack?.ResidMonth;
//            PoNumber = request.contractPack?.PoNumber == null ? "" : request.contractPack?.PoNumber;
//            PvtBagNumber = request.contractPack?.PvtBagNumber == null ? "" : request.contractPack?.PvtBagNumber;
//            PoSuburb = request.contractPack?.PoSuburb == null ? "" : request.contractPack?.PoSuburb;
//            HouseNr = request.contractPack?.HouseNr == null ? "" : request.contractPack?.HouseNr;
//            PoStreet = request.contractPack?.PoStreet == null ? "" : request.contractPack?.PoStreet;
//            PoPostCode = request.contractPack?.PoPostCode == null ? "" : request.contractPack?.PoPostCode;
//            ClientMaritalStatus = request.contractPack?.ClientMaritalStatus == null ? "" : request.contractPack?.ClientMaritalStatus;
//            KinName = request.contractPack?.KinName == null ? "" : request.contractPack?.KinName;
//            KinRelation = request.contractPack?.KinRelation == null ? "" : request.contractPack?.KinRelation;
//            KinCellNumber = request.contractPack?.KinCellNumber == null ? "" : request.contractPack?.KinCellNumber;
//            KinHomeNumber = request.contractPack?.KinHomeNumber == null ? "" : request.contractPack?.KinHomeNumber;
//            KinAddress = request.contractPack?.KinAddress == null ? "" : request.contractPack?.KinAddress;
//            KinWorkNumber = request.contractPack?.KinWorkNumber == null ? "" : request.contractPack?.KinWorkNumber;
//            PresentEmployer = request.contractPack?.PresentEmployer == null ? "" : request.contractPack?.PresentEmployer;
//            PresOccupation = request.contractPack?.PresOccupation == null ? "" : request.contractPack?.PresOccupation;
//            YearsEmploy = request.contractPack?.YearsEmploy == null ? "" : request.contractPack?.YearsEmploy;

//            BscSal = request.IncomeAndExpenses?.BscSal == null ? "" : request.IncomeAndExpenses?.BscSal;
//            CarAll = request.IncomeAndExpenses?.CarAll == null ? "" : request.IncomeAndExpenses?.CarAll;
//            ComSal = request.IncomeAndExpenses?.ComSal == null ? "" : request.IncomeAndExpenses?.ComSal;
//            OverSal = request.IncomeAndExpenses?.OverSal == null ? "" : request.IncomeAndExpenses?.OverSal;
//            OthSal = request.IncomeAndExpenses?.OthSal == null ? "" : request.IncomeAndExpenses?.OthSal;
//            SrcOthIn = request.IncomeAndExpenses?.SrcOthIn == null ? "" : request.IncomeAndExpenses?.SrcOthIn;
//            TotSal = request.IncomeAndExpenses?.TotSal == null ? "" : request.IncomeAndExpenses?.TotSal;
//            NetSal = request.IncomeAndExpenses?.NetSal == null ? "" : request.IncomeAndExpenses?.NetSal;
//            TotMthI = request.IncomeAndExpenses?.TotMthI == null ? "" : request.IncomeAndExpenses?.TotMthI;
//            BasSalSp = request.IncomeAndExpenses?.BasSalSp == null ? "" : request.IncomeAndExpenses?.BasSalSp;
//            CarAllSp = request.IncomeAndExpenses?.CarAllSp == null ? "" : request.IncomeAndExpenses?.CarAllSp;
//            ComSalSp = request.IncomeAndExpenses?.ComSalSp == null ? "" : request.IncomeAndExpenses?.ComSalSp;
//            OverSalSp = request.IncomeAndExpenses?.OverSalSp == null ? "" : request.IncomeAndExpenses?.OverSalSp;
//            OthSalSp = request.IncomeAndExpenses?.OthSalSp == null ? "" : request.IncomeAndExpenses?.OthSalSp;
//            SrcOthInSp = request.IncomeAndExpenses?.SrcOthInSp == null ? "" : request.IncomeAndExpenses?.SrcOthInSp;
//            TotSalSp = request.IncomeAndExpenses?.TotSalSp == null ? "" : request.IncomeAndExpenses?.TotSalSp;
//            NetSalSp = request.IncomeAndExpenses?.NetSalSp == null ? "" : request.IncomeAndExpenses?.NetSalSp;
//            TotMthISp = request.IncomeAndExpenses?.TotMthISp == null ? "" : request.IncomeAndExpenses?.TotMthISp;
//            BndPaym = request.IncomeAndExpenses?.BndPaym == null ? "" : request.IncomeAndExpenses?.BndPaym;
//            VehInst = request.IncomeAndExpenses?.VehInst == null ? "" : request.IncomeAndExpenses?.VehInst;
//            CredCrd = request.IncomeAndExpenses?.CredCrd == null ? "" : request.IncomeAndExpenses?.CredCrd;
//            CloAccn = request.IncomeAndExpenses?.CloAccn == null ? "" : request.IncomeAndExpenses?.CloAccn;
//            PolInsPre = request.IncomeAndExpenses?.PolInsPre == null ? "" : request.IncomeAndExpenses?.PolInsPre;
//            TraCost = request.IncomeAndExpenses?.TraCost == null ? "" : request.IncomeAndExpenses?.TraCost;
//            EduCost = request.IncomeAndExpenses?.EduCost == null ? "" : request.IncomeAndExpenses?.EduCost;
//            PropRent = request.IncomeAndExpenses?.PropRent == null ? "" : request.IncomeAndExpenses?.PropRent;
//            RatElect = request.IncomeAndExpenses?.RatElect == null ? "" : request.IncomeAndExpenses?.RatElect;
//            PersLoaR = request.IncomeAndExpenses?.PersLoaR == null ? "" : request.IncomeAndExpenses?.PersLoaR;
//            FurnAcco = request.IncomeAndExpenses?.FurnAcco == null ? "" : request.IncomeAndExpenses?.FurnAcco;
//            OverdPay = request.IncomeAndExpenses?.OverdPay == null ? "" : request.IncomeAndExpenses?.OverdPay;
//            TelePay = request.IncomeAndExpenses?.TelePay == null ? "" : request.IncomeAndExpenses?.TelePay;
//            FoodEnt = request.IncomeAndExpenses?.FoodEnt == null ? "" : request.IncomeAndExpenses?.FoodEnt;
//            MaintAmt = request.IncomeAndExpenses?.MaintAmt == null ? "" : request.IncomeAndExpenses?.MaintAmt;
//            OthAmt = request.IncomeAndExpenses?.OthAmt == null ? "" : request.IncomeAndExpenses?.OthAmt;
//            TotHsExp = request.IncomeAndExpenses?.TotHsExp == null ? "" : request.IncomeAndExpenses?.TotHsExp;
//            TotMonInc = request.IncomeAndExpenses?.TotMonInc == null ? "" : request.IncomeAndExpenses?.TotMonInc;
//            LesTotMont = request.IncomeAndExpenses?.LesTotMont == null ? "" : request.IncomeAndExpenses?.LesTotMont;
//            TotMthDis = request.IncomeAndExpenses?.TotMthDis == null ? "" : request.IncomeAndExpenses?.TotMthDis;
//            C1Y = request.IncomeAndExpenses?.C1Y == null ? "" : request.IncomeAndExpenses?.C1Y;
//            C1N = request.IncomeAndExpenses?.C1N == null ? "" : request.IncomeAndExpenses?.C1N;
//            C2Y = request.IncomeAndExpenses?.C2Y == null ? "" : request.IncomeAndExpenses?.C2Y;
//            C2N = request.IncomeAndExpenses?.C2N == null ? "" : request.IncomeAndExpenses?.C2N;
//            C3Y = request.IncomeAndExpenses?.C3Y == null ? "" : request.IncomeAndExpenses?.C3Y;
//            C3N = request.IncomeAndExpenses?.C3N == null ? "" : request.IncomeAndExpenses?.C3N;

//        }

//        public string ClientName { get; set; }
//        public string DateAndTime { get; set; }
//        public string ClientIdNumber { get; set; }
//            public string DealAccountNumber { get; set; }
//            public string ClientStreetAddress1 { get; set; }
//            public string ClientStreetAddress { get; set; }
//            public string ClientEmailAddress { get; set; }
//            public string VatNumber { get; set; }
//            public string ContractStartDate { get; set; }
//            public string MakeAndModel { get; set; }
//            public string YearOfFirstReg { get; set; }
//            public string DealerName { get; set; }
//            public string VinNumber { get; set; }
//            public string EngineNumber { get; set; }
//            public string RegNumber { get; set; }
//            public string PBCreditAdv { get; set; }
//            public string PBInitiatiFee { get; set; }
//            public string PBToAddCha { get; set; }
//            public string PBDeductDep { get; set; }
//            public string PBAmntAdv { get; set; }
//            public string PCPrincipalDeb { get; set; }
//            public string PCInitiationFee { get; set; }
//            public string PCTerm { get; set; }
//            public string PCAccFreque { get; set; }
//            public string PCInstalmentP { get; set; }
//            public string PCCurSymb { get; set; }
//            public string PCInsuranceDesc { get; set; }
//            public string PCInsuranceP { get; set; }
//            public string PCFinalBalloon { get; set; }
//            public string PDPrincipDebt { get; set; }
//            public string PDInterOverT { get; set; }
//            public string PDTotServF { get; set; }
//            public string PDTotalCost { get; set; }
//            public string PETrackingUnit { get; set; }
//            public string PEExtendedWar { get; set; }
//            public string PETotalAddition { get; set; }
//            public string AodSupplierName { get; set; }
//            public string AodSupplierAddress { get; set; }
//            public string AodSupplierContactNumber { get; set; }
//            public string AodSupplierContactPerson { get; set; }
//            public string PFPaymentMet { get; set; }
//            public string PFFstInstDate { get; set; }
//            public string PFPmtDate { get; set; }
//            public string PFLstInstDate { get; set; }
//            public string PFBalLstDate { get; set; }
//            public string AnualIntRate { get; set; }
//            public string LinkedToPrime { get; set; }
//            public string MonthlyAmt { get; set; }
//            public string TX { get; set; }
//            public string DX { get; set; }
//            public string CX { get; set; }
//            public string ClientTitle { get; set; }
//            public string FinManIdNumber { get; set; }
//            public string FspNumber { get; set; }
//            public string CatBIntRateBefRw { get; set; }
//            public string CatBIntRateAftRw { get; set; }
//            public string AppRatioTotCostPrinDebt { get; set; }
//            public string ComencementDate { get; set; }
//            public string CreditAgreementNumber { get; set; }
//            public string ClientBank { get; set; }
//            public string BankBranch { get; set; }
//            public string BankCode { get; set; }
//            public string AccountNumber { get; set; }
//            public string AccountType { get; set; }
//            public string XDebit { get; set; }
//            public string IdNumber { get; set; }
//            public string HomeAddress1 { get; set; }
//            public string HomeAddress2 { get; set; }
//            public string ContactNumber { get; set; }
//            public string CreditProvider { get; set; }
//            public string DealAccountNr { get; set; }
//            public string InsuranceCompany { get; set; }
//            public string InsAddress1 { get; set; }
//            public string InsAddress2 { get; set; }
//            public string InsAddress3 { get; set; }
//            public string InsAddress4 { get; set; }
//            public string InsAddress5 { get; set; }
//            public string InsAddress6 { get; set; }
//            public string PostalCode { get; set; }
//            public string BrokerName { get; set; }
//            public string TelNumber { get; set; }
//            public string PolRefNumber { get; set; }
//            public string mvi { get; set; }
//            public string MviStartDate { get; set; }
//            public string MviEndDate { get; set; }
//            public string ew { get; set; }
//            public string EwStartDate { get; set; }
//            public string EwEndDate { get; set; }
//            public string cl { get; set; }
//            public string ClStartDate { get; set; }
//            public string ClEndDate { get; set; }
//            public string os { get; set; }
//            public string OsStartDate { get; set; }
//            public string OsEndDate { get; set; }
//            public string ContractNumber { get; set; }
//            public string Category { get; set; }
//            public string SignedDate { get; set; }
//            public string SignedYear { get; set; }

//            public string FinInitFee { get; set; }
//            public string NewOrUsed { get; set; }
//            public string DealerCode { get; set; }
//            public string SuplierCode { get; set; }
//            public string SellingPrice { get; set; }
//            public string MandMCode { get; set; }
//            public string DateOfBirth { get; set; }
//            public string ClientGender { get; set; }
//            public string ClientFirstName { get; set; }
//            public string ClientSurName { get; set; }
//            public string ClientCellNumber { get; set; }
//            public string ClientHomeNumber { get; set; }
//            public string ClientWorkNumber { get; set; }
//            public string ClientStreetAddressApp { get; set; }
//            public string ClientSuburb { get; set; }
//            public string ClientPoCode { get; set; }
//            public string ClientProvince { get; set; }
//            public string ResidYear { get; set; }
//            public string ResidMonth { get; set; }
//            public string PoNumber { get; set; }
//            public string PvtBagNumber { get; set; }
//            public string PoSuburb { get; set; }
//            public string HouseNr { get; set; }
//            public string PoStreet { get; set; }
//            public string PoPostCode { get; set; }
//            public string ClientMaritalStatus { get; set; }
//            public string KinName { get; set; }
//            public string KinRelation { get; set; }
//            public string KinCellNumber { get; set; }
//            public string KinHomeNumber { get; set; }
//            public string KinAddress { get; set; }
//            public string KinWorkNumber { get; set; }
//            public string PresentEmployer { get; set; }
//            public string PresOccupation { get; set; }
//            public string YearsEmploy { get; set; }

//        public string BscSal { get; set; } = "";
//        public string CarAll { get; set; } = "";
//        public string ComSal { get; set; } = "";
//        public string OverSal { get; set; } = "";
//        public string OthSal { get; set; } = "";
//        public string SrcOthIn { get; set; } = "";
//        public string TotSal { get; set; } = "";
//        public string NetSal { get; set; } = "";
//        public string TotMthI { get; set; } = "";
//        public string BasSalSp { get; set; } = "";
//        public string CarAllSp { get; set; } = "";
//        public string ComSalSp { get; set; } = "";
//        public string OverSalSp { get; set; } = "";
//        public string OthSalSp { get; set; } = "";
//        public string SrcOthInSp { get; set; } = "";
//        public string TotSalSp { get; set; } = "";
//        public string NetSalSp { get; set; } = "";
//        public string TotMthISp { get; set; } = "";

//        public string? PipOfficial { get; set; } = "";
//        public string? PipRelated { get; set; } = "";
//        public string? PipRelationship { get; set; } = "";
//        public string? PipName { get; set; } = "";
//        public string? PipSurname { get; set; } = "";


//        public string BndPaym { get; set; } = "";
//        public string VehInst { get; set; } = "";
//        public string CredCrd { get; set; } = "";
//        public string CloAccn { get; set; } = "";
//        public string PolInsPre { get; set; } = "";
//        public string TraCost { get; set; } = "";
//        public string EduCost { get; set; } = "";
//        public string PropRent { get; set; } = "";
//        public string RatElect { get; set; } = "";
//        public string PersLoaR { get; set; } = "";
//        public string FurnAcco { get; set; } = "";
//        public string OverdPay { get; set; } = "";
//        public string TelePay { get; set; } = "";
//        public string FoodEnt { get; set; } = "";
//        public string MaintAmt { get; set; } = "";
//        public string OthAmt { get; set; } = "";
//        public string TotHsExp { get; set; } = "";
//        public string TotMonInc { get; set; } = "";
//        public string LesTotMont { get; set; } = "";
//        public string TotMthDis { get; set; } = "";
//        public string C1Y { get; set; } = "";
//        public string C1N { get; set; } = "";
//        public string C2Y { get; set; } = "";
//        public string C2N { get; set; } = "";
//        public string C3Y { get; set; } = "";
//        public string C3N { get; set; } = "";
//    }
//}