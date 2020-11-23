using Nfh.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Nfh
{
    internal static class StringExtensions
    {
        public static void Print(this IList<string> strings)
        {
            foreach (var str in strings)
                Console.WriteLine(str);
        }
    }

    internal class Playground
    {
        private readonly IGameLocator gameLocator;
        private readonly IProjectService projectService;
        private readonly IImageService imageService;

        public Playground(IGameLocator gameLocator, IProjectService projectService, IImageService imageService)
        {
            this.gameLocator = gameLocator;
            this.projectService = projectService;
            this.imageService = imageService;
        }

        public void Run()
        {
            var location = gameLocator.GetGameLocations().First();
            var projectPath = Path.Combine(@"D:", "TMP");
            projectService.CreateProject(location, projectPath);
            projectService.ListProjects(Path.Combine(@"D:\\")).Print();

            var seasonPack = projectService.LoadSeasonPack(projectPath);
            var firstLevelMeta = seasonPack.Seasons.Values.Select(v => v.Season).First().Levels.Values.Select(v => v.Level).First();

            var firstLevel = projectService.LoadLevel(projectPath, firstLevelMeta.Id);

            var gfxTmp = new DirectoryInfo(Path.Combine(@"D:", "GFX_TMP"));
            gfxTmp.Create();

            imageService.LoadLevelThumbnail(firstLevel.Id, location).Save(Path.Combine(gfxTmp.FullName, "thumbnal.png"));

            var obj = firstLevel.Objects["house"];
            imageService.LoadAnimationFrame(obj.Id, obj.Visuals?.Animations.Values.First().Frames.First().ImagePath, location).Save(Path.Combine(gfxTmp.FullName, "frame.png"));

            // projectService.PatchGame(projectPath, location);
            //projectService.DeleteProject(projectPath);
        }
    }
}
