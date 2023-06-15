using mobalyz.ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mobalyz.ErrorHandling
{
    [System.Serializable]
    public class ValidationsException : Exception
    {
        public ValidationsException(IList<ValidationResult> validationResults)
        {
            this.ValidationResults = validationResults;
        }

        public IList<ValidationResult> ValidationResults { get; private set; }

        public virtual void ThrowEnhancedValidationException(ValidationsException e)
        {
            var errorMessages = e.ValidationResults
                    .Select(x => x.ErrorMessage);

            throw new ModelValidationException(e.Message, errorMessages.ToList());
        }

    }
}