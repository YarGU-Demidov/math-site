using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Core;
using MathSite.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MathSite.Areas.Api.Controllers
{
	public enum AuthStatus
	{
		Success, WrongPassword, UserDoesntExists
	}

	public class AuthResult
	{
		public AuthResult(AuthStatus authStatus)
		{
			AuthStatus = authStatus;
		}

		public AuthStatus AuthStatus { get; }
	}

	[Area("Api")]
	public class AuthController : Controller
	{
		private readonly IMathSiteDbContext _dbContext;

		public AuthController(IMathSiteDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpPost]
		public async Task<AuthResult> Login(string login, string password)
		{
			var users = _dbContext.Users.Where(u => u.Login == login);
			var ourUser = users.FirstOrDefault();

			if (ourUser == null)
				return new AuthResult(AuthStatus.UserDoesntExists);

			if (Passwords.GetHash(password) != ourUser.PasswordHash)
				return new AuthResult(AuthStatus.WrongPassword);

			await HttpContext.Authentication.SignInAsync("Auth", new MathUserPrincipal(_dbContext, ourUser));
			return new AuthResult(AuthStatus.Success);
		}

		[Authorize]
		public string Test()
		{
			return "test";
		}
	}
}