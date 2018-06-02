﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MathSite.Areas.Api.Heplers.Auth;
using MathSite.Controllers;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
    [Area("api")]
    public class AuthController : BaseController
    {
        public AuthController(
            IUserValidationFacade userValidationFacade, 
            IUsersFacade usersFacade
        ) : base(userValidationFacade, usersFacade)
        {
        }

        [HttpPost]
        public async Task<LoginResult> Login(string login, string password)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return new LoginResult(LoginStatus.AlreadySignedIn);

            var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(login, password);

            if (ourUser == null)
                return new LoginResult(LoginStatus.WrongPasswordOrDoesntExists);

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