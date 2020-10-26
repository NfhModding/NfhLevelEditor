using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Image.Tga
{
    /// <summary>
    /// A Truevision TGA (TARGA) format image.
    /// </summary>
    public class TgaImage
    {
        private TgaHeader header;
        private TgaImageData data;

        /// <summary>
        /// The image width in pixels.
        /// </summary>
        public int Width => header.ImageSpec.ImageWidth;
        /// <summary>
        /// The image height in pixels.
        /// </summary>
        public int Height => header.ImageSpec.ImageHeight;
        /// <summary>
        /// The number of bytes per horizontal line.
        /// </summary>
        public int Stride => Width * header.ImageSpec.BitsPerPixel / 8;
        /// <summary>
        /// The raw pixel data.
        /// </summary>
        public byte[] Data => data.PixelData;
        /// <summary>
        /// The number of bits per pixel.
        /// </summary>
        public int BitsPerPixel => header.ImageSpec.BitsPerPixel;
        /// <summary>
        /// The number of bits in the aplha channel.
        /// </summary>
        public int AlphaBits => header.ImageSpec.AlphaWidthAndDirection & 0b1111;
        /// <summary>
        /// The pixel format of this TGA image.
        /// </summary>
        public TgaFormat PixelFormat => AlphaBits > 0 ? TgaFormat.Argb4 : TgaFormat.Rgb565;

        private TgaImage(TgaHeader header, TgaImageData data)
        {
            this.header = header;
            this.data = data;
        }

        /// <summary>
        /// Creates an empty <see cref="TgaImage"/> with the given specification.
        /// </summary>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <param name="format">The <see cref="TgaFormat"/> to create the image in.</param>
        public TgaImage(int width, int height, TgaFormat format)
        {
            if (width < 0 || width > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }
            if (height < 0 || height > ushort.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            header = new TgaHeader
            {
                ImageType = 2,
                ColorMapSpec = new TgaColorMapSpec { },
                ImageSpec = new TgaImageSpec
                {
                    ImageWidth = (ushort)width,
                    ImageHeight = (ushort)height,
                    BitsPerPixel = 16,
                    AlphaWidthAndDirection = (byte)(format == TgaFormat.Argb4 ? 36 : 32),
                }
            };
            data = new TgaImageData
            {
                ColorMapData = new byte[0],
                ImageId = new byte[0],
                PixelData = new byte[width * height * 2],
            };
        }

        /// <summary>
        /// Reads a TGA format image from a file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The read TGA image.</returns>
        public static TgaImage FromFile(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            return FromBytes(new BinaryReader(new FileStream(path, FileMode.Open)));
        }

        /// <summary>
        /// Parses a TGA format image from bytes.
        /// </summary>
        /// <param name="reader">The binary reader to read the bytes from.</param>
        /// <returns>The parsed <see cref="TgaImage">.</returns>
        public static TgaImage FromBytes(BinaryReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var header = TgaHeader.Deserialize(reader);
            var img = TgaImageData.Deserialize(header, reader);

            return new TgaImage(header, img);
        }

        /// <summary>
        /// Creates a TGA format image from a <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="bitmap">The <see cref="Bitmap"/> to create the image from.</param>
        /// <param name="format">The <see cref="TgaFormat"/> to use.</param>
        /// <returns>The converted image's TGA representation.</returns>
        public static TgaImage FromBitmap(Bitmap bitmap, TgaFormat format)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }
            var img = new TgaImage(bitmap.Width, bitmap.Height, format);
            var data = bitmap.LockBits(new Rectangle(Point.Empty, bitmap.Size),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* scan0 = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < img.Height; ++y)
                {
                    for (int x = 0; x < img.Width; ++x)
                    {
                        byte* rawData = scan0 + y * data.Stride + x * 4;
                        var col = Color.FromArgb(((int*)rawData)[0]);
                        img.SetPixel(x, y, col);
                    }
                }
            }
            return img;
        }

        /// <summary>
        /// Writes this <see cref="TgaImage"/> into a file.
        /// </summary>
        /// <param name="path">The path to the file to write the image to.</param>
        /// <param name="fileMode">The <see cref="FileMode"/> to open the file with..</param>
        public void ToFile(string path, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            using (var fs = new FileStream(path, fileMode))
            {
                ToBytes(new BinaryWriter(fs));
            }
        }

        /// <summary>
        /// Writes this <see cref="TgaImage"/> into it's binary format.
        /// </summary>
        /// <param name="writer">The binary writer to write the image to.</param>
        public void ToBytes(BinaryWriter writer)
        {
            header.Serialize(writer);
            data.Serialize(writer);
        }

        /// <summary>
        /// Converts this <see cref="TgaImage"/> into an equivalent <see cref="Bitmap"/>.
        /// </summary>
        /// <param name="pixelFormat">The <see cref="System.Drawing.Imaging.PixelFormat"/> to
        /// use for the bitmap.</param>
        /// <returns>The <see cref="Bitmap"/> equivalent of the TGA image.</returns>
        public Bitmap ToBitmap(PixelFormat pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        {
            Bitmap bmp = new Bitmap(Width, Height, pixelFormat);
            var data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* scan0 = (byte*)data.Scan0.ToPointer();
                for (int y = 0; y < Height; ++y)
                {
                    for (int x = 0; x < Width; ++x)
                    {
                        byte* rawData = scan0 + y * data.Stride + x * 4;
                        var col = GetPixel(x, y);
                        ((int*)rawData)[0] = col.ToArgb();
                    }
                }
            }
            bmp.UnlockBits(data);
            return bmp;
        }

        /// <summary>
        /// Retrieves the color at the given pixel position.
        /// </summary>
        /// <param name="x">The horizontal position of the pixel.</param>
        /// <param name="y">The vertical position of the pixel.</param>
        /// <returns>The <see cref="Color"/> at the given pixel.</returns>
        public Color GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            int offs = (x + y * Width) * 2;
            byte p1 = Data[offs];
            byte p2 = Data[offs + 1];

            if (PixelFormat == TgaFormat.Argb4)
            {
                int g = (int)((double)((p1 & 0xf0) >> 4) / 0xf * 255);
                int b = (int)(((double)(p1 & 0x0f)) / 0xf * 255);
                int a = (int)((double)((p2 & 0xf0) >> 4) / 0xf * 255);
                int r = (int)(((double)(p2 & 0x0f)) / 0xf * 255);
                return Color.FromArgb(a, r, g, b);
            }
            else
            {
                ushort u16 = (ushort)(p1 | (p2 << 8));
                int r = (int)((double)((u16 >> 11) & 0b11111) / 0b11111 * 255);
                int g = (int)((double)((u16 >> 5) & 0b111111) / 0b111111 * 255);
                int b = (int)((double)(u16 & 0b11111) / 0b11111 * 255);
                return Color.FromArgb(255, r, g, b);
            }
        }

        /// <summary>
        /// Sets the <see cref="Color"/> of a pixel.
        /// </summary>
        /// <param name="x">The horizontal position of the pixel.</param>
        /// <param name="y">The vertical position of the pixel.</param>
        /// <param name="color">The <see cref="Color"/> to set the pixel to.</param>
        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || x >= Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }
            if (y < 0 || y >= Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            int offs = (x + y * Width) * 2;
            if (PixelFormat == TgaFormat.Argb4)
            {
                byte a = (byte)((color.A / 255.0) * 0b1111);
                byte r = (byte)((color.R / 255.0) * 0b1111);
                byte g = (byte)((color.G / 255.0) * 0b1111);
                byte b = (byte)((color.B / 255.0) * 0b1111);

                Data[offs] = (byte)((g << 4) | b);
                Data[offs + 1] = (byte)((a << 4) | r);
            }
            else
            {
                byte r = (byte)((color.R) / 255.0 * 0b11111);
                byte g = (byte)((color.G) / 255.0 * 0b111111);
                byte b = (byte)((color.B) / 255.0 * 0b11111);

                ushort u16 = (ushort)((r << 11) | (g << 5) | b);
                Data[offs] = (byte)(u16 & 0xff);
                Data[offs + 1] = (byte)((u16 >> 8) & 0xff);
            }
        }
    }
}
