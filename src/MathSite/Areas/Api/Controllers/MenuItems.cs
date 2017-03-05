using MathSite.Controllers;
using MathSite.Db;
using MathSite.ViewModels.Api.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Areas.Api.Controllers
{
	[Area("Api")]
	[Route("/api/menu-items")]
	public class MenuItems : BaseController
	{
		public MenuItems(IMathSiteDbContext dbContext) : base(dbContext)
		{
		}

		[HttpGet]
		public MenuItemData[] Index()
		{
			return new[]
			{
				new MenuItemData("assignment_turned_in", "Tasks", "#", new[]
				{
					new[]
					{
						new MenuItemData("", "test subitem #1", "#", new[]
						{
							new[]
							{
								new MenuItemData("", "yo #0", "#"),
								new MenuItemData("", "yo #1", "#"),
								new MenuItemData("", "yo #2", "#"),
							}
						}),
						new MenuItemData("", "test subitem #2", "#")
					},
					new []
					{
						new MenuItemData("", "test subitem #3", "#"),
						new MenuItemData("", "test subitem #4", "#")
					}
				}),
				new MenuItemData("face", "Users", "#"),
				new MenuItemData("event", "Calendar", "#"),
				new MenuItemData("art_track", "Content", "#"),
			};
		}
	}
}