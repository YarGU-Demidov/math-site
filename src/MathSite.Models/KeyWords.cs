using System;
using System.Collections.Generic;

namespace MathSite.Models
{
	public class Keywords
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Alias { get; set; }
		public List<PostKeywords> Posts { get; set; }
	}
}
