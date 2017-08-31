using System;
using System.Collections.Generic;
using System.Globalization;
using MathSite.Db;
using MathSite.Domain.Common;
using MathSite.ViewModels.Capability;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController(MathSiteDbContext dbContext, IBusinessLogicManger businessLogicManager)
			: base(dbContext, businessLogicManager)
		{
			Logic = businessLogicManager;
		}

		private IBusinessLogicManger Logic { get; }

		public IActionResult Index()
		{
			var menu = new List<MenuItem>
			{
				new MenuItem("Абитуриентам", "for-entrants"),
				new MenuItem("Студентам", "for-students"),
				new MenuItem("Школа", "for-scholars"),
				new MenuItem("Контакты", "contacts")
			};

			var footerMenu = new Tuple<IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>, IEnumerable<MenuItem>>(
				new List<MenuItem>
				{
					new MenuItem("Абитуриентам", "for-entrants"),
					new MenuItem("Кафедры", "departments")
				},
				new List<MenuItem> {new MenuItem("Студентам", "for-students")},
				new List<MenuItem> {new MenuItem("Школа", "for-scholars")},
				new List<MenuItem>
				{
					new MenuItem("Контакты", "contacts"),
					new MenuItem("math@uniyar.ac.ru", "mailto:math@uniyar.ac.ru"),
					new MenuItem("www.math.uniyar.ac.ru", "http://www.math.uniyar.ac.ru"),
					new MenuItem("vk.com/math.uniyar.abitur", "https://vk.com/math.uniyar.abitur")
				}
			);

			var pageTitle = new PageTitle("Главная страница", " | ", "Математический Факультет ЯрГУ");

			var posts = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test4", "/news/test4-url", "Test content for 4th post", DateTime.Now.AddDays(-3).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test5", "/news/test5-url", "Test content for 5th post", DateTime.Now.AddMonths(-7).AddDays(-15).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test6", "/news/test6-url", "Test content for 6th post", DateTime.Now.AddMonths(-5).AddDays(-10).ToString("dd MMM yyyy", new CultureInfo("ru"))),
			};

			var model = new MainPageViewModel(
				pageTitle,
				"Главная страница сайта Математического факультета ЯрГУ",
				"Математика, ЯрГУ, Матфак",
				menu,
				footerMenu,
				posts
			);

			return View(model);
		}
	}
}