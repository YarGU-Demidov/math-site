using System;
using System.Collections.Generic;
using System.Globalization;
using MathSite.ViewModels.Capability;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class NewsController : Controller
	{
		public IActionResult Index(string query, int page = 1)
		{
			ViewData.Add("page", page);

			return string.IsNullOrWhiteSpace(query)
				? ShowAllNews()
				: ShowNews(query, page);
		}

		[NonAction]
		private IActionResult ShowAllNews()
		{
			var pageTitle = new PageTitle("Новости факультета", " | ", "Математический Факультет ЯрГУ");

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
				new List<MenuItem> { new MenuItem("Студентам", "for-students") },
				new List<MenuItem> { new MenuItem("Школа", "for-scholars") },
				new List<MenuItem>
				{
					new MenuItem("Контакты", "contacts"),
					new MenuItem("math@uniyar.ac.ru", "mailto:math@uniyar.ac.ru"),
					new MenuItem("www.math.uniyar.ac.ru", "http://www.math.uniyar.ac.ru"),
					new MenuItem("vk.com/math.uniyar.abitur", "https://vk.com/math.uniyar.abitur")
				}
			);

			var postsPreview = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru")))
			};

			var posts = new List<PostPreviewViewModel>
			{
				new PostPreviewViewModel("Test1", "/news/test1-url", "Test content for 1st post", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test2", "/news/test2-url", "Test content for 2nd post", DateTime.Now.AddMonths(-2).AddDays(-1).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test3", "/news/test3-url", "Test content for 3d post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test4", "/news/test4-url", "Test content for 4th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test5", "/news/test5-url", "Test content for 5th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test6", "/news/test6-url", "Test content for 6th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test7", "/news/test7-url", "Test content for 7th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test8", "/news/test8-url", "Test content for 8th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test9", "/news/test9-url", "Test content for 9th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test10", "/news/test10-url", "Test content for 10th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test11", "/news/test11-url", "Test content for 11th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test12", "/news/test12-url", "Test content for 12th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test13", "/news/test13-url", "Test content for 13th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test14", "/news/test14-url", "Test content for 14th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test15", "/news/test15-url", "Test content for 15th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
				new PostPreviewViewModel("Test16", "/news/test16-url", "Test content for 16th post", DateTime.Now.AddDays(-2).ToString("dd MMM yyyy", new CultureInfo("ru"))),
			};

			var sidebar = new List<MenuItem>
			{
				new MenuItem(
					"Кафедры",
					"/departments",
					new List<MenuItem>
					{
						new MenuItem("Кафедра общей математики", "/departments/general-math"),
						new MenuItem("Кафедра математического анализа", "/departments/calculus"),
						new MenuItem("Кафедра компьютерной безопасности и математических методов обработки информации", "/departments/computer-security"),
						new MenuItem("Кафедра алгебры и математической логики", "/departments/algebra"),
						new MenuItem("Кафедра математического моделирования", "/departments/mathmod"),
						new MenuItem("Кафедра дифференциальных уравнений", "/departments/differential-equations")
					}
				)
			};

			var model = new NewsPageInfo(
				pageTitle,
				"Новости нашего факультета",
				"Математика, ЯрГУ, Матфак",
				menu,
				footerMenu,
				postsPreview,
				posts,
				sidebar
			);

			return View(model);
		}

		[NonAction]
		private IActionResult ShowNews(string query, int page)
		{
			return Content("news content will be here... soon, we promise");
		}
	}
}