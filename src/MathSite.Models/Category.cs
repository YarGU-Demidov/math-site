using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class Category
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
		public List<PostCategory> PostCategories { get; set; }
	}
}
