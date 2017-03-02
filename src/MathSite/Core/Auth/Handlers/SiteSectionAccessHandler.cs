using System;
using System.Linq;
using System.Threading.Tasks;
using MathSite.Core.Auth.Requirements;
using MathSite.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Core.Auth.Handlers
{
	public class SiteSectionAccessHandler : AuthorizationHandler<SiteSectionAccess>
	{
		private readonly IMathSiteDbContext _dbContext;

		public SiteSectionAccessHandler(IMathSiteDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SiteSectionAccess requirement)
		{
			var userRightsRetriever = new UserRightsRetriever();

			if (!context.User.Identity.IsAuthenticated)
			{
				context.Fail();
				return Task.FromResult(0);
			}

			var userIdGuidString = context.User.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

			if (string.IsNullOrWhiteSpace(userIdGuidString))
			{
				context.Fail();
				return Task.FromResult(0);
			}

			var userId = Guid.Parse(userIdGuidString);
			var user = _dbContext.Users.Where(u => u.Id == userId)
				.Include(u => u.Group)
				.ThenInclude(group => group.GroupsRights)
				.ThenInclude(group => group.Right)
				.Include(u => u.UsersRights)
				.ThenInclude(usersRights => usersRights.Right)
				.FirstOrDefault();

			if (user == null)
			{
				context.Fail();
				return Task.FromResult(0);
			}

			var rights = userRightsRetriever.GetUserRights(user);

			if (rights.ContainsKey(requirement.SectionName) && rights[requirement.SectionName])
			{
				context.Succeed(requirement);
				return Task.FromResult(0);
			}

			context.Fail();
			return Task.FromResult(0);
		}
	}
}