using System;
using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class DirectoryConfiguration : AbstractEntityConfiguration<Directory>
    {
        protected override string TableName { get; } = nameof(Directory);

        protected override void SetFields(EntityTypeBuilder<Directory> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder.Property(d => d.RootDirectoryId)
                .IsRequired(false);
        }

        protected override void SetRelationships(EntityTypeBuilder<Directory> modelBuilder)
        {
            base.SetRelationships(modelBuilder); 

            modelBuilder.HasMany(d => d.Directories)
                .WithOne(d => d.RootDirectory)
                .HasForeignKey(d => d.RootDirectoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasMany(d => d.Files)
                .WithOne(f => f.Directory)
                .HasForeignKey(f => f.DirectoryId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}