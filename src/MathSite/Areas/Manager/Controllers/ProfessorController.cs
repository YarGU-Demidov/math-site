using System.Threading.Tasks;
using MathSite.Db.DataSeeding.StaticData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Manager.Controllers
{
    [Authorize(RightAliases.AdminAccess)]
    [Area("manager")]
    [Route("[area]/[controller]")]
    public class ProfessorController : Controller
    {
        [Route("")]
        [Route("index")]
        [Route("list")]
        public async Task<IActionResult> Index()
        {
            return Content("test");
        }
    }
}