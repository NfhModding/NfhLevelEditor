﻿using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Nfh.Editor
{
    internal static class Services
    {
        // TODO: Stub
        private class TempImageService : IImageService
        {
            public Bitmap LoadAnimationFrame(string objectId, string frameName, string gamePath) =>
                throw new NotImplementedException();

            public Bitmap LoadLevelThumbnail(string levelId, string gamePath) =>
                new Bitmap("c:/TMP/NeighborsFromHell_Assets/tutorial_1.png");
        }

        // TODO: Stub
        private class ProjectService : IProjectService
        {
            public string? DefaultWorkDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public void CreateProject(string gameSourcePath, string targetProjectPath) =>
                throw new NotImplementedException();

            public void DeleteProject(string targetProjectPath) =>
                throw new NotImplementedException();

            public IList<string> ListProjects(string rootFolder) =>
                throw new NotImplementedException();

            public Level LoadLevel(string sourcePath, string levelId) =>
                throw new NotImplementedException();

            public SeasonPack LoadSeasonPack(string sourcePath)
            {
                var sp = new SeasonPack();
                for (int i = 0; i < 4; ++i)
                {
                    var s = GenerateSeason($"set{i}");
                    sp.Seasons.Add(s.Id, (s, i));
                }
                return sp;
            }

            private static Season GenerateSeason(string id)
            {
                var s = new Season(id);
                s.Unlocked = true;
                for (int i = 0; i < (id == "set0" ? 3 : id == "set1" ? 6 : 4); ++i)
                {
                    var l = GenerateLevelMeta($"level{i}");
                    s.Levels.Add(l.Id, (l, i));
                }
                return s;
            }

            private static LevelMeta GenerateLevelMeta(string id)
            {
                var meta = new LevelMeta(id)
                {
                    Description = new LevelDescription
                    {
                        Description = @"Basic movement tutorial.

Do as I say lol.",
                        Hint = "Playing several tricks one after the other makes for better ratings!",
                        ThumbnailDescription = "Woody, our hero",
                        Title = $"The basics",
                    },
                    MinPercent = 75,
                    TimeLimit = new TimeSpan(0, 0, 10, 0, 0),
                    TrickCount = 7,
                    Unlocked = true,
                };
                return meta;
            }

            public void PatchGame(string sourceProjectPath, string targetGamePath) =>
                throw new NotImplementedException();

            public void SaveLevel(Level level, string targetPath) =>
                throw new NotImplementedException();

            public void SaveSeasonPack(SeasonPack seasonPack, string targetPath) =>
                throw new NotImplementedException();
        }

        // TODO: Stub
        private class GameLocatorA : IGameLocator
        {
            public IEnumerable<string> GetGameLocations()
            {
                yield return "C:/Nfh";
            }
        }

        // TODO: Stub
        private class BackupService : IBackupService
        {
            public bool BackupExists => true;

            public void BackupGameData(string sourceGamePath)
            {
            }

            public void RestoreGameData(string targetGamePath)
            {
            }
        }

        // Backend
        public static string? GamePath { get; set; }

        public static IBackupService Backup { get; } = new BackupService();
        public static IGameLocator GameLocator { get; } = new GameLocatorA();
        public static IImageService Image { get; } = new TempImageService();
        public static IProjectService Project { get; } = new ProjectService();

        // Frontend
        public static IModelChangeNotifier ModelChangeNotifier { get; } = new ModelChangeNotifier();
        public static IUndoRedoStack UndoRedo { get; } = new UndoRedoStack();
    }
}
