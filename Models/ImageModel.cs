using System;
using Avalonia.Media.Imaging;

namespace GimpClone.Models
{
    public class ImageModel
    {

        public WriteableBitmap _source;
        public int Width { get; set; }
        public int Height { get; set; }
        public int RowBytes { get; set; }
        public int PixelOffset { get; set; }
        public byte[] Buffer { get; set; }
        public IntPtr Address { get; set; }

        public ImageModel(WriteableBitmap sourceImage)
        {
            _source = sourceImage;

            // Now lock the bitmap for pixel operations
            using (var lockedBitmap = _source.Lock())
            {
                // Get the pixel size to iterate through all pixels
                Width = _source.PixelSize.Width;
                Height = _source.PixelSize.Height;

                // Use IntPtr instead of unsafe code
                Address = lockedBitmap.Address;
                RowBytes = lockedBitmap.RowBytes;
                Buffer = new byte[RowBytes * Height];
            }

            // Copy the pixel data to the buffer
            System.Runtime.InteropServices.Marshal.Copy(Address, Buffer, 0, Buffer.Length);
        }

    }

}