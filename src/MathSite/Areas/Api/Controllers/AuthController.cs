using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class AuthController : Controller
	{
		[HttpPost]
		public JsonResult Login(string login, string password)
		{
			return new JsonResult(new { Login = login, Pass = password }, new JsonSerializerSettings {Formatting = Formatting.Indented});
		}
	}
}