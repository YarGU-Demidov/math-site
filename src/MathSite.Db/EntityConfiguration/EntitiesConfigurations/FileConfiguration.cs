using System;
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
        protected override void SetFields(EntityTypeBuilder<File> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(f => f.Name)
                .IsRequired();
            
            modelBuilder
                .Property(f => f.DateAdded)
                .IsRequired();
            
            modelBuilder
                .Property(f => f.Path)
                .IsRequired();
            
            modelBuilder
                .Property(f => f.Extension)
                .IsRequired(false);
            
            modelBuilder.Property(f => f.DirectoryId)
                .IsRequired(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<File> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

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

            modelBuilder
                .HasOne(file => file.Directory)
                .WithMany(directory => directory.Files)
                .HasForeignKey(file => file.DirectoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}