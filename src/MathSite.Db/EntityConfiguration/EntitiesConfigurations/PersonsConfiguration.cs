using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class PersonsConfiguration: AbstractEntityConfiguration
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

		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.HasOne(p => p.User)
				.WithOne(user => user.Person)
				.HasForeignKey<User>(person => person.PersonId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Person";
	}
}