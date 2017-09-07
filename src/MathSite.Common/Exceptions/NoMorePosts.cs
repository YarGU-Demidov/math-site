using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
	public class NoMorePosts : ApplicationException
	{
		public NoMorePosts()
		{
		}

		protected NoMorePosts(SerializationInfo info, StreamingContext context) : base(info, context)
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