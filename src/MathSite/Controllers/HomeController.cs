using System.Linq;
using MathSite.Common;
using MathSite.Db;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		public IActionResult Index()
		{
			var data = DbContext.Groups.Where(group => group.Alias == GroupsAliases.Admin)
				.SelectMany(group => group.Users)
				.Select(
					user =>
						$"User {user.Person.Surname} {user.Person.Name} {user.Person.MiddleName} is in {user.Group.Name}({user.Group.Description}) group with alias \"{user.Group.Alias}\"")
				.ToArray();
			ViewBag.UsersData = data;

			return View();
		}
	}
}