using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException()
        {
        }

        public AlreadyExistsException(string message) : base(message)
        {
        }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotExistsException : Exception
    {
        public NotExistsException()
        {
        }

        public NotExistsException(string message) : base(message)
        {
        }

        public NotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class XmlFileLoadCreateException : Exception
    {
        private string filePath;
        private string v;
        private Exception ex;

        public XmlFileLoadCreateException()
        {
        }

        public XmlFileLoadCreateException(string message) : base(message)
        {
        }

        public XmlFileLoadCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public XmlFileLoadCreateException(string filePath, string v, Exception ex)
        {
            this.filePath = filePath;
            this.v = v;
            this.ex = ex;
        }

        protected XmlFileLoadCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
