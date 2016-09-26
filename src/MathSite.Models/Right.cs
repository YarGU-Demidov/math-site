using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	public class Right
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual List<GroupsRights> GroupsRights { get; set; }
		public virtual List<UsersRights> UsersRights { get; set; }
	}
}