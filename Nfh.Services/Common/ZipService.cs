using System.IO;
using System.IO.Compression;

namespace Nfh.Services.Common
{
    internal class ZipService : IZipService
    {
        public void CreateZipFromFolder(string sourceDirectoryName, string destinationArchiveFileName, bool overrideFile)
        {
            if (overrideFile)
            {
                File.Delete(destinationArchiveFileName);
            }
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }

        public void UnzipToFolderWithOverride(FileInfo file, DirectoryInfo destinationFolder)
        {
            ZipFile.ExtractToDirectory(file.FullName, destinationFolder.FullName, overwriteFiles: true);
        }
    }
}
