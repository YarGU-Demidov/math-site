using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class GroupsConfiguration: AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasKey(group => group.Id);
			modelBuilder.Entity<Group>()
				.HasAlternateKey(group => group.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.Property(group => group.Name)
				.IsRequired();
			modelBuilder.Entity<Group>()
				.Property(group => group.Description)
				.IsRequired(false);
			modelBuilder.Entity<Group>()
				.Property(group => group.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasMany(group => group.Users)
				.WithOne(user => user.Group)
				.HasForeignKey(user => user.GroupId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Groups";
	}
}