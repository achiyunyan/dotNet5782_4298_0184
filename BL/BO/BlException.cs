using System;
using System.Runtime.Serialization;

namespace BL
{
    [Serializable]
    public class BlException : Exception
    {
        public BlException()
        {
        }

        public BlException(string message) : base(message)
        {
        }

        public BlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}