using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes; // For IClassicDesktopStyleApplicationLifetime
using Avalonia.Platform.Storage; // For OpenFileDialog
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using GimpClone.Views;
using Avalonia.Controls;
using Avalonia.Platform;

namespace GimpClone.Models
{
    public class ImageLoader
    {
        public static async Task<Bitmap> LoadImageFromFileExplorer(Window mainWindow)
        {

            ArgumentNullException.ThrowIfNull(mainWindow);

            var options = new FilePickerOpenOptions
            {
                Title = "Open Image File",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
            new FilePickerFileType("Image Files")
            {
                Patterns = new[] { "*.png", "*.jpg", "*.jpeg", "*.bmp" }
            }
        }
            };

            var result = await mainWindow.StorageProvider.OpenFilePickerAsync(options);
            if (result.Count > 0)
            {
                var file = result[0];
                using var stream = await file.OpenReadAsync();
                var bitmap = new Bitmap(stream);

                return bitmap;
            }

            throw new InvalidOperationException("No file selected.");
        }

        public static WriteableBitmap LoadWriteableFromBitmap(Bitmap bitmap)
        {
            var writeableBitmap = new WriteableBitmap(
                bitmap.PixelSize,
                bitmap.Dpi,
                bitmap.Format
            );
            using (var fb = writeableBitmap.Lock())
            {
                bitmap.CopyPixels(fb, AlphaFormat.Opaque);
            }
            return writeableBitmap;
        }
    }
}