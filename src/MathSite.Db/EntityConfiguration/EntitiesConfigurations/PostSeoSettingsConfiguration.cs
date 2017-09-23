using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostSeoSettingsConfiguration : AbstractEntityConfiguration<PostSeoSetting>
    {
        protected override string TableName { get; } = nameof(PostSeoSetting);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostSeoSetting> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(postSeoSettings => postSeoSettings.Url)
                .IsRequired(false);

            modelBuilder
                .Property(postSeoSettings => postSeoSettings.Title)
                .IsRequired(false);

            modelBuilder
                .Property(postSeoSettings => postSeoSettings.Description)
                .IsRequired(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostSeoSetting> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(postSeoSettings => postSeoSettings.Post)
                .WithOne(post => post.PostSeoSetting)
                .HasForeignKey<Post>(post => post.PostSeoSettingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(postSeoSettings => postSeoSettings.PostKeywords)
                .WithOne(postKeywords => postKeywords.PostSeoSettings)
                .HasForeignKey(postSeoSettings => postSeoSettings.PostSeoSettingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<PostSeoSetting> modelBuilder)
        {
            base.SetIndexes(modelBuilder);

            modelBuilder
                .HasIndex(setting => setting.Url)
                .IsUnique();
        }
    }
}