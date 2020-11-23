using Nfh.Services.Common;
using System.IO;

namespace Nfh.Services.ImageServices
{
    internal class GfxPrepareService : IGfxPrepareService
    {
        // If null, we need to prepare
        private DirectoryInfo? gfxDataFolder = null;

        private readonly IApplicationWorkFolder applicationWorkFolder;
        private readonly IZipHelper zipHelper;

        public GfxPrepareService(IApplicationWorkFolder applicationWorkFolder, IZipHelper zipHelper)
        {
            this.applicationWorkFolder = applicationWorkFolder;
            this.zipHelper = zipHelper;
        }

        public DirectoryInfo PrepareGfxData(DirectoryInfo gamePath)
        {
            if (gfxDataFolder is not null)
                return gfxDataFolder;

            if (!gamePath.Exists)
                throw new("Game path does not exits");

            // Unzip gfxdata.bnd to appwork folder
            var gfxdataFile = new FileInfo(Path.Combine(gamePath.FullName, "data", "gfxdata.bnd"));

            var appGfxdataFolder = new DirectoryInfo(Path.Combine(applicationWorkFolder.Info.FullName, "gfxdata"));
            if (!appGfxdataFolder.Exists)
                appGfxdataFolder.Create();

            zipHelper.UnzipToFolderWithOverride(gfxdataFile, appGfxdataFolder);

            gfxDataFolder = appGfxdataFolder;
            return gfxDataFolder;
        }
    }
}
