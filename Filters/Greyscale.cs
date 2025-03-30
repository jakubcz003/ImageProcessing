using System;
using Avalonia.Media.Imaging;
using GimpClone.Models;

namespace GimpClone.Filters
{
    class Greyscale(ImageModel sourceImage, ImageModel originalImage) : FilterBase(sourceImage, originalImage)
    {
        public override void Transform()
        {
            Console.WriteLine("Applying greyscale filter...");

            BeforeFilter();

            // Iterate through each pixel
            // Iterate through all pixels
            for (int y = 0; y < _source.Height; y++)
            {
                for (int x = 0; x < _source.Width; x++)
                {
                    int pixelOffset = (y * _source.RowBytes) + (x * 4); // 4 bytes per pixel (BGRA)

                    // Get the current RGB values
                    byte b = _source.Buffer[pixelOffset];
                    byte g = _source.Buffer[pixelOffset + 1];
                    byte r = _source.Buffer[pixelOffset + 2];

                    byte grey = (byte)(0.3 * r + 0.6 * g + 0.1 * b);

                    // Set all RGB channels to the same grey value
                    _source.Buffer[pixelOffset] = grey;     // B
                    _source.Buffer[pixelOffset + 1] = grey; // G
                    _source.Buffer[pixelOffset + 2] = grey; // R

                }
            }

            AfterFilter();
        }

    }
}