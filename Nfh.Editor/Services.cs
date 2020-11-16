using Image.Tga;
using Mvvm.Framework.UndoRedo;
using Mvvm.Framework.ViewModel;
using Nfh.Domain.Interfaces;
using Nfh.Domain.Models.InGame;
using Nfh.Domain.Models.Meta;
using Nfh.Editor.Commands.ModelCommands;
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
                TgaImage.FromFile($"C:/TMP/NeighborsFromHell_Assets/{frameName}").ToBitmap();

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

            public Level LoadLevel(string sourcePath, string levelId)
            {
                var result = new Level { Meta = GenerateLevelMeta(levelId) };
                for (int i = 0; i < 3; ++i)
                {
                    var ob = MakeLevelObject(i, 0);
                    result.Objects.Add(ob.Id, ob);
                }
                for (int i = 0; i < 3; ++i)
                {
                    var room = MakeRoom(i, i);
                    result.Rooms.Add(room.Id, room);
                }
                return result;
            }

            private Room MakeRoom(int id, int layer)
            {
                var result = new Room($"r_{id}");
                for (int i = 0; i < 15; ++i)
                {
                    var obj = MakeLevelObject(i, layer);
                    result.Objects.Add(obj.Id, obj);
                }
                return result;
            }

            private LevelObject MakeLevelObject(int seed, int layer)
            {
                string id = $"lo_{seed}";
                if (seed % 3 == 0)
                {
                    return new Door(id)
                    {
                        Position = new Point(seed * 40, 30 + layer * 30),
                        Visuals = MakeVisuals('d'),
                    };
                }
                else if (seed % 7 == 0)
                {
                    return new Actor(id)
                    {
                        Position = new Point(seed * 40, 150 + layer * 30),
                        Visuals = MakeVisuals('a'),
                    };
                }
                else
                {
                    return new LevelObject(id)
                    {
                        Position = new Point(seed * 40, 250 + layer * 30),
                        Visuals = MakeVisuals(' '),
                    };
                }
            }

            private Visuals MakeVisuals(char c)
            {
                string path = c == 'a' ? "W_triumph_0000.tga" : c == 'd' ? "W_leave_0009.tga" : "W_Hide_Wardrobe_0019.tga";
                var visuals = new Visuals();
                var standing = new Animation("ms");
                standing.Frames.Add(new Animation.Frame
                {
                    ImageOffset = new Point(0, 0),
                    ImagePath = path,
                });
                visuals.Animations.Add(standing.Id, standing);
                return visuals;
            }

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
        public static IUndoRedoStack UndoRedo { get; } = new UndoRedoStack
        {
            MergeStrategy = new CommandMergeStrategy(),
        };
    }
}
