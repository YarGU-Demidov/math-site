using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Importer;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
using MathSite.Facades.Persons;
using MathSite.Facades.Posts;
using MathSite.Facades.SiteSettings;
using MathSite.Facades.Users;
using MathSite.Facades.UserValidation;
using MathSite.Repository;
using MathSite.Repository.Core;
using MathSite.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using File = System.IO.File;

namespace NewsImporter
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            if (args.Length != 1)
                throw new ArgumentException("You should pass only 1 argument: connection string!", nameof(args));

            var filePath = new[]
            {
                Path.Combine(Environment.CurrentDirectory, "news.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "NewsImporter", "news.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "..", "NewsImporter", "news.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "NewsImporter", "news.json")
            }.Where(File.Exists).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(filePath))
                throw new FileNotFoundException("Need file with data.");

            var posts = JsonConvert.DeserializeObject<ICollection<RainlabBlogPost>>(File.ReadAllText(filePath));

            var options = new DbContextOptionsBuilder()
                .UseNpgsql(args[0], builder => builder.MigrationsAssembly("MathSite").EnableRetryOnFailure(2))
                .Options;

            using (var context = new MathSiteDbContext(options))
            {
                await Process(context, posts);
            }
        }

        private static async Task Process(MathSiteDbContext context, ICollection<RainlabBlogPost> posts)
        {
            var manager = new RepositoryManager(
                new GroupsRepository(context),
                new PersonsRepository(context),
                new UsersRepository(context),
                new FilesRepository(context),
                new SiteSettingsRepository(context),
                new RightsRepository(context),
                new PostsRepository(context),
                new PostSeoSettingsRepository(context),
                new PostSettingRepository(context),
                new PostTypeRepository(context),
                new GroupTypeRepository(context),
                new DirectoriesRepository(context),
                new CategoryRepository(context),
                new ProfessorsRepository(context),
                new PostCategoryRepository(context)
            );

            var loggerFactory = new LoggerFactory().AddConsole();
            
            var passwordsManager = new DoubleSha512HashPasswordsManager();

            var userValidation = new UserValidationFacade(
                manager,
                passwordsManager
            );
            
            var usersFacade = new UsersFacade(
                manager, 
                userValidation, 
                passwordsManager
            );

            var settings = new SiteSettingsFacade(
                manager,
                userValidation,
                usersFacade
            );

            var postsFacade = new PostsFacade(
                manager,
                settings,
                loggerFactory.CreateLogger<IPostsFacade>(),
                userValidation,
                usersFacade
            );

            await UpdateData(postsFacade, manager, posts);
        }

        private static async Task UpdateData(IPostsFacade postsFacade, IRepositoryManager manager,
            ICollection<RainlabBlogPost> posts)
        {
            foreach (var post in posts)
            {
                var newPostId = await postsFacade.CreatePostAsync(
                    await ConvertToPost(post, manager.UsersRepository, manager.PostTypeRepository)
                );

                if (newPostId == Guid.Empty)
                    throw new ApplicationException("Something went wrong. Check exception above.");
            }
        }

        private static async Task<Post> ConvertToPost(RainlabBlogPost oldPost, IUsersRepository usersRepository,
            IPostTypeRepository postTypeRepository)
        {
            return new Post
            {
                AuthorId =
                    (await usersRepository.FirstOrDefaultAsync(user => user.Login == UsersAliases.Mokeev1995)).Id,
                Content = oldPost.ContentHtml,
                Excerpt = oldPost.Excerpt,
                Title = oldPost.Title,
                Published = oldPost.Published,
                PublishDate = oldPost.PublishedAt?.UtcDateTime ?? DateTime.UtcNow,
                CreationDate = oldPost.CreatedAt?.UtcDateTime ?? DateTime.UtcNow,
                PostType = await postTypeRepository.FirstOrDefaultAsync(
                    new SameAliasSpecification<PostType>(PostTypeAliases.News)),
                PostSettings = ConvertToPostSetting(oldPost),
                PostSeoSetting = ConvertToPostSeoSettings(oldPost)
            };
        }

        private static PostSeoSetting ConvertToPostSeoSettings(RainlabBlogPost oldPost)
        {
            return new PostSeoSetting
            {
                Title = oldPost.Title,
                Description = oldPost.Excerpt,
                Url = oldPost.Slug
            };
        }

        private static PostSetting ConvertToPostSetting(RainlabBlogPost oldPost)
        {
            return new PostSetting
            {
                CanBeRated = false,
                IsCommentsAllowed = true,
                PostOnStartPage = oldPost.FrontPageVisible
            };
        }
    }
}