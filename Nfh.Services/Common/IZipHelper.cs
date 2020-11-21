namespace Nfh.Services.Common
{
    internal interface IZipHelper
    {
        void CreateZipFromDirectory(string sourceDirectoryName, string destinationArchiveFileName, bool overrideFile);
    }
}
