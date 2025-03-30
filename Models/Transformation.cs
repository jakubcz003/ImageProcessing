using Avalonia; // For Application
using Avalonia.Controls;
using Avalonia.Media;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.ApplicationLifetimes; // For IClassicDesktopStyleApplicationLifetime
using CommunityToolkit.Mvvm.Input;
using Avalonia.Platform.Storage; // For OpenFileDialog
using System.Collections.Generic; // For List<T>
using System.IO; // For File operations
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;

namespace GimpClone.Models
{
    public class Transformation
    {
        public double Greyscale { get; set; }
        public double Brightness { get; set; }
        public double Contrast { get; set; }
        public double Gamma { get; set; }
        public double Shift { get; set; }
        public double Factor { get; set; }
    }
}
