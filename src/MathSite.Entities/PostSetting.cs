using System;

namespace MathSite.Entities
{
	public class PostSetting
	{
		public Guid Id { get; set; }
		public bool? IsCommentsAllowed { get; set; }
		public bool? CanBeRated { get; set; }
		public bool? PostOnStartPage { get; set; }
		public Post Post { get; set; }
		public PostType PostType { get; set; }
		public Guid? PreviewImageId { get; set; }
		public File PreviewImage { get; set; }
	}
}