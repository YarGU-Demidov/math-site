using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MathSite.Db.EntityConfiguration.EntitiesConfigurations
{
	/// <inheritdoc />
	public class UserRightsConfiguration : AbstractEntityConfiguration
	{
		/// <inheritdoc />
		protected override void SetPrimaryKey(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UsersRights>()
				.HasKey(gr => gr.Id);

		}

		/// <inheritdoc />
		protected override void SetFields(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UsersRights>()
				.Property(gr => gr.Allowed)
				.IsRequired();
		}

		/// <inheritdoc />
		protected override void SetRelationships(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.User)
				.WithMany(user => user.UserRights)
				.HasForeignKey(usersRights => usersRights.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.Right)
				.WithMany(right => right.UsersRights)
				.HasForeignKey(usersRights => usersRights.RightId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		/// <inheritdoc />
		public override string ConfigurationName { get; } = "UserRight";
	}
}