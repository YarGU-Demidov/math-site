using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	public class PostKeywords
	{
		public Guid Id { get; set; }
		public Guid KeyWordId { get; set; }
		public KeyWord KeyWord { get; set; }
		public List<PostSeoSettings> PostSeoSettings { get; set; }
	}
}
