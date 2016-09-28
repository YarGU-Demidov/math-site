using System;

namespace MathSite.Models
{
	public class UsersRights
	{
		public Guid Id { get; set; }
		public bool Value { get; set; }
		
		public Guid UserId { get; set; }
		public Guid RightId { get; set; }

		public virtual User User { get; set; }
		public virtual Right Right { get; set; }
	}
}