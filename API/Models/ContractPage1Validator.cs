//using FluentValidation;
//using Mobalyz.Domain.Odyssey.Models;

//namespace Mobalyz.Domain.Odyssey.Validation
//{
//    public class ContractPage1Validator : AbstractValidator<Page1>
//    {
//        public ContractPage1Validator()
//        {
//            RuleSet("name", () =>
//            {
//                RuleFor(x => x.ClientName).NotNull().MinimumLength(10).WithMessage("name could not be null");
//            });

//        }
//    }
//}