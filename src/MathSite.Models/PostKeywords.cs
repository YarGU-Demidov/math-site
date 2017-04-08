using System;

namespace MathSite.Models
{
	public class PostKeywords
	{
		public Guid Id { get; set; }
		public Guid? KeywordId { get; set; }
		public Keywords Keyword { get; set; }
		public Guid? PostSeoSettingsId { get; set; }
		public PostSeoSettings PostSeoSettings { get; set; }
	}
}
