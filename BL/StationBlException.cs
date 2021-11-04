using System;
using System.Runtime.Serialization;

namespace BL
{
    namespace BO
    {
        [Serializable]
        public class StationBlException : Exception
        {
            public StationBlException()
            {
            }

            public StationBlException(string message) : base(message)
            {
            }

            public StationBlException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected StationBlException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}