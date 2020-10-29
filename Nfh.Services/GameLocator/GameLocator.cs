using Nfh.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Nfh.Services
{
    public class GameLocator : IGameLocator
    {
        private readonly string GameName = "Neighbours from Hell";

        private IReadOnlyCollection<IGameLocationProvider> gameLocationProviders
            = new List<IGameLocationProvider>() { new SteamGameLocationProvider() };

        public IEnumerable<string> GetGameLocations() => 
            gameLocationProviders.Select(p => p.Locate(GameName).FullName);
    }
}
