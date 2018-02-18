using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
    [Serializable]
    public class PersonIsUsedException : ApplicationException
    {
        public PersonIsUsedException()
        {
        }

        protected PersonIsUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PersonIsUsedException(string message) : base(message)
        {
        }

        public PersonIsUsedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}