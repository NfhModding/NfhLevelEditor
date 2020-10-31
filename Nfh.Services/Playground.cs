using Nfh.Services.BackupServices;
using System.Collections.Generic;
using System.Linq;

namespace Nfh.Services
{
    public static class Playground
    {
        public static void BackupGameData()
        {
            var gameLocator = new GameLocator(new List<IGameLocationProvider> { new SteamGameLocationProvider(), });
            var gamePath = gameLocator.GetGameLocations().First();

            var backupService = new BackupService(new ApplicationWorkFolder());
            backupService.RestoreGameData(gamePath);
        }
    }
}
