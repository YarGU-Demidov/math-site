using System;

namespace MathSite.Models
{
	public class PostCategory
	{
		public Guid Id { get; set; }
		public Guid? CategoryId { get; set; }
		public Category Category { get; set; }
		public Guid? PostId { get; set; }
		public Post Post { get; set; }
	}
}
