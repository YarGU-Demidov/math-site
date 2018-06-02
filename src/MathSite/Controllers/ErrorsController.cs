using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
    public class ErrorsController : Controller
    {
        [AllowAnonymous]
        [Route("/error/404")]
        public IActionResult NotFound404()
        {
            Response.StatusCode = 404;
            return View();
        }

        [AllowAnonymous]
        [Route("/error/403")]
        [Route("/error/forbidden")]
        public IActionResult Forbidden(string returnUrl)
        {
            Response.StatusCode = 403;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [Route("/error/400")]
        public IActionResult BadRequest(string returnUrl)
        {
            Response.StatusCode = 400;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}