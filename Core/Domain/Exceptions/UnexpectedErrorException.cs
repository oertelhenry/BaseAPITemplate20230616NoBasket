using System;
using System.Runtime.Serialization;

namespace mobalyz.ErrorHandling
{
    [Serializable]
    public class UnexpectedErrorException : Exception
    {
        public UnexpectedErrorException()
        {
        }

        public UnexpectedErrorException(string message) : base(message)
        {
        }

        public UnexpectedErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}