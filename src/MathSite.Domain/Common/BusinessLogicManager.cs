using MathSite.Domain.Logic.Files;
using MathSite.Domain.Logic.Groups;
using MathSite.Domain.Logic.Persons;
using MathSite.Domain.Logic.Posts;
using MathSite.Domain.Logic.PostSeoSettings;
using MathSite.Domain.Logic.PostSettings;
using MathSite.Domain.Logic.PostTypes;
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
            IRightsLogic rightsLogic,
            IPostsLogic postsLogic,
            IPostSeoSettingsLogic postSeoSettingsLogic,
            IPostSettingLogic postSettingLogic,
            IPostTypeLogic postTypeLogic
        )
        {
            GroupsLogic = groupsLogic;
            PersonsLogic = personsLogic;
            UsersLogic = usersLogic;
            FilesLogic = filesLogic;
            SiteSettingsLogic = siteSettingsLogic;
            RightsLogic = rightsLogic;
            PostsLogic = postsLogic;
            PostSeoSettingsLogic = postSeoSettingsLogic;
            PostSettingLogic = postSettingLogic;
            PostTypeLogic = postTypeLogic;
        }

        public IGroupsLogic GroupsLogic { get; }
        public IPersonsLogic PersonsLogic { get; }
        public IUsersLogic UsersLogic { get; }
        public IFilesLogic FilesLogic { get; }
        public ISiteSettingsLogic SiteSettingsLogic { get; }
        public IRightsLogic RightsLogic { get; }
        public IPostsLogic PostsLogic { get; }
        public IPostSeoSettingsLogic PostSeoSettingsLogic { get; }
        public IPostSettingLogic PostSettingLogic { get; }
        public IPostTypeLogic PostTypeLogic { get; }
    }
}