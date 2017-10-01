using MathSite.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
    public class GroupTypeConfiguration : AbstractEntityWithAliasConfiguration<GroupType>
    {
        protected override string TableName { get; } = nameof(GroupType);
        
        /// <inheritdoc />
        protected override void SetFields(EntityTypeBuilder<GroupType> modelBuilder)
        {
            base.SetFields(modelBuilder);

            modelBuilder
                .Property(groupType => groupType.Name)
                .IsRequired();

            modelBuilder
                .Property(groupType => groupType.Description)
                .IsRequired(false);
        }

        /// <inheritdoc />
        protected override void SetRelationships(EntityTypeBuilder<GroupType> modelBuilder)
        {
            base.SetRelationships(modelBuilder);

            modelBuilder
                .HasMany(groupType => groupType.Groups)
                .WithOne(group => group.GroupType)
                .HasForeignKey(group => group.GroupTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}