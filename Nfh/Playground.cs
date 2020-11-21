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

        public Playground(IGameLocator gameLocator, IProjectService projectService)
        {
            this.gameLocator = gameLocator;
            this.projectService = projectService;
        }

        public void Run()
        {
            var location = gameLocator.GetGameLocations().First();
            var projectPath = Path.Combine(@"D:", "TMP");
            projectService.CreateProject(location, projectPath);
            projectService.ListProjects(Path.Combine(@"D:\\")).Print();

            var seasonPack = projectService.LoadSeasonPack(projectPath);
            foreach (var season in seasonPack.Seasons.Values.Select(v => v.Season))
            {
                season.Unlocked = true;
                foreach (var level in season.Levels.Values.Select(v => v.Level))
                {
                    level.TrickCount = 69;
                    level.Description.Title = "Modded!";
                }
            }

            projectService.SaveSeasonPack(seasonPack, projectPath);

            projectService.PatchGame(projectPath, location);
            projectService.DeleteProject(projectPath);
        }
    }
}
