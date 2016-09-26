using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string Login { get; set; }
		public string PasswordHash { get; set; }

		public Guid GroupId { get; set; }

		public virtual Person Person { get; set; }
		public virtual Group Group { get; set; }
		public virtual List<UsersRights> UsersRights { get; set; }
	}
}