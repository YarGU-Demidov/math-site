using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
    [Serializable]
    public class NoMorePosts : EntityNotFoundException
    {
        public NoMorePosts()
        {
        }

        public NoMorePosts(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context)
        {
        }

        public NoMorePosts(Type entityType, object id) : base(entityType, id)
        {
        }

        public NoMorePosts(Type entityType, object id, Exception innerException) : base(entityType, id, innerException)
        {
        }

        public NoMorePosts(string message) : base(message)
        {
        }

        public NoMorePosts(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}