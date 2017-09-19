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
    public interface IBusinessLogicManager
    {
        IGroupsLogic GroupsLogic { get; }
        IPersonsLogic PersonsLogic { get; }
        IUsersLogic UsersLogic { get; }
        IFilesLogic FilesLogic { get; }
        ISiteSettingsLogic SiteSettingsLogic { get; }
        IRightsLogic RightsLogic { get; }
        IPostsLogic PostsLogic { get; }
        IPostSeoSettingsLogic PostSeoSettingsLogic { get; }
        IPostSettingLogic PostSettingLogic { get; }
        IPostTypeLogic PostTypeLogic { get; }
    }
}