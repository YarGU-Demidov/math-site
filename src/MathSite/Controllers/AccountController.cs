using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Facades.UserValidation;
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
		public AccountController(IUserValidationFacade userValidationFacade) : base(userValidationFacade)
		{
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
			var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);

			if (ourUser == null)
				return View(model);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(
					new ClaimsIdentity(
						new List<Claim>
						{
							new Claim("UserId", ourUser.Id.ToString())
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
			return await UserValidationFacade.DoesUserExistsAsync(login)
				? Json(true)
				: Json("Данного пользователя не существует");
		}

		public async Task<IActionResult> CheckPassword(string password, string login)
		{
			var user = await UserValidationFacade.GetUserByLoginAndPasswordAsync(login, password);
			
			return user != null
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