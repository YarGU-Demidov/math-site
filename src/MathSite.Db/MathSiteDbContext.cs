using MathSite.Common;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace MathSite.Db
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class MathSiteDbContext : DbContext
	{
		public DbSet<Person> Persons { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql(Settings.Instance.ConnectionString, builder => builder.MigrationsAssembly("MathSite"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
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
				.HasOne(u => u.Person)
				.WithOne(p => p.User)
				.HasForeignKey<Person>(u => u.UserId)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Person>()
				.HasKey(p => p.Id);
			modelBuilder.Entity<Person>()
				.Property(p => p.Name)
				.IsRequired();
			modelBuilder.Entity<Person>()
				.Property(p => p.Surname)
				.IsRequired();

			modelBuilder.HasPostgresExtension("uuid-ossp");

			base.OnModelCreating(modelBuilder);
		}
	}
}