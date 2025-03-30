using System;
using System.Drawing;
using Avalonia.Media.Imaging;
using GimpClone.Models;

namespace GimpClone.Filters
{
    public class Correction(ImageModel sourceImage, ImageModel originalImage, Transformation transformation) : FilterBase(sourceImage, originalImage)
    {

        public override void Transform()
        {
            Console.WriteLine("Applying Correction filter...");

            BeforeFilter();

            // Iterate through each pixel
            // Iterate through all pixels
            for (int y = 0; y < _source.Height; y++)
            {
                for (int x = 0; x < _source.Width; x++)
                {
                    // Calculate the pixel index
                    int pixelOffset = (y * _source.RowBytes) + (x * 4); // 4 bytes per pixel (BGRA)

                    int pixelOffsetCopy = (y * _copy.RowBytes) + (x * 4); // 4 bytes per pixel (BGRA)

                    // Get the original RGB values
                    byte b = _copy.Buffer[pixelOffsetCopy];
                    byte g = _copy.Buffer[pixelOffsetCopy + 1];
                    byte r = _copy.Buffer[pixelOffsetCopy + 2];

                    // // Apply gamma correction
                    // b = (byte)(255 * Math.Pow(b / 255.0, transformation.Gamma));
                    // g = (byte)(255 * Math.Pow(g / 255.0, transformation.Gamma));
                    // r = (byte)(255 * Math.Pow(r / 255.0, transformation.Gamma));


                    // Apply brightness and contrast adjustments
                    int brightnessOffset = (int)(transformation.Brightness * 2.55);
                    byte newB = (byte)Math.Clamp(b + brightnessOffset, 0, 255);
                    byte newG = (byte)Math.Clamp(g + brightnessOffset, 0, 255);
                    byte newR = (byte)Math.Clamp(r + brightnessOffset, 0, 255);

                    // b = (byte)Math.Clamp((b - 128) * transformation.Contrast + 128, 0, 255);
                    // g = (byte)Math.Clamp((g - 128) * transformation.Contrast + 128, 0, 255);
                    // r = (byte)Math.Clamp((r - 128) * transformation.Contrast + 128, 0, 255);


                    // Set the new RGB values back to the buffer
                    _source.Buffer[pixelOffset] = newB;     // B
                    _source.Buffer[pixelOffset + 1] = newG; // G
                    _source.Buffer[pixelOffset + 2] = newR; // R

                }
            }

            AfterFilter();
        }
    }
}