using MathSite.Models;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Db
{
	public interface IMathSiteDbContext
	{
		DbSet<Group> Groups { get; set; }
		DbSet<Person> Persons { get; set; }
		DbSet<Right> Rights { get; set; }
		DbSet<User> Users { get; set; }
	}
}