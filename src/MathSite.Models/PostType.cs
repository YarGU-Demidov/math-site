using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class PostType
	{
		public Guid Id { get; set; }
		public string TypeName { get; set; }
		public PostSettings DefaultPostsSettings { get; set; }
		public List<Post> Posts { get; set; }
	}
}