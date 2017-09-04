using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Rights;
using MathSite.Domain.Logic.SiteSettings;
using MathSite.Domain.Logic.Users;

namespace MathSite.Domain.Common
{
	public class BusinessLogicManager : IBusinessLogicManager
	{
		public BusinessLogicManager(
			IGroupsLogic groupsLogic,
			IPersonsLogic personsLogic,
			IUsersLogic usersLogic,
			IFilesLogic filesLogic,
			ISiteSettingsLogic siteSettingsLogic,
			IRightsLogic rightsLogic
		)
		{
			GroupsLogic = groupsLogic;
			PersonsLogic = personsLogic;
			UsersLogic = usersLogic;
			FilesLogic = filesLogic;
			SiteSettingsLogic = siteSettingsLogic;
			RightsLogic = rightsLogic;
		}

		public IGroupsLogic GroupsLogic { get; }
		public IPersonsLogic PersonsLogic { get; }
		public IUsersLogic UsersLogic { get; }
		public IFilesLogic FilesLogic { get; }
		public ISiteSettingsLogic SiteSettingsLogic { get; }
		public IRightsLogic RightsLogic { get; }
	}
}