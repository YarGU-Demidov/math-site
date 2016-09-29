using System;
using System.Threading;
using System.Threading.Tasks;
using MathSite.Models;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db
{
	public interface IMathSiteDbContext : IDisposable
	{
		DbSet<Group> Groups { get; set; }
		DbSet<Person> Persons { get; set; }
		DbSet<Right> Rights { get; set; }
		DbSet<User> Users { get; set; }
		DbSet<GroupsRights> GroupsRights { get; set; }
		DbSet<UsersRights> UsersRights { get; set; }

		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
	}
}