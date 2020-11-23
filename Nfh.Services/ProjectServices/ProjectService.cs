using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using Nfh.Services.Common;
using Nfh.Services.ProjectServices.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Nfh.Services.ProjectServices
{
    internal class ProjectService : IProjectService
    {
        private readonly IFolderHelper folderHelper;
        private readonly IZipHelper zipHelper;
        private readonly ISeasonPackLoader seasonPackLoader;
        private readonly ILevelLoader levelLoader;

        public ProjectService(
            IFolderHelper folderHelper, 
            IZipHelper zipHelper, 
            ISeasonPackLoader seasonPackLoader,
            ILevelLoader levelLoader)
        {
            this.folderHelper = folderHelper;
            this.zipHelper = zipHelper;
            this.seasonPackLoader = seasonPackLoader;
            this.levelLoader = levelLoader;
        }

        public void CreateProject(string gameSourcePath, string targetProjectPath)
        {
            if (!Directory.Exists(gameSourcePath))
                return;

            var projectFolder = new DirectoryInfo(targetProjectPath);
            if (!projectFolder.Exists)
                projectFolder.Create();

            var gamedataFolder = createGamedataDirectoryInfo(projectFolder);
            var dataFolder = folderHelper.GetGamesDataFolder(gameSourcePath);
            ZipFile.ExtractToDirectory(Path.Combine(dataFolder.FullName, "gamedata.bnd"), gamedataFolder.FullName, overwriteFiles: true);
        }

        public void DeleteProject(string targetProjectPath)
        {
            if (!Directory.Exists(targetProjectPath))
                return;

            if (!isValidProjectFolder(new(targetProjectPath)))
                return;

            Directory.Delete(targetProjectPath, recursive: true);
        }

        public IList<string> ListProjects(string rootFolderPath)
        {
            var rootFolder = new DirectoryInfo(rootFolderPath);
            if (!rootFolder.Exists)
                return Array.Empty<string>();

            return rootFolder.EnumerateDirectories(
                    searchPattern: "*", 
                    enumerationOptions: new EnumerationOptions { ReturnSpecialDirectories = false, RecurseSubdirectories = false })
                .Select(subfolder => subfolder)
                .Where(isValidProjectFolder)
                .Select(f => f.FullName)
                .ToList();
        }

        public void PatchGame(string sourceProjectPath, string targetGamePath)
        {
            var sourceProjectFolder = new DirectoryInfo(sourceProjectPath);
            if (!sourceProjectFolder.Exists || !isValidProjectFolder(sourceProjectFolder))
                return;

            if (!Directory.Exists(targetGamePath))
                return;

            var targetFileName = Path.Combine(folderHelper.GetGamesDataFolder(targetGamePath).FullName, "gamedata.bnd");
            zipHelper.CreateZipFromFolder(createGamedataDirectoryInfo(sourceProjectFolder).FullName, targetFileName, overrideFile: true);
        }

        public SeasonPack LoadSeasonPack(string sourcePath)
        {
            var projectFolder = new DirectoryInfo(sourcePath);
            if (!projectFolder.Exists)
                throw new();

            return seasonPackLoader.Load(createGamedataDirectoryInfo(projectFolder));
        }

        public void SaveSeasonPack(SeasonPack seasonPack, string targetPath)
        {
            var projectFolder = new DirectoryInfo(targetPath);
            if (!projectFolder.Exists)
                throw new();

            seasonPackLoader.Save(seasonPack, createGamedataDirectoryInfo(projectFolder));
        }

        public Level LoadLevel(string sourcePath, string levelId)
        {
            var projectFolder = new DirectoryInfo(sourcePath);
            if (!projectFolder.Exists)
                throw new();

            return levelLoader.Load(createGamedataDirectoryInfo(projectFolder), levelId);
        }

        public void SaveLevel(Level level, string targetPath)
        {
            var projectFolder = new DirectoryInfo(targetPath);
            if (!projectFolder.Exists)
                throw new();

            levelLoader.Save(createGamedataDirectoryInfo(projectFolder), level);
        }

        private bool isValidProjectFolder(DirectoryInfo projectFolder)
        {
            var gamedataFolder = createGamedataDirectoryInfo(projectFolder);
            if (!gamedataFolder.Exists)
                return false;

            return gamedataFolder
                .EnumerateFiles(searchPattern: "*.xml", searchOption: SearchOption.TopDirectoryOnly)
                .Any(f => f.Name == "leveldata.xml");
        }

        private DirectoryInfo createGamedataDirectoryInfo(DirectoryInfo projectFolder) =>
            new DirectoryInfo(Path.Combine(projectFolder.FullName, "gamedata"));
               
    }
}
