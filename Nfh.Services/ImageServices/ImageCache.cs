using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Nfh.Services.ImageServices
{
    internal class ImageCache : IImageCache
    {
        private Dictionary<string, Bitmap> cache = new();

        public Bitmap Set(FileInfo imagePath, Bitmap image) => 
            cache[imagePath.FullName] = image;

        public bool TryGetImage(FileInfo imagePath, out Bitmap image) => 
            cache.TryGetValue(imagePath.FullName, out image);

    }
}
