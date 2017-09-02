using System;

namespace MathSite.Entities
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
		public string RightAlias { get; set; }

		/// <summary>
		/// </summary>
		public Right Right { get; set; }
	}
}