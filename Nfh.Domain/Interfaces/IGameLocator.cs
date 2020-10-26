using System.Collections.Generic;

namespace Nfh.Domain.Interfaces
{
    public interface IGameLocator
    {
        /// <summary>
        /// Locates all known installations of the game.
        /// </summary>
        public IEnumerable<string> GetGameLocations();
    }
}
