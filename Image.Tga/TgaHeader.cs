using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Image.Tga
{
    internal class TgaHeader
    {
        public byte IdLength { get; set; }
        public byte ColorMapType { get; set; }
        public byte ImageType { get; set; }
        public TgaColorMapSpec ColorMapSpec { get; set; }
        public TgaImageSpec ImageSpec { get; set; }

        public static TgaHeader Deserialize(BinaryReader reader)
        {
            return new TgaHeader
            {
                IdLength = reader.ReadByte(),
                ColorMapType = reader.ReadByte(),
                ImageType = reader.ReadByte(),
                ColorMapSpec = TgaColorMapSpec.Deserialize(reader),
                ImageSpec = TgaImageSpec.Deserialize(reader),
            };
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(IdLength);
            writer.Write(ColorMapType);
            writer.Write(ImageType);
            ColorMapSpec.Serialize(writer);
            ImageSpec.Serialize(writer);
        }
    }
}
