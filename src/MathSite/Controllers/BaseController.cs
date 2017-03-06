using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
	public class BaseController : Controller
	{
		protected readonly IMathSiteDbContext DbContext;
		protected User CurrentUser { get; private set; }

		public BaseController(IMathSiteDbContext dbContext)
		{
			DbContext = dbContext;
		}

		private void TrySetUser(ActionContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
				return;

			var userId = context.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

			if (userId == null)
				return;

			var userIdGuid = Guid.Parse(userId);
			var currentUser = DbContext.Users
				.Include(user => user.Person)
				.Include(user => user.Group)
				.FirstOrDefault(user => user.Id == userIdGuid);

			CurrentUser = currentUser;
			ViewBag.User = currentUser;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			TrySetUser(context);
			base.OnActionExecuting(context);
		}

		public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			TrySetUser(context);
			return base.OnActionExecutionAsync(context, next);
		}
	}
}