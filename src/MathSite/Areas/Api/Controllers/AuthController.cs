using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Areas.Api.Heplers.Auth;
using MathSite.Common.Crypto;
using MathSite.Controllers;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class AuthController : BaseController
	{
		private readonly IPasswordsManager _passwordHasher;

		public AuthController(MathSiteDbContext dbContext, IPasswordsManager passwordHasher, IBusinessLogicManger logicManger) : base(dbContext, logicManger)
		{
			_passwordHasher = passwordHasher;
		}

		[HttpPost]
		public async Task<LoginResult> Login(string login, string password)
		{
			if (HttpContext.User.Identity.IsAuthenticated)
				return new LoginResult(LoginStatus.AlreadySignedIn);

			var ourUser = DbContext.Users
				.Include(user => user.UserRights)
				.Include(user => user.Group)
				.ThenInclude(group => group.GroupsRights)
				.ThenInclude(rights => rights.Right)
				.FirstOrDefault(u => u.Login == login);

			if (ourUser == null)
				return new LoginResult(LoginStatus.UserDoesntExists);

			if (_passwordHasher.PasswordsAreEqual(login, password, ourUser.PasswordHash))
				return new LoginResult(LoginStatus.WrongPassword);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
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
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				return new LogoutResult(LogoutStatus.Success);
			}
			catch (Exception)
			{
				return new LogoutResult(LogoutStatus.Error);
			}
		}
	}
}