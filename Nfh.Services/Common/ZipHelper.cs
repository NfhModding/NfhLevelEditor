using System.IO;
using System.IO.Compression;

namespace Nfh.Services.Common
{
    internal class ZipHelper : IZipHelper
    {
        public void CreateZipFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, bool overrideFile)
        {
            if (overrideFile)
            {
                File.Delete(destinationArchiveFileName);
            }
            ZipFile.CreateFromDirectory(sourceDirectoryName, destinationArchiveFileName);
        }
    }
}
