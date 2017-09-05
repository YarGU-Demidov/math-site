using System;
using System.Collections.Generic;

namespace MathSite.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class PostType
	{
		public Guid Id { get; set; }
		public string TypeName { get; set; }
		public PostSetting DefaultPostsSettings { get; set; }
		public ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}