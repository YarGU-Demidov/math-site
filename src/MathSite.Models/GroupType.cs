using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class GroupType
	{
		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string Alias { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<Group> Groups { get; set; }
	}
}