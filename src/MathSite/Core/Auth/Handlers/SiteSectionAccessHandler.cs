using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Core.Auth.Requirements;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authorization;

namespace MathSite.Core.Auth.Handlers
{
	public class SiteSectionAccessHandler : AuthorizationHandler<SiteSectionAccess>
	{
		private readonly IUserValidationFacade _userValidationFacade;

		public SiteSectionAccessHandler(IUserValidationFacade userValidationFacade)
		{
			_userValidationFacade = userValidationFacade;
		}

		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, SiteSectionAccess requirement)
		{

			if (!context.User.Identity.IsAuthenticated)
			{
				context.Fail();
				return;
			}

			var userIdGuidString = context.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

			if (string.IsNullOrWhiteSpace(userIdGuidString))
			{
				context.Fail();
				return;
			}

			var userId = Guid.Parse(userIdGuidString);

			if (!await _userValidationFacade.DoesUserExistsAsync(userId))
			{
				context.Fail();
				return;
			}

			if (!await _userValidationFacade.UserHasRightAsync(userId, requirement.SectionName))
			{
				context.Fail();
				return;
			}

			context.Succeed(requirement);
		}
	}
}