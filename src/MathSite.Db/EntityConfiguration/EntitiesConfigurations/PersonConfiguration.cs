using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class PersonConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.HasKey(p => p.Id);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.Property(p => p.Name)
				.IsRequired();

			modelBuilder.Entity<Person>()
				.Property(p => p.Surname)
				.IsRequired();

			modelBuilder.Entity<Person>()
				.Property(p => p.MiddleName)
				.IsRequired(false);

			modelBuilder.Entity<Person>()
				.Property(p => p.Birthday)
				.IsRequired();

			modelBuilder.Entity<Person>()
				.Property(p => p.Phone)
				.IsRequired(false);

			modelBuilder.Entity<Person>()
				.Property(p => p.AdditionalPhone)
				.IsRequired(false);

			modelBuilder.Entity<Person>()
				.Property(p => p.PhotoId)
				.IsRequired(false);

			modelBuilder.Entity<Person>()
				.Property(p => p.CreationDate)
				.HasDefaultValueSql("NOW()")
				.IsRequired(false);

		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.HasOne(person => person.User)
				.WithOne(user => user.Person)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Person>()
				.HasOne(person => person.Photo)
				.WithOne(file => file.Person)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Person";
	}
}