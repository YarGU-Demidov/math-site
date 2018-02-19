using System;
using System.Collections.Generic;

namespace MathSite.Entities.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid PostTypeId { get; set; }
        public Guid AuthorId { get; set; }
        public User Author { get; set; }
        public PostType PostType { get; set; }
        public Guid? PostSettingsId { get; set; }
        public PostSetting PostSettings { get; set; }
        public Guid PostSeoSettingsId { get; set; }
        public PostSeoSetting PostSeoSetting { get; set; }
        public IEnumerable<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
        public IEnumerable<PostOwner> PostOwners { get; set; } = new List<PostOwner>();
        public IEnumerable<PostUserAllowed> UsersAllowed { get; set; } = new List<PostUserAllowed>();
        public IEnumerable<PostRating> PostRatings { get; set; } = new List<PostRating>();
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        public IEnumerable<PostAttachment> PostAttachments { get; set; } = new List<PostAttachment>();
        public IEnumerable<PostGroupsAllowed> GroupsAllowed { get; set; } = new List<PostGroupsAllowed>();
    }
}
