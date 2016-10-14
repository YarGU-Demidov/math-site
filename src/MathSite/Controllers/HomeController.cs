using System.Linq;
using MathSite.Db;
using Microsoft.AspNetCore.Authorization;
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
			var data = DbContext.Groups.Where(group => group.Alias == "admin")
				.SelectMany(group => group.Users)
				.Select(
					user =>
							$"User {user.Person.Surname} {user.Person.Name} {user.Person.MiddleName} is in {user.Group.Name}({user.Group.Description}) group with alias \"{user.Group.Alias}\"")
				.ToArray();
			ViewBag.UsersData = data;
			
			return View();
		}

		[Authorize("admin")]
		public IActionResult Test()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
	}
}