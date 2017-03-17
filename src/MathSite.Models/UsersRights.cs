using System;

namespace MathSite.Models
{
	public class UsersRights
	{
		public Guid Id { get; set; }
		public bool Allowed { get; set; }

		public Guid UserId { get; set; }
		public Guid RightId { get; set; }

		public User User { get; set; }
		public Right Right { get; set; }
	}
}