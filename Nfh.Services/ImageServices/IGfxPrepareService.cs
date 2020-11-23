using System.IO;

namespace Nfh.Services.ImageServices
{
    internal interface IGfxPrepareService
    {
        DirectoryInfo PrepareGfxData(DirectoryInfo gameFolder);
    }
}
