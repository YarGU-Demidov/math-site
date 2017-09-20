using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    /// <inheritdoc />
    public class FileConfiguration : AbstractEntityConfiguration<File>
    {
        protected override string TableName { get; } = nameof(File);

        /// <inheritdoc />
        protected override void SetKeys(EntityTypeBuilder<File> modelBuilder)
        {
            modelBuilder
                .HasKey(f => f.Id);
        }

        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<File> modelBuilder)
        {
            modelBuilder
                .Property(f => f.FileName)
                .IsRequired();
            modelBuilder
                .Property(f => f.DateAdded)
                .IsRequired();
            modelBuilder
                .Property(f => f.FilePath)
                .IsRequired();
            modelBuilder
                .Property(f => f.Extension)
                .IsRequired(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<File> modelBuilder)
        {
            modelBuilder
                .HasOne(file => file.Person)
                .WithOne(person => person.Photo)
                .HasForeignKey<Person>(person => person.PhotoId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(file => file.PostSettings)
                .WithOne(postSettings => postSettings.PreviewImage)
                .HasForeignKey(postSettings => postSettings.PreviewImageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .HasMany(file => file.PostAttachments)
                .WithOne(postAttachments => postAttachments.File)
                .HasForeignKey(postAttachments => postAttachments.FileId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void SetIndexes(EntityTypeBuilder<File> modelBuilder)
        {
        }
    }
}