using MathSite.Common;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace MathSite.Db
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class MathSiteDbContext : DbContext, IMathSiteDbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<Right> Rights { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql(Settings.Instance.ConnectionString, builder => builder.MigrationsAssembly("MathSite"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetUserModel(modelBuilder);
			SetPersonUserRelation(modelBuilder);
			SetPersonModel(modelBuilder);
			SetGroupModel(modelBuilder);
			SetGroupsRightsModel(modelBuilder);
			SetUserRightsModel(modelBuilder);

			modelBuilder.HasPostgresExtension("uuid-ossp");
			base.OnModelCreating(modelBuilder);
		}

		private static void SetPersonUserRelation(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasOne(u => u.Person)
				.WithOne(p => p.User)
				.HasForeignKey<Person>(u => u.UserId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);
		}

		private static void SetUserModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasKey(u => u.Id);
			modelBuilder.Entity<User>()
				.Property(u => u.Login)
				.IsRequired();
			modelBuilder.Entity<User>()
				.Property(u => u.PasswordHash)
				.IsRequired();
			modelBuilder.Entity<User>()
				.Property(user => user.GroupId)
				.IsRequired();
		}

		private static void SetPersonModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Person>()
				.Property(p => p.Name)
				.IsRequired();
			modelBuilder.Entity<Person>()
				.Property(p => p.Surname)
				.IsRequired();
		}

		private static void SetGroupModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasKey(g => g.Id);
			modelBuilder.Entity<Group>()
				.Property(group => group.Name)
				.IsRequired();
			modelBuilder.Entity<Group>()
				.Property(group => group.Description)
				.IsRequired(false);
			modelBuilder.Entity<Group>()
				.Property(group => group.Alias)
				.IsRequired();

			modelBuilder.Entity<Group>()
				.HasMany(group => group.Users)
				.WithOne(user => user.Group)
				.HasForeignKey(user => user.GroupId);
		}

		private static void SetGroupsRightsModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupsRights>()
				.HasKey(gr => gr.Id);
			modelBuilder.Entity<GroupsRights>()
				.Property(gr => gr.Value)
				.IsRequired();

			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Group)
				.WithMany(group => group.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.GroupId);

			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Right)
				.WithMany(right => right.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.RightId);
		}

		private static void SetUserRightsModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UsersRights>()
				.HasKey(gr => gr.Id);
			modelBuilder.Entity<UsersRights>()
				.Property(gr => gr.Value)
				.IsRequired();

			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.User)
				.WithMany(user => user.UsersRights)
				.HasForeignKey(usersRights => usersRights.UserId);
			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.Right)
				.WithMany(right => right.UsersRights)
				.HasForeignKey(usersRights => usersRights.RightId);
		}
	}
}