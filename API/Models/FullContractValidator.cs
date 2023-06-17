using FluentValidation;
using Mobalyz.Domain.Odyssey.Models;

namespace Mobalyz.Domain.Odyssey.Validation
{
    public class FullContractValidator : AbstractValidator<ContractRequestDto>
    {
        public FullContractValidator()
        {
            RuleSet("name", () =>
            {
                RuleFor(x => x.Category).NotNull().MinimumLength(2).WithMessage("Category is not valid");
                RuleFor(x => x.page1.ClientName).NotNull().MinimumLength(2).WithMessage("ClientName is not valid");
                RuleFor(x => x.page1.ClientIdNumber).NotNull().MinimumLength(13).MaximumLength(13).WithMessage("ClientIdNumber is not valid");
                RuleFor(x => x.page1.DealAccountNumber).NotNull().MinimumLength(2).WithMessage("DealAccountNumber is not valid");
                RuleFor(x => x.page1.ClientStreetAddress).NotNull().MinimumLength(2).MaximumLength(35).WithMessage("ClientStreetAddress is not valid max 30");
                RuleFor(x => x.page1.ClientStreetAddress1).NotNull().MinimumLength(2).MaximumLength(35).WithMessage("ClientStreetAddress1 is not valid max 30");
                RuleFor(x => x.page1.ClientEmailAddress).NotNull().MinimumLength(2).WithMessage("ClientEmailAddress is not valid");
                RuleFor(x => x.page1.VatNumber).NotNull().MinimumLength(2).WithMessage("VatNumber is not valid");
                RuleFor(x => x.page1.ContractStartDate).NotNull().MinimumLength(2).WithMessage("ContractStartDate is not valid");
                RuleFor(x => x.page1.MakeAndModel).NotNull().MinimumLength(2).WithMessage("MakeAndModel is not valid");
                RuleFor(x => x.page1.YearOfFirstReg).NotNull().MinimumLength(2).WithMessage("YearOfFirstReg is not valid");
                RuleFor(x => x.page1.DealerName).NotNull().MinimumLength(2).WithMessage("DealerName is not valid");
                RuleFor(x => x.page1.VinNumber).NotNull().MinimumLength(2).WithMessage("VinNumber is not valid");
                RuleFor(x => x.page1.EngineNumber).NotNull().MinimumLength(2).WithMessage("EngineNumber is not valid");
                //RuleFor(x => x.page1.RegNumber).NotNull().MinimumLength(2).WithMessage("RegNumber is not valid");
                RuleFor(x => x.page1.PBCreditAdvanced).NotNull().MinimumLength(2).WithMessage("PBCreditAdvanced is not valid");
                RuleFor(x => x.page1.PBInitiationFee).NotNull().MinimumLength(2).WithMessage("PBInitiationFee is not valid");
                RuleFor(x => x.page1.PBTotalAddCharg).NotNull().MinimumLength(2).WithMessage("PBTotalAddCharg is not valid");
                RuleFor(x => x.page1.PBDeductDeposit).NotNull().MinimumLength(2).WithMessage("PBDeductDeposit is not valid");
                RuleFor(x => x.page1.PBAmountAdvanc).NotNull().MinimumLength(2).WithMessage("PBAmountAdvanc is not valid");
                RuleFor(x => x.page1.PCPrincipalDebtInst).NotNull().MinimumLength(2).WithMessage("PCPrincipalDebtInst is not valid");
                RuleFor(x => x.page1.PCInitiationFee).NotNull().MinimumLength(2).WithMessage("PCInitiationFee is not valid");
                RuleFor(x => x.page1.PCNumberOfInst).NotNull().MinimumLength(2).WithMessage("PCNumberOfInst is not valid");
                RuleFor(x => x.page1.PCAccFrequency).NotNull().MinimumLength(2).WithMessage("PCAccFrequency is not valid");
                RuleFor(x => x.page1.PCInstalmentPay).NotNull().MinimumLength(2).WithMessage("PCInstalmentPay is not valid");
                //RuleFor(x => x.page1.PCFinalBalloon).NotNull().MinimumLength(2).WithMessage("PCFinalBalloon is not valid");
                RuleFor(x => x.page1.PDPrincipalDebt).NotNull().MinimumLength(2).WithMessage("PDPrincipalDebt is not valid");
                RuleFor(x => x.page1.PDInterestOverT).NotNull().MinimumLength(2).WithMessage("PDInterestOverT is not valid");
                RuleFor(x => x.page1.PDTotalServiceF).NotNull().MinimumLength(2).WithMessage("PDTotalServiceF is not valid");
                RuleFor(x => x.page1.PDTotalCost).NotNull().MinimumLength(2).WithMessage("PDTotalCost is not valid");
                //RuleFor(x => x.page1.PETrackingUnit).NotNull().MinimumLength(2).WithMessage("PETrackingUnit is not valid");
                //RuleFor(x => x.page1.PEExtendedWar).NotNull().MinimumLength(2).WithMessage("PEExtendedWar is not valid");
                //RuleFor(x => x.page1.PETotalAddition).NotNull().MinimumLength(2).WithMessage("PETotalAddition is not valid");

                RuleFor(x => x.page2.PFPaymentMet).NotNull().MinimumLength(2).WithMessage("PFPaymentMet is not valid");
                RuleFor(x => x.page2.PFFstInstDate).NotNull().MinimumLength(2).WithMessage("PFFstInstDate is not valid");
                RuleFor(x => x.page2.PFPmtDate).NotNull().MinimumLength(2).WithMessage("PFPmtDate is not valid");
                RuleFor(x => x.page2.PFLstInstDate).NotNull().MinimumLength(2).WithMessage("PFLstInstDate is not valid");
                RuleFor(x => x.page2.PFBalLstDate).NotNull().MinimumLength(2).WithMessage("PFBalLstDate is not valid");
                RuleFor(x => x.page2.AnualIntRate).NotNull().MinimumLength(2).WithMessage("AnualIntRate is not valid");
                RuleFor(x => x.page2.LinkedToPrime).NotNull().MinimumLength(2).WithMessage("LinkedToPrime is not valid");
                RuleFor(x => x.page2.MonthlyAmt).NotNull().MinimumLength(2).WithMessage("MonthlyAmt is not valid");
                //RuleFor(x => x.page2.TelemarketConsent).NotNull().MinimumLength(2).WithMessage("TelemarketConsent is not valid");
                //RuleFor(x => x.page2.ThirdPConsent).NotNull().MinimumLength(2).WithMessage("ThirdPConsent is not valid");
                //RuleFor(x => x.page2.MediaComsConsent).NotNull().MinimumLength(2).WithMessage("MediaComsConsent is not valid");
                RuleFor(x => x.page2.ClientTitle).NotNull().MinimumLength(1).WithMessage("ClientTitle is not valid");
                RuleFor(x => x.page2.FinManIdNumber).NotNull().MinimumLength(2).WithMessage("FinManIdNumber is not valid");
                RuleFor(x => x.page2.FspNumber).NotNull().MinimumLength(2).WithMessage("FspNumber is not valid");
                RuleFor(x => x.page2.AppRatioTotCostPrinDebt).NotNull().WithMessage("AppRatioTotCostPrinDebt is not valid");
                //RuleFor(x => x.page2.CatBIntRateBefRw).NotNull().MinimumLength(2).WithMessage("CatBIntRateBefRw is not valid");
                //RuleFor(x => x.page2.CatBIntRateAftRw).NotNull().MinimumLength(2).WithMessage("CatBIntRateAftRw is not valid");

                RuleFor(x => x.page7.ComencementDate).NotNull().MinimumLength(2).WithMessage("ComencementDate is not valid");
                RuleFor(x => x.page7.CreditAgreementNumber).NotNull().MinimumLength(2).WithMessage("CreditAgreementNumber is not valid");
                RuleFor(x => x.page7.CleintBank).NotNull().MinimumLength(2).WithMessage("CleintBank is not valid");
                RuleFor(x => x.page7.BankBranch).NotNull().MinimumLength(2).WithMessage("BankBranch is not valid");
                RuleFor(x => x.page7.BankCode).NotNull().MinimumLength(2).WithMessage("BankCode is not valid");
                RuleFor(x => x.page7.AccountNumber).NotNull().MinimumLength(2).WithMessage("AccountNumber is not valid");
                RuleFor(x => x.page7.AccountType).NotNull().MinimumLength(2).WithMessage("AccountType is not valid");
                //RuleFor(x => x.page7.XDebit).NotNull().MinimumLength(2).WithMessage("XDebit is not valid");


                //RuleFor(x => x.page9.IdNumber).NotNull().MinimumLength(2).WithMessage("IdNumber is not valid");
                //RuleFor(x => x.page9.HomeAddress1).NotNull().MinimumLength(2).WithMessage("HomeAddress1 is not valid");
                //RuleFor(x => x.page9.HomeAddress2).NotNull().MinimumLength(2).WithMessage("HomeAddress2 is not valid");
                //RuleFor(x => x.page9.ContactNumber).NotNull().MinimumLength(2).WithMessage("ContactNumber is not valid");
                //RuleFor(x => x.page9.CreditProvider).NotNull().MinimumLength(2).WithMessage("CreditProvider is not valid");
                //RuleFor(x => x.page9.DealAccountNr).NotNull().MinimumLength(2).WithMessage("DealAccountNr is not valid");
                //RuleFor(x => x.page9.InsuranceCompany).NotNull().MinimumLength(2).WithMessage("InsuranceCompany is not valid");
                //RuleFor(x => x.page9.InsAddress1).NotNull().MinimumLength(2).WithMessage("InsAddress1 is not valid");
                //RuleFor(x => x.page9.InsAddress2).NotNull().MinimumLength(2).WithMessage("InsAddress2 is not valid");
                //RuleFor(x => x.page9.InsAddress3).NotNull().MinimumLength(2).WithMessage("InsAddress3 is not valid");
                //RuleFor(x => x.page9.InsAddress4).NotNull().MinimumLength(2).WithMessage("InsAddress4 is not valid");
                //RuleFor(x => x.page9.InsAddress5).NotNull().MinimumLength(2).WithMessage("InsAddress5 is not valid");
                //RuleFor(x => x.page9.InsAddress6).NotNull().MinimumLength(2).WithMessage("InsAddress6 is not valid");
                //RuleFor(x => x.page9.PostalCode).NotNull().MinimumLength(2).WithMessage("PostalCode is not valid");
                //RuleFor(x => x.page9.TelNumber).NotNull().MinimumLength(2).WithMessage("TelNumber is not valid");
                //RuleFor(x => x.page9.BrokerName).NotNull().MinimumLength(2).WithMessage("BrokerName is not valid");
                //RuleFor(x => x.page9.PolRefNumber).NotNull().MinimumLength(2).WithMessage("PolRefNumber is not valid");
                //RuleFor(x => x.page9.mvi).NotNull().MinimumLength(2).WithMessage("mvi is not valid");
                //RuleFor(x => x.page9.MviStartDate).NotNull().MinimumLength(2).WithMessage("MviStartDate is not valid");
                //RuleFor(x => x.page9.MviEndDate).NotNull().MinimumLength(2).WithMessage("MviEndDate is not valid");
                //RuleFor(x => x.page9.ew).NotNull().MinimumLength(2).WithMessage("ew is not valid");
                //RuleFor(x => x.page9.EwStartDate).NotNull().MinimumLength(2).WithMessage("EwStartDate is not valid");
                //RuleFor(x => x.page9.EwEndDate).NotNull().MinimumLength(2).WithMessage("EwEndDate is not valid");
                //RuleFor(x => x.page9.cl).NotNull().MinimumLength(2).WithMessage("cl is not valid");
                //RuleFor(x => x.page9.ClStartDate).NotNull().MinimumLength(2).WithMessage("ClStartDate is not valid");
                //RuleFor(x => x.page9.ClEndDate).NotNull().MinimumLength(2).WithMessage("ClEndDate is not valid");
                //RuleFor(x => x.page9.os).NotNull().MinimumLength(2).WithMessage("os is not valid");
                //RuleFor(x => x.page9.OsStartDate).NotNull().MinimumLength(2).WithMessage("OsStartDate is not valid");
                //RuleFor(x => x.page9.OsEndDate).NotNull().MinimumLength(2).WithMessage("OsEndDate is not valid");
            });

        }
    }
}
