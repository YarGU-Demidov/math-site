using System;
using System.Linq;
using MathSite.Db;
using MathSite.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace MathSite.Controllers
{
	public class HomeController : BaseController
	{
		private readonly IBusinessLogicManger _logic;

		public HomeController(MathSiteDbContext dbContext, IBusinessLogicManger businessLogicManager) : base(dbContext)
		{
			_logic = businessLogicManager;
		}

		public IActionResult Index()
		{
			var currentUserId = Guid.Parse("0c4c48cc-ac0e-4bae-8b88-59ea39764bae"); // andrey_devyatkin
			var groupId = Guid.Parse("1b51199d-d7cf-4de6-a4c1-495e35b1e770");
			//var groupTypeId = Guid.Parse("0cc0d232-8682-4753-826f-a2a7fc6a679e"); // Students

			var createdPerson = _logic.PersonsLogic.CreatePersonAsync(
				name: "А",
				surname: "Д",
				middlename: "В",
				birthday: DateTime.Now,
				phoneNumber: "1",
				additionalPhoneNumber: "1",
				userId: null,
				photoId: null,
				creationDate: DateTime.Now).Result;

			var person = _logic.PersonsLogic.GetFromPersons(p => p.FirstOrDefault(i => i.Id == createdPerson));

			//var createdUser = _logic.UsersLogic.CreateUserAsync(
			//	login: "Test",
			//	passwordHash: "Test",
			//	creationDate: DateTime.Now,
			//	personId: person.Id,
			//	groupId: groupId).Result;
			var updatedUser = _logic.UsersLogic.UpdateUserAsync(
				currentUserId: currentUserId,
				passwordHash: "1",
				groupId: groupId);

			ViewBag.UsersData = person;

			return View();
		}
	}
}