using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Areas.Api.Heplers.Auth;
using MathSite.Common.Crypto;
using MathSite.Controllers;
using MathSite.Db;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class AuthController : BaseController
	{
		public AuthController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		//[HttpPost]
		public async Task<LoginResult> Login(string login, string password)
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				return new LoginResult(LoginStatus.AlreadySignedIn);

			var ourUser = DbContext.Users
				.Include(user => user.UsersRights)
				.Include(user => user.Group)
				.ThenInclude(group => group.GroupsRights)
				.ThenInclude(rights => rights.Right)
				.FirstOrDefault(u => u.Login == login);

			if (ourUser == null)
				return new LoginResult(LoginStatus.UserDoesntExists);

			if (Passwords.GetHash(password) != ourUser.PasswordHash)
				return new LoginResult(LoginStatus.WrongPassword);

			await HttpContext.Authentication.SignInAsync(
				"Auth",
				new ClaimsPrincipal(
					new ClaimsIdentity(
						new List<Claim>
						{
							new Claim("UserId", ourUser.Id.ToString()),
							new Claim("Login", ourUser.Login),
							new Claim("GroupAlias", ourUser.Group.Alias)
						},
						"Auth"
					)
				),
				new AuthenticationProperties
				{
					ExpiresUtc = DateTime.UtcNow.AddMonths(12)
				}
			);

			return new LoginResult(LoginStatus.Success);
		}

		public async Task<LogoutResult> Logout()
		{
			if (!HttpContext.User.Identity.IsAuthenticated)
				return new LogoutResult(LogoutStatus.NotLoggedIn);
			try
			{
				await HttpContext.Authentication.SignOutAsync("Auth");
				return new LogoutResult(LogoutStatus.Success);
			}
			catch (Exception)
			{
				return new LogoutResult(LogoutStatus.Error);
			}
		}
	}
}