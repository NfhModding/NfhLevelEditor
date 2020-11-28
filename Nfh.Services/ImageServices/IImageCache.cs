using System.Drawing;
using System.IO;

namespace Nfh.Services.ImageServices
{
    internal interface IImageCache
    {
        bool TryGetImage(FileInfo imagePath, out Bitmap image);
        Bitmap Set(FileInfo imagePath, Bitmap image);
    }
}
