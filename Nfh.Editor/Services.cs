﻿using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                new Bitmap("C:/TMP/NfhModdingLogo.png");
        }

        // TODO: Stub
        private class ProjectServide : IProjectService
        {
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
            }

            public void PatchGame(string sourceProjectPath, string targetGamePath) =>
                throw new NotImplementedException();

            public void SaveLevel(Level level, string targetPath) =>
                throw new NotImplementedException();

            public void SaveSeasonPack(SeasonPack seasonPack, string targetPath) =>
                throw new NotImplementedException();
        }

        // Backend
        public static string GamePath { get; }

        public static IBackupService Backup { get; }
        public static IGameLocator GameLocator { get; }
        public static IImageService Image { get; } = new TempImageService();
        public static IProjectService Project { get; }

        // Frontend
        public static IModelChangeNotifier ModelChangeNotifier { get; } = new ModelChangeNotifier();

        static Services()
        {
            GamePath = LocateGame();
        }

        // TODO: Temporary stub
        private static string LocateGame() => "C:/TMP";
    }
}