using Nfh.Domain;
using Nfh.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Nfh.Services
{
    internal class GameLocator : IGameLocator
    {
        private readonly string GameName = ApplicationInformation.GameName;
        private IReadOnlyCollection<IGameLocationProvider> gameLocationProviders;

        public GameLocator(IEnumerable<IGameLocationProvider> gameLocationProviders)
        {
            this.gameLocationProviders = gameLocationProviders.ToList();
        }

        public IEnumerable<string> GetGameLocations() =>
            gameLocationProviders.Select(p => p.Locate(GameName).FullName);
    }
}
