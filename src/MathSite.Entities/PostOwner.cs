using System;

namespace MathSite.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class PostOwner
	{
		/// <summary>
		///     Идентификатор.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid PostId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Post Post { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public User User { get; set; }
	}
}