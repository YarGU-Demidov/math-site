using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MathSite.Controllers
{
	public class AccountController : BaseController
	{
		private readonly IPasswordsManager _passwordHasher;

		public AccountController(MathSiteDbContext dbContext, IPasswordsManager passwordHasher) : base(dbContext)
		{
			_passwordHasher = passwordHasher;
		}

		[HttpGet("/login")]
		public IActionResult Login(string returnUrl)
		{
			if (string.IsNullOrWhiteSpace(returnUrl))
				returnUrl = "/";

			if (HttpContext.User.Identity.IsAuthenticated)
				if (!string.IsNullOrWhiteSpace(returnUrl))
					return Redirect(returnUrl);
				else
					return RedirectToAction("Index", "Home");

			return View();
		}

		[HttpPost("/login")]
		public async Task<IActionResult> Login(LoginFormViewModel model)
		{
			var ourUser = DbContext.Users.Include(user1 => user1.Group)
				.ThenInclude(group => group.GroupsRights)
				.FirstOrDefault(
					user => user.Login == model.Login &&
					        _passwordHasher.PasswordsAreEqual(model.Login, model.Password, user.PasswordHash)
				);

			if (ourUser == null)
				return View(model);

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
				new AuthenticationProperties {ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(12)}
			);

			var returnUrl = HttpContext.Request.Query["returnUrl"].ToString();

			if (!string.IsNullOrWhiteSpace(returnUrl))
				return Redirect(returnUrl);

			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> CheckLogin(string login)
		{
			return await DbContext.Users.FirstOrDefaultAsync(user => user.Login == login) != null
				? Json(true)
				: Json("Данного пользователя не существует");
		}

		public async Task<IActionResult> CheckPassword(string password, string login)
		{
			return
				await
					DbContext.Users.FirstOrDefaultAsync(
						user => user.Login == login && _passwordHasher.PasswordsAreEqual(login, password, user.PasswordHash)) != null
					? Json(true)
					: Json("Пароль неверен");
		}

		[Authorize("logout")]
		[HttpGet("/logout")]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Index", "Home");
		}
	}
}