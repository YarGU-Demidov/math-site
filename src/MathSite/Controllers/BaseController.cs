using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MathSite.Controllers
{
	public class BaseController : Controller
	{
		public BaseController(IUserValidationFacade userValidationFacade)
		{
			UserValidationFacade = userValidationFacade;
		}
		
		protected IUserValidationFacade UserValidationFacade { get; }
		
		protected Guid? CurrentUserId { get; private set; }

		[NonAction]
		private async Task TrySetUser(ActionContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
				return;

			var userId = context.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

			if (userId == null)
				return;

			var userIdGuid = Guid.Parse(userId);

			if(userIdGuid == Guid.Empty)
				return;

			if (!await UserValidationFacade.DoesUserExistsAsync(userIdGuid))
				return;
			
			CurrentUserId = userIdGuid;
		}

		[NonAction]
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
			var userSetTask = TrySetUser(context);
			userSetTask.Wait();
		}

		[NonAction]
		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			await base.OnActionExecutionAsync(context, next);
			await TrySetUser(context);
		}
	}
}