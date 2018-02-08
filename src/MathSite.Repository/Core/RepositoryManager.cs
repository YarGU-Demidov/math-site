using System.Collections.Generic;
using System.Linq;

// ReSharper disable SuggestBaseTypeForParameter

namespace MathSite.Repository.Core
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ICollection<IRepository> _repositories = new List<IRepository>();

        public RepositoryManager(
            IGroupsRepository groupsRepository,
            IPersonsRepository personsRepository,
            IUsersRepository usersRepository,
            IFilesRepository filesRepository,
            ISiteSettingsRepository siteSettingsRepository,
            IRightsRepository rightsRepository,
            IPostsRepository postsRepository,
            IPostSeoSettingsRepository postSeoSettingsRepository,
            IPostSettingRepository postSettingRepository,
            IPostTypeRepository postTypeRepository,
            IGroupTypeRepository groupTypeRepository,
            IDirectoriesRepository directoriesRepository,
            ICategoryRepository categoryRepository
        )
        {
            _repositories.Add(groupsRepository);
            _repositories.Add(personsRepository);
            _repositories.Add(usersRepository);
            _repositories.Add(filesRepository);
            _repositories.Add(siteSettingsRepository);
            _repositories.Add(rightsRepository);
            _repositories.Add(postsRepository);
            _repositories.Add(postSeoSettingsRepository);
            _repositories.Add(postSettingRepository);
            _repositories.Add(postTypeRepository);
            _repositories.Add(groupTypeRepository);
            _repositories.Add(directoriesRepository);
            _repositories.Add(categoryRepository);
        }

        public IGroupsRepository GroupsRepository => TryGetRepository<IGroupsRepository>();
        public IPersonsRepository PersonsRepository => TryGetRepository<IPersonsRepository>();
        public IUsersRepository UsersRepository => TryGetRepository<IUsersRepository>();
        public IFilesRepository FilesRepository => TryGetRepository<IFilesRepository>();
        public IDirectoriesRepository DirectoriesRepository => TryGetRepository<IDirectoriesRepository>();
        public ISiteSettingsRepository SiteSettingsRepository => TryGetRepository<ISiteSettingsRepository>();
        public IRightsRepository RightsRepository => TryGetRepository<IRightsRepository>();
        public IPostsRepository PostsRepository => TryGetRepository<IPostsRepository>();
        public IPostSeoSettingsRepository PostSeoSettingsRepository => TryGetRepository<IPostSeoSettingsRepository>();
        public IPostSettingRepository PostSettingRepository => TryGetRepository<IPostSettingRepository>();
        public IPostTypeRepository PostTypeRepository => TryGetRepository<IPostTypeRepository>();
        public IGroupTypeRepository GroupTypeRepository => TryGetRepository<IGroupTypeRepository>();
        public ICategoryRepository CategoryRepository => TryGetRepository<ICategoryRepository>();

        public T TryGetRepository<T>() where T : class, IRepository
        {
            return _repositories.First(repository => repository is T) as T;
        }
    }
}