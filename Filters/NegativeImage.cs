using System;
using System.Drawing;
using Avalonia.Media.Imaging;
using GimpClone.Models;

namespace GimpClone.Filters
{
    public class NegativeImage(ImageModel sourceImage, ImageModel originalImage) : FilterBase(sourceImage, originalImage)
    {
        public override void Transform()
        {
            Console.WriteLine("Applying NegativeImage filter...");

            BeforeFilter();

            // Iterate through each pixel
            // Iterate through all pixels
            for (int y = 0; y < _source.Height; y++)
            {
                for (int x = 0; x < _source.Width; x++)
                {
                    // Calculate the pixel index
                    int pixelOffset = (y * _source.RowBytes) + (x * 4); // 4 bytes per pixel (BGRA)

                    // Get the current RGB values
                    byte b = _source.Buffer[pixelOffset];
                    byte g = _source.Buffer[pixelOffset + 1];
                    byte r = _source.Buffer[pixelOffset + 2];

                    // Apply negative effect (invert colors)
                    _source.Buffer[pixelOffset] = (byte)(255 - b);
                    _source.Buffer[pixelOffset + 1] = (byte)(255 - g);
                    _source.Buffer[pixelOffset + 2] = (byte)(255 - r);
                }
            }

            AfterFilter();
        }
    }
}