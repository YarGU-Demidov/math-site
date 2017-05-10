using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
	public class BaseController : Controller
	{
		protected IBusinessLogicManger LogicManger { get; }
		protected MathSiteDbContext DbContext { get; }
		protected User CurrentUser { get; private set; }

		public BaseController(MathSiteDbContext dbContext, IBusinessLogicManger logicManger)
		{
			LogicManger = logicManger;
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
				.Where(u => u.Id == userIdGuid)
				.Include(user => user.Person)
				.Include(user => user.Group)
				.FirstOrDefault();

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