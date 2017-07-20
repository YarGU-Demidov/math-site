using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class GroupTypeConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupType>()
				.HasKey(groupType => groupType.Id);

			modelBuilder.Entity<GroupType>()
				.HasAlternateKey(groupType => groupType.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupType>()
				.Property(groupType => groupType.Name)
				.IsRequired();

			modelBuilder.Entity<GroupType>()
				.Property(groupType => groupType.Description)
				.IsRequired(false);

			modelBuilder.Entity<GroupType>()
				.Property(groupType => groupType.Alias)
				.IsRequired(false);
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupType>()
				.HasMany(groupType => groupType.Groups)
				.WithOne(group => group.GroupType)
				.HasForeignKey(group => group.GroupTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "GroupType";
	}
}