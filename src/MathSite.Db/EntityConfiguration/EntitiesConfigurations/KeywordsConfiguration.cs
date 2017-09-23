using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class KeywordsConfiguration : AbstractEntityWithAliasConfiguration<Keyword>
    {
        protected override string TableName { get; } = nameof(Keyword);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<Keyword> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(keyword => keyword.Name)
                .IsRequired();
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<Keyword> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(keyword => keyword.Posts)
                .WithOne(posts => posts.Keyword)
                .HasForeignKey(keyword => keyword.KeywordId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}