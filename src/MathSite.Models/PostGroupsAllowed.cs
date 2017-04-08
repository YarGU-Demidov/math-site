using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class PostGroupsAllowed
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool Allowed { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? PostId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Post Post { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Guid? GroupId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Group Group { get; set; }
	}
}
