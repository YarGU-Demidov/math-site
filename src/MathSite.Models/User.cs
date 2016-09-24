using System;

namespace MathSite.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string PasswordHash { get; set; }

		public virtual Person Person { get; set; }
	}
}