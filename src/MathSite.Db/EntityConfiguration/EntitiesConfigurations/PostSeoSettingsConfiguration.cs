using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class PostSeoSettingsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostSeoSettings>()
				.HasKey(postSeoSettings => postSeoSettings.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostSeoSettings>()
				.Property(postSeoSettings => postSeoSettings.Url)
				.IsRequired(false);

			modelBuilder.Entity<PostSeoSettings>()
				.Property(postSeoSettings => postSeoSettings.Title)
				.IsRequired(false);

			modelBuilder.Entity<PostSeoSettings>()
				.Property(postSeoSettings => postSeoSettings.Description)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PostSeoSettings>()
				.HasOne(postSeoSettings => postSeoSettings.Post)
				.WithOne(post => post.PostSeoSettings)
				.HasForeignKey<Post>(post => post.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<PostSeoSettings>()
				.HasMany(postSeoSettings => postSeoSettings.PostKeywords)
				.WithOne(postKeywords => postKeywords.PostSeoSettings)
				.HasForeignKey(postSeoSettings => postSeoSettings.PostSeoSettingsId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "PostSeoSettings";
	}
}