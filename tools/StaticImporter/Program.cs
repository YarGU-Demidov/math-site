using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.Common.Crypto;
using MathSite.Db;
using MathSite.Db.DataSeeding.StaticData;
using MathSite.Entities;
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
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace StaticImporter
{
    public static class Program
    {
        private static IEnumerable<string> GetFilesContent(string dir)
        {
            return Directory.GetFiles(dir, "*.htm").Select(file => File.ReadAllText(file, Encoding.UTF8));
        }

        private static StaticPageModel ConvertFileToModel(string fileContent)
        {
            var splitedValues = fileContent.Split("==", StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim())
                .ToArray();

            var model = new StaticPageModel();

            var header = splitedValues[0]
                .Replace("[viewBag]", "")
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Replace("\"", ""))
                .Select(s => s.Split("=", StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(strings => strings[0].Trim(), strings => strings[1].Trim());

            model.Title = header["title"];
            model.Url = header["url"].Remove(0, 1);
            model.Content = splitedValues[1];

            return model;
        }

        private static void GenerateJson()
        {
            var dir = $"{Environment.CurrentDirectory}/static-pages";

            if (!Directory.Exists(dir))
                throw new DirectoryNotFoundException($"Directory '{dir}' not found!");

            var models = GetFilesContent(dir)
                .Select(ConvertFileToModel);

            var json = JsonConvert.SerializeObject(models, Formatting.Indented);

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "static-pages.json"), json);
        }

        public static async Task Main(string[] args)
        {
            if (args.Any(s => s.ToLower() == "generate"))
            {
                var tmp = new List<string>(args);
                tmp.RemoveAll(s => s.ToLower() == "generate");
                args = tmp.ToArray();
                GenerateJson();
            }

            if (args.Length != 1)
                throw new ArgumentException("You should pass connection string!", nameof(args));

            var filePath = new[]
            {
                Path.Combine(Environment.CurrentDirectory, "static-pages.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "StaticImporter", "static-pages.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "..", "StaticImporter", "static-pages.json"),
                Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "StaticImporter", "static-pages.json")
            }.Where(File.Exists).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(filePath))
                throw new FileNotFoundException("Need file with data.");

            var posts = JsonConvert.DeserializeObject<ICollection<StaticPageModel>>(File.ReadAllText(filePath));

            var options = new DbContextOptionsBuilder()
                .UseNpgsql(args[0], builder => builder.MigrationsAssembly("MathSite").EnableRetryOnFailure(2))
                .Options;

            using (var context = new MathSiteDbContext(options))
            {
                await Process(context, posts);
            }
        }

        private static async Task Process(MathSiteDbContext context, IEnumerable<StaticPageModel> posts)
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
                new CategoryRepository(context)
            );

            var loggerFactory = new LoggerFactory().AddConsole();

            var memCache = new MemoryCache(new MemoryCacheOptions());

            var userValidation = new UserValidationFacade(
                manager,
                memCache,
                new DoubleSha512HashPasswordsManager()
            );

            var usersFacade = new UsersFacade(manager, memCache);

            var settings = new SiteSettingsFacade(
                manager,
                userValidation,
                memCache,
                usersFacade
            );

            var postsFacade = new PostsFacade(
                manager,
                memCache,
                settings,
                loggerFactory.CreateLogger<IPostsFacade>(),
                userValidation,
                usersFacade
            );

            await UpdateData(postsFacade, manager, posts);
        }


        private static async Task UpdateData(IPostsFacade postsFacade, IRepositoryManager manager,
            IEnumerable<StaticPageModel> posts)
        {
            foreach (var post in posts)
            {
                var newPostId = await postsFacade.CreatePostAsync(
                    await ConvertToPost(post, manager.UsersRepository, manager.PostTypeRepository));

                if (newPostId == Guid.Empty)
                    throw new ApplicationException("Something went wrong. Check exception above.");
            }
        }

        private static async Task<Post> ConvertToPost(StaticPageModel oldPost, IUsersRepository usersRepository,
            IPostTypeRepository postTypeRepository)
        {
            return new Post
            {
                AuthorId =
                    (await usersRepository.FirstOrDefaultAsync(user => user.Login == UsersAliases.Mokeev1995)).Id,
                Content = oldPost.Content,
                Excerpt = oldPost.Content.Length > 50 ? $"{oldPost.Content.Substring(0, 47)}..." : oldPost.Content,
                Title = oldPost.Title,
                Published = true,
                PublishDate = new DateTime(2017, 03, 01),
                CreationDate = new DateTime(2017, 03, 01),
                PostType = await postTypeRepository.FirstOrDefaultAsync(
                    new SameAliasSpecification<PostType>(PostTypeAliases.StaticPage)),
                PostSettings = ConvertToPostSetting(oldPost),
                PostSeoSetting = ConvertToPostSeoSettings(oldPost)
            };
        }

        private static PostSeoSetting ConvertToPostSeoSettings(StaticPageModel oldPost)
        {
            return new PostSeoSetting
            {
                Title = oldPost.Title,
                Description = oldPost.Content.Length > 50 ? $"{oldPost.Content.Substring(0, 47)}..." : oldPost.Content,
                Url = oldPost.Url
            };
        }

        private static PostSetting ConvertToPostSetting(StaticPageModel oldPost)
        {
            return new PostSetting
            {
                CanBeRated = false,
                IsCommentsAllowed = true,
                PostOnStartPage = true
            };
        }
    }

    public class StaticPageModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
