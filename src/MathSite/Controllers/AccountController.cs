using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class AccountController: Controller
	{
		[HttpGet("/login")]
		[HttpGet("/auth")]
		[HttpGet("/account/login")]
		[HttpGet("/account/auth")]
		[AllowAnonymous]
		public async Task<IActionResult> Login()
		{
			/*var a = new ClaimsPrincipal();
			await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);*/

			return View();
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			throw new NotImplementedException();
		}

		[Authorize("Logout")]
		public async Task Logout()
		{
			await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");
		}

		[AllowAnonymous]
		public IActionResult Forbidden()
		{
			throw new NotImplementedException();
		}
	}
}