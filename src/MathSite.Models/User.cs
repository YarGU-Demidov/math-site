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
		public Guid PersonId { get; set; }

		public Person Person { get; set; }
		public Group Group { get; set; }
		public List<UsersRights> UsersRights { get; set; }
	}
}