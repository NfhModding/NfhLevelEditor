using System.IO;

namespace Image.Tga
{
    internal class TgaImageSpec
    {
        public ushort OriginX { get; set; }
        public ushort OriginY { get; set; }
        public ushort ImageWidth { get; set; }
        public ushort ImageHeight { get; set; }
        public byte BitsPerPixel { get; set; }
        public byte AlphaWidthAndDirection { get; set; }

        public static TgaImageSpec Deserialize(BinaryReader reader)
        {
            return new TgaImageSpec
            {
                OriginX = reader.ReadUInt16(),
                OriginY = reader.ReadUInt16(),
                ImageWidth = reader.ReadUInt16(),
                ImageHeight = reader.ReadUInt16(),
                BitsPerPixel = reader.ReadByte(),
                AlphaWidthAndDirection = reader.ReadByte(),
            };
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(OriginX);
            writer.Write(OriginY);
            writer.Write(ImageWidth);
            writer.Write(ImageHeight);
            writer.Write(BitsPerPixel);
            writer.Write(AlphaWidthAndDirection);
        }
    }
}
