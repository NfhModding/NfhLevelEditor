using System.IO;

namespace Nfh.Services.Common
{
    internal interface IZipHelper
    {
        void CreateZipFromFolder(string sourceFolderName, string destinationArchiveFileName, bool overrideFile);
        void UnzipToFolderWithOverride(FileInfo file, DirectoryInfo destinationFolder);
    }
}
