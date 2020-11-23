using System.IO;

namespace Nfh.Services
{
    internal interface IGameLocationProvider
    {
        public DirectoryInfo Locate(string gameName);
    }
}
