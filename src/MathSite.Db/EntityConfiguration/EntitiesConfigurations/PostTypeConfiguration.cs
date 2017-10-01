using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class PostTypeConfiguration : AbstractEntityWithAliasConfiguration<PostType>
    {
        protected override string TableName { get; } = nameof(PostType);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostType> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(postType => postType.Name)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostType> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(postType => postType.Posts)
                .WithOne(posts => posts.PostType)
                .HasForeignKey(postType => postType.PostTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(postType => postType.DefaultPostsSettings)
                .WithOne(postSetting => postSetting.PostType)
                .HasForeignKey<PostType>(postSetting => postSetting.DefaultPostsSettingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}