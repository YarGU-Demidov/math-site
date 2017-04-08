using System;
using System.Collections.Generic;
using System.Text;

namespace MathSite.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class Post
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Excerpt { get; set; }
		public string Content { get; set; }
		public bool Published { get; set; }
		public bool? Deleted { get; set; }
		public DateTime PublishDate { get; set; }
		public Guid? PostTypeId { get; set; }
		public PostType PostType { get; set; }
		public List<PostCategory> PostCategories { get; set; }
		public List<PostOwner> PostOwners { get; set; }
		public List<PostSettings> PostSettings { get; set; }
		public List<PostUserAllowed> UsersAllowed { get; set; }
		public List<PostRating> PostRatings { get; set; }
		public List<Comment> Comments { get; set; }
		public List<PostAttachment> PostAttachments { get; set; }
		public List<PostSeoSettings> PostSeoSettings { get; set; }
		public List<PostGroupsAllowed> GroupsAllowed { get; set; }
	}
}
