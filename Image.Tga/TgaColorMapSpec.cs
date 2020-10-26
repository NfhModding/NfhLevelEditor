using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Image.Tga
{
    internal class TgaColorMapSpec
    {
        public ushort FirstEntryIndex { get; set; }
        public ushort ColorMapLength { get; set; }
        public byte ColorMapEntrySize { get; set; }

        public static TgaColorMapSpec Deserialize(BinaryReader reader)
        {
            return new TgaColorMapSpec
            {
                FirstEntryIndex = reader.ReadUInt16(),
                ColorMapLength = reader.ReadUInt16(),
                ColorMapEntrySize = reader.ReadByte(),
            };
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(FirstEntryIndex);
            writer.Write(ColorMapLength);
            writer.Write(ColorMapEntrySize);
        }
    }
}
