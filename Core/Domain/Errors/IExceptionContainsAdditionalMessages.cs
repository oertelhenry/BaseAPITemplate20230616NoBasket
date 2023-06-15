using System.Collections.Generic;

namespace mobalyz.ErrorHandling
{
    public interface IExceptionContainsAdditionalMessages
    {
        IList<string> AdditionalMessages { get; set; }
    }
}