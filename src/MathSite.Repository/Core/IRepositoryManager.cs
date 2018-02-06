namespace MathSite.Repository.Core
{
    public interface IRepositoryManager
    {
        IGroupsRepository GroupsRepository { get; }
        IPersonsRepository PersonsRepository { get; }
        IUsersRepository UsersRepository { get; }
        IFilesRepository FilesRepository { get; }
        IDirectoriesRepository DirectoriesRepository { get; }
        ISiteSettingsRepository SiteSettingsRepository { get; }
        IRightsRepository RightsRepository { get; }
        IPostsRepository PostsRepository { get; }
        IPostSeoSettingsRepository PostSeoSettingsRepository { get; }
        IPostSettingRepository PostSettingRepository { get; }
        IPostTypeRepository PostTypeRepository { get; }
        IGroupTypeRepository GroupTypeRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        T TryGetRepository<T>() 
            where T: class, IRepository;
    }
}