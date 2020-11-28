using Nfh.Services.Common;
using System.IO;

namespace Nfh.Services.ImageServices
{
    internal class GfxPrepareService : IGfxPrepareService
    {
        private readonly IApplicationWorkFolder applicationWorkFolder;
        private readonly IZipService zipHelper;

        public GfxPrepareService(IApplicationWorkFolder applicationWorkFolder, IZipService zipHelper)
        {
            this.applicationWorkFolder = applicationWorkFolder;
            this.zipHelper = zipHelper;
        }

        // ToDo make it async
        public DirectoryInfo PrepareGfxData(DirectoryInfo gamePath)
        {
            if (!gamePath.Exists)
                throw new("Game path does not exits");

            var gfxDataFolder = GetGfxDataFolderIfExists(applicationWorkFolder.Info);

            // If the "gfxdata" folder exists we can say the graphics are available -> ToDo what if the user has modified it?
            if (gfxDataFolder is not null)
                return gfxDataFolder;

            gfxDataFolder = GetGfxFolderInfo(applicationWorkFolder.Info);
            gfxDataFolder.Create();

            // Unzip gfxdata.bnd to appwork folder
            var gfxdataFile = new FileInfo(Path.Combine(gamePath.FullName, "data", "gfxdata.bnd"));
            zipHelper.UnzipToFolderWithOverride(gfxdataFile, gfxDataFolder);

            return gfxDataFolder;
        }

        private static DirectoryInfo? GetGfxDataFolderIfExists(DirectoryInfo workingFolder)
        {
            var appGfxdataFolder = GetGfxFolderInfo(workingFolder);
            if (appGfxdataFolder.Exists)
                return appGfxdataFolder;

            return null;
        }

        private static DirectoryInfo GetGfxFolderInfo(DirectoryInfo workingFolder) =>
            new DirectoryInfo(Path.Combine(workingFolder.FullName, "gfxdata"));
    }
}
