using System;

namespace MathSite.Models
{
	public class GroupsRights
	{
		public Guid Id { get; set; }
		public bool Allowed { get; set; }

		public Guid? GroupId { get; set; }
		public Guid? RightId { get; set; }

		public Group Group { get; set; }
		public Right Right { get; set; }
	}
}