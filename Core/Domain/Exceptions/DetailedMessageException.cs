using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace mobalyz.ErrorHandling
{
    [Serializable]
    public class DetailedMessageException : Exception, IExceptionContainsAdditionalMessages
    {
        public DetailedMessageException()
            : base("An error occurred while processing your request")
        {
            this.AdditionalMessages = new List<string>();
        }

        public DetailedMessageException(string message, IList<string> additionalMessages)
            : base(message)
        {
            this.AdditionalMessages = additionalMessages;
        }

        public DetailedMessageException(string message, Exception innerException, IList<string> additionalMessages)
            : base(message, innerException)
        {
            this.AdditionalMessages = additionalMessages;
        }

        public DetailedMessageException(IList<string> additionalMessages)
            : base("An error occurred while processing your request")
        {
            this.AdditionalMessages = additionalMessages;
        }

        protected DetailedMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public IList<string> AdditionalMessages { get; set; }
    }
}