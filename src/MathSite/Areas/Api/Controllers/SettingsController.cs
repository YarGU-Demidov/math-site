using MathSite.Controllers;
using MathSite.Db;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	public class SettingsController : BaseController
	{
		public SettingsController(MathSiteDbContext dbContext) : base(dbContext)
		{
		}
	}
}