using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Authenticator;
using MathSite.Common.Crypto;
using MathSite.Common.Extensions;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IUserValidationFacade userValidationFacade, IUsersFacade usersFacade)
            : base(userValidationFacade, usersFacade)
        {
        }

        [HttpGet("/login")]
        public IActionResult Login(string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(returnUrl))
                returnUrl = "/";

            if (HttpContext.User.Identity.IsAuthenticated)
                if (!string.IsNullOrWhiteSpace(returnUrl))
                    return LocalRedirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");

            return View(new LoginFormViewModel {ReturnUrl = returnUrl});
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginFormViewModel model)
        {
            var valid = TryValidateModel(model);

            if (!valid)
                return View(model);

            var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);

            if (ourUser == null)
                return View(model);

            if (ourUser.TwoFactorAuthenticationKey == null)
            {
                return await ContinueLogin(model);
            }
            if (ourUser.TwoFactorAuthenticationKey.IsNotNullAndEmpty())
            {
                var tfa = new TwoFactorAuthenticator();
                var key = await UserValidationFacade.KeyManager.CreateEncryptedKey();
                var keyString = await UserValidationFacade.KeyManager.GetDecryptedString(key);
                model.TemporalTwoFactorAutenticationKey = key;
                var setupInfo = tfa.GenerateSetupCode("Math site", model.Login, keyString, 300, 300);
                model.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                model.SetupCode = setupInfo.ManualEntryKey;
                return View("~/Views/Account/Add2FA.cshtml", model);
            }
            model.HasTwoFactorAutentification = true;
            return View("~/Views/Account/TwoFactorAuthentication.cshtml", model);
        }

        public async Task<IActionResult> CheckLogin(string login)
        {
            return await UsersFacade.DoesUserExistsAsync(login)
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
        public async Task<IActionResult> CheckKey(string token, byte[] temporalTwoFactorAutenticationKey)
        {
            var tfa = new TwoFactorAuthenticator();
            var userUniqueKey =
                await UserValidationFacade.KeyManager.GetDecryptedString(temporalTwoFactorAutenticationKey);
            var isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, token);
            return isValid
                ? Json(true)
                : Json("Ключ неверен");
        }
        [Authorize("logout")]
        [HttpGet("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/continue-login")]
        public async Task<IActionResult> ContinueLogin(LoginFormViewModel model)
        {
            var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);
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
                return LocalRedirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost("/verify-autentication")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyTwoFactorAutentication(LoginFormViewModel model)
        {
            var tfa = new TwoFactorAuthenticator();
            string userUniqueKey = "";
            if (model.TemporalTwoFactorAutenticationKey == null)
            {
                var ourUser = await UserValidationFacade.GetUserByLoginAndPasswordAsync(model.Login, model.Password);
                userUniqueKey =
                    await UserValidationFacade.KeyManager.GetDecryptedString(ourUser.TwoFactorAuthenticationKey);
            }
            else
                userUniqueKey =
                    await UserValidationFacade.KeyManager.GetDecryptedString(model.TemporalTwoFactorAutenticationKey.ToArray());
            var isValid = tfa.ValidateTwoFactorPIN(userUniqueKey, model.Token);
            if (!isValid)
                return View("~/Views/Account/TwoFactorAuthentication.cshtml", model);
            if (model.TemporalTwoFactorAutenticationKey.IsNotNull())
                await UserValidationFacade.SetUserKey(model.Login, model.TemporalTwoFactorAutenticationKey.ToArray());
            return await ContinueLogin(model);
        }
    }
}
