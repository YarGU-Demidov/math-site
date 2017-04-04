using System;

namespace MathSite.Models
{
	/// <summary>
	/// </summary>
	public class UsersRights
	{
		/// <summary>
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// </summary>
		public bool Allowed { get; set; }

		/// <summary>
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// </summary>
		public Guid RightId { get; set; }

		/// <summary>
		/// </summary>
		public Right Right { get; set; }
	}
}