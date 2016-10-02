using System;
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
		public IActionResult Login(string returnUrl)
		{
			if (string.IsNullOrWhiteSpace(returnUrl))
				returnUrl = "/";

			if (HttpContext.User.Identity.IsAuthenticated)
				Redirect(returnUrl);

			return View();
		}

		[AllowAnonymous]
		public IActionResult Register()
		{
			throw new NotImplementedException();
		}

		[Authorize("logout")]
		public async Task Logout()
		{
			await HttpContext.Authentication.SignOutAsync("Auth");
		}

		[AllowAnonymous]
		[Route("/forbidden")]
		[Route("/account/forbidden")]
		public IActionResult Forbidden(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}