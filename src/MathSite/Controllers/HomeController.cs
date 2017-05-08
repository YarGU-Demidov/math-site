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
			var groupId = Guid.Parse("bb82cef1-9403-4179-bab0-f95d5a8edad7");
			var groupTypeId = Guid.Parse("0cc0d232-8682-4753-826f-a2a7fc6a679e"); // Students

			var createGroup = _logic.GroupsLogic
				.CreateGroupAsync(currentUserId, "Test name", "Test description", groupTypeId, null);
			//var updatedGroup = _logic.GroupsLogic.UpdateGroupAsync(currentUserId, groupId, "Update", "Update", groupTypeId, null);
			//var deleteGroup = _logic.GroupsLogic.DeleteGroupAsync(currentUserId, groupId);
			var group = _logic.GroupsLogic.GetFromGroups(g => g.FirstOrDefault(i => i.Id == groupTypeId));

			//var personLogic = _logic.PersonsLogic;
			//var persons = personLogic.GetFromPersons(allPersons => allPersons
			//	.Select(s => s.Surname));

			//var groupLogic = _logic.GroupsLogic;
			//var groups = groupLogic.GetFromGroups(allGroups => allGroups
			//	.Select(i => i.Name));

			ViewBag.UsersData = group;

			return View();
		}
	}
}