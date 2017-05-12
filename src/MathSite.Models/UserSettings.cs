using System;

namespace MathSite.Models
{
	/// <summary>
	/// Настройки пользователя.
	/// </summary>
	public class UserSettings
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Namespace { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Value { get; set; }

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