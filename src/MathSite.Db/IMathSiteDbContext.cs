using MathSite.Entities;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db
{
	public interface IMathSiteDbContext : IDbContextBase
	{
		DbSet<Category> Categories { get; set; }
		DbSet<Comment> Comments { get; set; }
		DbSet<File> Files { get; set; }
		DbSet<Group> Groups { get; set; }
		DbSet<GroupsRights> GroupsRights { get; set; }
		DbSet<GroupType> GroupTypes { get; set; }
		DbSet<Keywords> Keywords { get; set; }
		DbSet<Person> Persons { get; set; }
		DbSet<Post> Posts { get; set; }
		DbSet<PostAttachment> PostAttachments { get; set; }
		DbSet<PostCategory> PostCategories { get; set; }
		DbSet<PostGroupsAllowed> PostGroupsAlloweds { get; set; }
		DbSet<PostKeywords> PostKeywords { get; set; }
		DbSet<PostOwner> PostOwners { get; set; }
		DbSet<PostRating> PostRatings { get; set; }
		DbSet<PostSeoSettings> PostSeoSettings { get; set; }
		DbSet<PostSettings> PostSettings { get; set; }
		DbSet<PostType> PostTypes { get; set; }
		DbSet<PostUserAllowed> PostUserAlloweds { get; set; }
		DbSet<Right> Rights { get; set; }
		DbSet<User> Users { get; set; }
		DbSet<UserSettings> UserSettingses { get; set; }
		DbSet<UsersRights> UsersRights { get; set; }
	}
}