using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Models;
using MathSite.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
	public class AccountController : BaseController
	{
		public AccountController(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}
		
		[HttpGet("/login")]
		public IActionResult Login(string returnUrl)
		{
			if (string.IsNullOrWhiteSpace(returnUrl))
				returnUrl = "/";

			if (HttpContext.User.Identity.IsAuthenticated)
				return Redirect(returnUrl);

			return View();
		}
		
		[HttpPost("/login")]
		public async Task<IActionResult> Login(LoginFormViewModel model)
		{
			User ourUser;
			if (
			(ourUser = DbContext.Users.Include(user1 => user1.Group)
					.ThenInclude(group => group.GroupsRights)
					.FirstOrDefault(user => (user.Login == model.Login) && (user.PasswordHash == Passwords.GetHash(model.Password)))
			) != null)
			{
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
					new AuthenticationProperties {ExpiresUtc = DateTime.UtcNow.AddMonths(12)}
				);
				return Redirect(HttpContext.Request.Query["returnUrl"]);
			}

			return View(model);
		}

		public IActionResult CheckLogin(string login)
		{
			return DbContext.Users.FirstOrDefault(user => user.Login == login) != null
				? Json(true)
				: Json("Данного пользователя не существует");
		}
		
		public IActionResult CheckPassword(string password, string login)
		{
			return
				DbContext.Users.FirstOrDefault(user => (user.Login == login) && (user.PasswordHash == Passwords.GetHash(password))) !=
				null
					? Json(true)
					: Json("Пароль неверен");
		}

		[Authorize("logout")]
		[HttpGet("/logout")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.Authentication.SignOutAsync("Auth");

			return RedirectToAction("Index", "Home");
		}
	}
}