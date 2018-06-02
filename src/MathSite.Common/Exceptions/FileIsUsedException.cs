using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
    [Serializable]
    public class FileIsUsedException : ApplicationException
    {
        public FileIsUsedException()
        {
        }

        protected FileIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public FileIsUsedException(string message) : base(message)
        {
        }

        public FileIsUsedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}