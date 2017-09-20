using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class PostKeywordsConfiguration : AbstractEntityConfiguration<PostKeyword>
    {
        protected override string TableName { get; } = nameof(PostKeyword);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<PostKeyword> modelBuilder)
        {
            modelBuilder
                .HasKey(postKeywords => postKeywords.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<PostKeyword> modelBuilder)
        {
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<PostKeyword> modelBuilder)
        {
            modelBuilder
                .HasOne(postKeywords => postKeywords.Keyword)
                .WithMany(post => post.Posts)
                .HasForeignKey(postSeoSettings => postSeoSettings.KeywordId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasOne(postKeywords => postKeywords.PostSeoSettings)
                .WithMany(postSeoSettings => postSeoSettings.PostKeywords)
                .HasForeignKey(postKeywords => postKeywords.PostSeoSettingsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<PostKeyword> modelBuilder)
        {
        }
    }
}