using System;

namespace MathSite.Models
{
	public class GroupsRights
	{
		public Guid Id { get; set; }
		public bool Value { get; set; }

		public Guid GroupId { get; set; }
		public Guid RightId { get; set; }

		public virtual Group Group { get; set; }
		public virtual Right Right { get; set; }
	}
}