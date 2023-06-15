using System;
using System.Runtime.Serialization;

namespace Mobalyz.Data
{
    [System.Serializable]
    public class DatabaseUnavailableException : Exception
    {
        public DatabaseUnavailableException()
        {
        }

        public DatabaseUnavailableException(string message) : base(message)
        {
        }

        public DatabaseUnavailableException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected DatabaseUnavailableException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}