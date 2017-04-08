using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class RightsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Right>()
				.HasKey(right => right.Id);
			modelBuilder.Entity<Right>()
				.HasAlternateKey(right => right.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Right>()
				.Property(right => right.Alias)
				.IsRequired();
			modelBuilder.Entity<Right>()
				.Property(right => right.Description)
				.IsRequired(false);
			modelBuilder.Entity<Right>()
				.Property(right => right.Name)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Right>()
				.HasMany(right => right.GroupsRights)
				.WithOne(groupsRights => groupsRights.Right)
				.HasForeignKey(groupsRights => groupsRights.RightId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			modelBuilder.Entity<Right>()
				.HasMany(right => right.UsersRights)
				.WithOne(usersRights => usersRights.Right)
				.HasForeignKey(usersRights => usersRights.RightId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "Right";
	}
}