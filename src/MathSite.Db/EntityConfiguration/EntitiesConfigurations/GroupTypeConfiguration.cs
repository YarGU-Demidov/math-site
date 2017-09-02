using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	public class GroupTypeConfiguration : AbstractEntityConfiguration<GroupType>
	{
		protected override string TableName { get; } = nameof(GroupType);

		/// <inheritdoc />
		protected override void SetKeys(EntityTypeBuilder<GroupType> modelBuilder)
		{
			modelBuilder
				.HasKey(groupType => groupType.Id);

			modelBuilder
				.HasAlternateKey(groupType => groupType.Alias);
		}

		/// <inheritdoc />
		protected override void SetFields(EntityTypeBuilder<GroupType> modelBuilder)
		{
			modelBuilder
				.Property(groupType => groupType.Name)
				.IsRequired();

			modelBuilder
				.Property(groupType => groupType.Description)
				.IsRequired(false);

			modelBuilder
				.Property(groupType => groupType.Alias)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(EntityTypeBuilder<GroupType> modelBuilder)
		{
			modelBuilder
				.HasMany(groupType => groupType.Groups)
				.WithOne(group => group.GroupType)
				.HasForeignKey(group => group.GroupTypeId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}

		protected override void SetIndexes(EntityTypeBuilder<GroupType> modelBuilder)
		{
		}
	}
}