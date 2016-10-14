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
		public DbSet<GroupsRights> GroupsRights { get; set; }
		public DbSet<UsersRights> UsersRights { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql(Settings.Instance.ConnectionString, builder => builder.MigrationsAssembly("MathSite"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			SetUserModel(modelBuilder);
			SetPersonModel(modelBuilder);
			SetRightsModel(modelBuilder);
			SetGroupModel(modelBuilder);
			SetGroupsRightsModel(modelBuilder);
			SetUserRightsModel(modelBuilder);

			modelBuilder.HasPostgresExtension("uuid-ossp");
			base.OnModelCreating(modelBuilder);
		}

		private static void SetRightsModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Right>()
				.HasKey(right => right.Id);

			modelBuilder.Entity<Right>()
				.HasAlternateKey(right => right.Alias);

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

		private static void SetPersonModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Person>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Person>()
				.Property(p => p.Name)
				.IsRequired();

			modelBuilder.Entity<Person>()
				.HasOne(p => p.User)
				.WithOne(user => user.Person)
				.HasForeignKey<Person>(person => person.UserId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Person>()
				.Property(p => p.Surname)
				.IsRequired();
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
				.HasOne(user => user.Person)
				.WithOne(person => person.User)
				.HasForeignKey<Person>(person => person.UserId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasOne(user => user.Group)
				.WithMany(group => group.Users)
				.HasForeignKey(user => user.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<User>()
				.HasMany(user => user.UsersRights)
				.WithOne(usersRights => usersRights.User)
				.HasForeignKey(usersRights => usersRights.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		private static void SetGroupModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Group>()
				.HasKey(group => group.Id);
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
				.HasAlternateKey(group => group.Alias);

			modelBuilder.Entity<Group>()
				.HasMany(group => group.Users)
				.WithOne(user => user.Group)
				.HasForeignKey(user => user.GroupId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		private static void SetGroupsRightsModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<GroupsRights>()
				.ToTable(nameof(GroupsRights));

			modelBuilder.Entity<GroupsRights>()
				.HasKey(groupsRights => groupsRights.Id);
			modelBuilder.Entity<GroupsRights>()
				.Property(groupsRights => groupsRights.Allowed)
				.IsRequired();

			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Group)
				.WithMany(group => group.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.GroupId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<GroupsRights>()
				.HasOne(groupsRights => groupsRights.Right)
				.WithMany(right => right.GroupsRights)
				.HasForeignKey(groupsRights => groupsRights.RightId)
				.OnDelete(DeleteBehavior.Cascade);
		}

		private static void SetUserRightsModel(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UsersRights>()
				.ToTable(nameof(UsersRights));

			modelBuilder.Entity<UsersRights>()
				.HasKey(gr => gr.Id);
			modelBuilder.Entity<UsersRights>()
				.Property(gr => gr.Allowed)
				.IsRequired();

			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.User)
				.WithMany(user => user.UsersRights)
				.HasForeignKey(usersRights => usersRights.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<UsersRights>()
				.HasOne(usersRights => usersRights.Right)
				.WithMany(right => right.UsersRights)
				.HasForeignKey(usersRights => usersRights.RightId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}