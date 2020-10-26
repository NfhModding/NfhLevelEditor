using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Image.Tga
{
    internal class TgaImageData
    {
        public byte[] ImageId { get; set; }
        public byte[] ColorMapData { get; set; }
        public byte[] PixelData { get; set; }

        public static TgaImageData Deserialize(TgaHeader header, BinaryReader reader)
        {
            var colorMapByteLen = header.ColorMapSpec.ColorMapLength * header.ColorMapSpec.ColorMapEntrySize;
            var imageByteLen = (int)Math.Ceiling(header.ImageSpec.ImageWidth * header.ImageSpec.ImageHeight * header.ImageSpec.BitsPerPixel / 8.0);

            return new TgaImageData
            {
                ImageId = reader.ReadBytes(header.IdLength),
                ColorMapData = reader.ReadBytes(colorMapByteLen),
                PixelData = reader.ReadBytes(imageByteLen),
            };
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(ImageId);
            writer.Write(ColorMapData);
            writer.Write(PixelData);
        }
    }
}
