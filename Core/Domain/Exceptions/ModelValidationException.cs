using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace mobalyz.ErrorHandling
{
    [Serializable]
    public class ModelValidationException : DetailedMessageException
    {
        public ModelValidationException()
            : base()
        {
        }

        public ModelValidationException(IList<string> messages) : base(messages)
        {
        }

        public ModelValidationException(string message, IList<string> messages) : base(message, messages)
        {
        }

        public ModelValidationException(string message, Exception innerException, IList<string> messages) : base(message, innerException, messages)
        {
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}