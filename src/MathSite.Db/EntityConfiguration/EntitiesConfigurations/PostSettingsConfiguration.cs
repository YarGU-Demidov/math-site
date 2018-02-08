using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostSettingsConfiguration : AbstractEntityConfiguration<PostSetting>
    {
        protected override string TableName { get; } = nameof(PostSetting);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostSetting> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(postSettings => postSettings.IsCommentsAllowed)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder.Property(setting => setting.Layout)
                .IsRequired(false);

            modelBuilder.Property(setting => setting.EventTime)
                .IsRequired(false);

            modelBuilder.Property(setting => setting.EventLocation)
                .IsRequired(false);

            modelBuilder
                .Property(postSettings => postSettings.CanBeRated)
                .IsRequired()
                .HasDefaultValue(false);

            modelBuilder
                .Property(postSettings => postSettings.PostOnStartPage)
                .IsRequired()
                .HasDefaultValue(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostSetting> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasOne(postSettings => postSettings.PostType)
                .WithOne(postType => postType.DefaultPostsSettings)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(postSettings => postSettings.Post)
                .WithOne(post => post.PostSettings)
                .HasForeignKey<Post>(post => post.PostSettingsId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(postSettings => postSettings.PreviewImage)
                .WithMany(previewImage => previewImage.PostSettings)
                .HasForeignKey(postSettings => postSettings.PreviewImageId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}