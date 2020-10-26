using Image.Tga;
using Steam.Acf;
using System;

namespace Nfh
{
	class Program
    {
        static void Main(string[] args)
        {
            var img = TgaImage.FromFile(@"c:\TMP\NeighborsFromHell_Assets\tutorial_1.tga");
            img.ToBitmap().Save(@"c:\TMP\NeighborsFromHell_Assets\tutorial_1.png");
		}
    }
}
