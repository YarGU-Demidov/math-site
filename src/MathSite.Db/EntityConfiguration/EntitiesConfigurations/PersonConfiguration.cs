using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class PersonConfiguration : AbstractEntityConfiguration<Person>
	{
		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<Person> modelBuilder)
		{
			modelBuilder
				.HasKey(p => p.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<Person> modelBuilder)
		{
			modelBuilder
				.Property(p => p.Name)
				.IsRequired();

			modelBuilder
				.Property(p => p.Surname)
				.IsRequired();

			modelBuilder
				.Property(p => p.MiddleName)
				.IsRequired(false);

			modelBuilder
				.Property(p => p.Birthday)
				.IsRequired();

			modelBuilder
				.Property(p => p.Phone)
				.IsRequired(false);

			modelBuilder
				.Property(p => p.AdditionalPhone)
				.IsRequired(false);

			modelBuilder
				.Property(p => p.PhotoId)
				.IsRequired(false);

			modelBuilder
				.Property(p => p.CreationDate)
				.HasDefaultValueSql("NOW()")
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<Person> modelBuilder)
		{
			modelBuilder
				.HasOne(person => person.User)
				.WithOne(user => user.Person)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.HasOne(person => person.Photo)
				.WithOne(file => file.Person)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}