using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nfh.Services.ImageServices
{
    internal interface IImageCache
    {
        bool TryGetImage(FileInfo imagePath, out Bitmap image);
        Bitmap Set(FileInfo imagePath, Bitmap image);
    }
}
