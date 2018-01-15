using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common.Extensions;
using MathSite.Facades.FileSystem;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public interface IFilesManagerViewModelBuilder
    {
        Task<IndexFilesViewModel> BuildIndexViewModelAsync(string directory = "/");
    }

    public class FilesManagerViewModelBuilder : AdminPageBaseViewModelBuilder, IFilesManagerViewModelBuilder
    {
        private readonly IDirectoryFacade _directoryFacade;

        public FilesManagerViewModelBuilder(ISiteSettingsFacade siteSettingsFacade, IDirectoryFacade directoryFacade) :
            base(siteSettingsFacade)
        {
            _directoryFacade = directoryFacade;
        }

        public async Task<IndexFilesViewModel> BuildIndexViewModelAsync(string directory = "/")
        {
            var model = await BuildAdminBaseViewModelAsync<IndexFilesViewModel>(
                link => link.Alias == "Files",
                link => link.Alias == "List"
            );

            var (directories, files) = await GetAllItemsInDirectoryAsync(directory);

            model.Directories = directories;
            model.Files = files;

            return model;
        }

        private async Task<(IEnumerable<DirectoryViewModel> Directories, IEnumerable<FileViewModel>)> GetAllItemsInDirectoryAsync(string path)
        {
            var all = await _directoryFacade.GetDirectoryWithPathAsync(path);

            var backdir = GetBackDirectoryModel(all, path);

            var dirsWithBackDir = new List<DirectoryViewModel>();

            if(backdir.IsNotNull())
                dirsWithBackDir.Add(backdir);

            var dirs = all.Directories.Select(directory => new DirectoryViewModel
            {
                CreationDate = GetDateString(directory.CreationDate),
                Title = directory.Name,
                Id = directory.Id.ToString(),
                FullPath = Path.Combine(path, directory.Name)
            });

            dirsWithBackDir.AddRange(dirs);

            var files = all.Files.Select(file => new FileViewModel
            {
                CreationDate = GetDateString(file.CreationDate),
                Id = file.Id.ToString(),
                Title = $"{file.Name}{file.Extension}",
                FullFilePath = Path.Combine(path, $"{file.Name}{file.Extension}")
            });

            return (dirsWithBackDir, files);
        }

        private DirectoryViewModel GetBackDirectoryModel(Entities.Directory currenetDirectory, string path)
        {
            var realPath = path.Substring(1);

            if (realPath.IsNullOrWhiteSpace())
                return null;

            var paths = new List<string>{ "" };

            var backdirPathSections = realPath.Split(new []{ '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s != currenetDirectory.Name)
                .ToArray();
            
            paths.AddRange(backdirPathSections);

            var backdirPath = paths
                .Aggregate((f, s) => $"{f}{Path.DirectorySeparatorChar}{s}");

            return new DirectoryViewModel
            {
                Id = currenetDirectory.Id.ToString(),
                CreationDate = GetDateString(currenetDirectory.CreationDate),
                FullPath = backdirPath,
                Title = ".."
            };
        }

        private string GetDateString(DateTime date)
        {
            return date.ToString("F");
        }

        protected override async Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return new List<MenuLink>();
        }
    }
}