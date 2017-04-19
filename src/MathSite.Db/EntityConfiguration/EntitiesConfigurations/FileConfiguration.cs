using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class FileConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<File>()
				.HasKey(f => f.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<File>()
				.Property(f => f.FileName)
				.IsRequired();
			modelBuilder.Entity<File>()
				.Property(f => f.DateAdded)
				.IsRequired();
			modelBuilder.Entity<File>()
				.Property(f => f.FilePath)
				.IsRequired();
			modelBuilder.Entity<File>()
				.Property(f => f.Extension)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<File>()
				.HasOne(file => file.Person)
				.WithOne(person => person.Photo)
				.HasForeignKey<Person>(person => person.PhotoId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<File>()
				.HasMany(file => file.PostSettings)
				.WithOne(postSettings => postSettings.PreviewImage)
				.HasForeignKey(postSettings => postSettings.PreviewImageId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<File>()
				.HasMany(file => file.PostAttachments)
				.WithOne(postAttachments => postAttachments.File)
				.HasForeignKey(postAttachments => postAttachments.FileId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "File";
	}
}