using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
    public class CategoryDoesNotExists : EntityNotFoundException
    {
        public CategoryDoesNotExists()
        {
        }

        public CategoryDoesNotExists(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public CategoryDoesNotExists(Type entityType, object id) : base(entityType, id)
        {
        }

        public CategoryDoesNotExists(Type entityType, object id, Exception innerException) : base(entityType, id, innerException)
        {
        }

        public CategoryDoesNotExists(string message) : base(message)
        {
        }

        public CategoryDoesNotExists(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}