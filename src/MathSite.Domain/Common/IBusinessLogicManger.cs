using System;
using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Rights;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.Logic.Users;

namespace MathSite.Domain.Common
{
	public interface IBusinessLogicManger : IDisposable
	{
		IGroupsLogic GroupsLogic { get; }
		IPersonsLogic PersonsLogic { get; }
		IUsersLogic UsersLogic { get; }
		IFilesLogic FilesLogic { get; }
		ISiteSettingsLogic SiteSettingsLogic { get; }
		IRightsLogic RightsLogic { get; }
	}
}