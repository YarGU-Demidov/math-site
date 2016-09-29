using System.Linq;
using System.Security.Claims;
using MathSite.Db;
using MathSite.Models;

namespace MathSite.Core
{
	public sealed class MathUser : ClaimsIdentity
	{
		public MathUser(User user)
		{
			AddClaims(new ClaimsGenerator().GetUserClaims(user));
		}
	}

	public sealed class MathUserPrincipal : ClaimsPrincipal
	{
		private readonly IMathSiteDbContext _dbContext;

		public MathUserPrincipal(IMathSiteDbContext dbContext, User user)
		{
			_dbContext = dbContext;
			AddIdentity(new MathUser(user));
		}
	}
}