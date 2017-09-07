using System;
using System.Runtime.Serialization;

namespace MathSite.Common.Exceptions
{
	public class PostNotFoundException : ApplicationException
	{
		public PostNotFoundException()
		{
		}

		protected PostNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public PostNotFoundException(string message) : base(message)
		{
		}

		public PostNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}