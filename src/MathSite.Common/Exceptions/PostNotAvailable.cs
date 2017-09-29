using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
    [Serializable]
    public class PostNotAvailable : ApplicationException
    {
        public PostNotAvailable()
        {
        }

        protected PostNotAvailable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PostNotAvailable(string message) : base(message)
        {
        }

        public PostNotAvailable(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}