using System;
using MathSite.Db;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Users;

namespace MathSite.Domain.Common
{
	public class BusinessLogicManager : IBusinessLogicManger
	{
		private readonly IMathSiteDbContext _context;
		private bool _isDisposed;

		public IGroupsLogic GroupsLogic { get; set; }
		public IPersonsLogic PersonsLogic { get; set; }
		public IUsersLogic UsersLogic { get; set; }

		public BusinessLogicManager(IMathSiteDbContext context, IGroupsLogic groupsLogic, IPersonsLogic personsLogic,
			IUsersLogic usersLogic)
		{
			_context = context;
			GroupsLogic = groupsLogic;
			PersonsLogic = personsLogic;
			UsersLogic = usersLogic;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool canDispose)
		{
			if (_isDisposed)
				return;

			if (canDispose)
				_context.Dispose();

			_isDisposed = true;
		}
	}
}