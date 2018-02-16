﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathSite.BasicAdmin.ViewModels.SharedModels.AdminPagesViewModel;
using MathSite.BasicAdmin.ViewModels.SharedModels.Menu;
using MathSite.Common.Extensions;
using MathSite.Entities;
using MathSite.Facades.FileSystem;
using MathSite.Facades.SiteSettings;

namespace MathSite.BasicAdmin.ViewModels.Files
{
    public interface IFilesManagerViewModelBuilder
    {
        Task<IndexFilesViewModel> BuildIndexViewModelAsync(string directory = "/");
        Task<UploadedFilesViewModel> BuildUploadedViewModelAsync(User currentUser, string fileName, Stream fileStream, string directory = "/");
    }

    public class FilesManagerViewModelBuilder : AdminPageBaseViewModelBuilder, IFilesManagerViewModelBuilder
    {
        private readonly IFileFacade _fileFacade;
        private readonly IDirectoryFacade _directoryFacade;

        public FilesManagerViewModelBuilder(
            ISiteSettingsFacade siteSettingsFacade, 
            IFileFacade fileFacade, 
            IDirectoryFacade directoryFacade
        ) : base(siteSettingsFacade)
        {
            _fileFacade = fileFacade;
            _directoryFacade = directoryFacade;
        }

        public async Task<IndexFilesViewModel> BuildIndexViewModelAsync(string directory = "/")
        {
            var model = await BuildAdminBaseViewModelAsync<IndexFilesViewModel>(
                link => link.Alias == "Files"
            );

            var (directories, files) = await GetAllItemsInDirectoryAsync(directory);

            model.Directories = directories;
            model.Files = files;
            model.CurrentDirectory = directory;

            return model;
        }

        public async Task<UploadedFilesViewModel> BuildUploadedViewModelAsync(User currentUser, string fileName, Stream fileStream, string directory = "/")
        {
            var model = await BuildAdminBaseViewModelAsync<UploadedFilesViewModel>(
                link => link.Alias == "Files"
            );

            var fileId = await _fileFacade.SaveFileAsync(currentUser, fileName, fileStream);

            model.FileName = fileName;
            model.FileId = fileId.ToString();

            return model;
        }

        private async Task<(IEnumerable<DirectoryViewModel> Directories, IEnumerable<FileViewModel> Files)> GetAllItemsInDirectoryAsync(string path)
        {
            if (path.IsNotNullOrWhiteSpace() && path[0] != Path.DirectorySeparatorChar)
            {
                path = new StringBuilder(path).Replace(path[0], Path.DirectorySeparatorChar, 0, 1).ToString();
            }

            var all = await _directoryFacade.GetDirectoryWithPathAsync(path);

            var backdir = GetBackDirectoryModel(all, path);

            var dirsWithBackDir = new List<DirectoryViewModel>();

            if(backdir.IsNotNull())
                dirsWithBackDir.Add(backdir);

            var dirs = all.Directories.Select(directory => new DirectoryViewModel
            {
                CreationDate = GetDateString(directory.CreationDate),
                Id = directory.Id.ToString(),
                Title = directory.Name,
                FullPath = Path.Combine(path, directory.Name)
            });

            dirsWithBackDir.AddRange(dirs);

            var files = all.Files.Select(file => new FileViewModel
            {
                CreationDate = GetDateString(file.CreationDate),
                Id = file.Id.ToString(),
                Title = file.Name,
                FullFilePath = Path.Combine(path, file.Name)
            });

            return (dirsWithBackDir, files);
        }

        private DirectoryViewModel GetBackDirectoryModel(Entities.Directory currenetDirectory, string path)
        {
            var realPath = path.Substring(1);

            if (realPath.IsNullOrWhiteSpace())
                return null;

            var paths = new List<string>{ "" };

            var backdirPathSections = realPath.Split(new []{ '/', '\\' }, StringSplitOptions.RemoveEmptyEntries)
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

        protected override Task<IEnumerable<MenuLink>> GetLeftMenuLinks()
        {
            return Task.FromResult(Enumerable.Empty<MenuLink>());
        }
    }
}