namespace GimpClone.ViewModels;

using Avalonia; // For Application
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
using GimpClone.Models;
using GimpClone.Filters;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private WriteableBitmap? currentImage;
    private WriteableBitmap? OriginalImage;

    private ImageModel? CurrentImageModel;
    private ImageModel? OriginalImageModel;

    [ObservableProperty]
    private Color selectedColor = Colors.Black;

    [ObservableProperty]
    private double gamma = 1;
    [ObservableProperty]
    private double brightness = 0;
    [ObservableProperty]
    private double contrast = 0;

    [ObservableProperty]
    private double canvasWidth = 800;

    [ObservableProperty]
    private double canvasHeight = 600;

    protected FilterBase? _filter;

    private Transformation _transformation = new Transformation();

    [RelayCommand]
    private async Task Open()
    {
        // Get the main window using the Application.Current
        var mainWindow = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

        if (mainWindow == null) return;

        Bitmap bitmap = await ImageLoader.LoadImageFromFileExplorer(mainWindow);

        // Use the LoadWriteableFromBitmap method to create a WriteableBitmap
        CurrentImage = ImageLoader.LoadWriteableFromBitmap(bitmap);
        OriginalImage = CurrentImage;

        CanvasWidth = bitmap.PixelSize.Width;
        CanvasHeight = bitmap.PixelSize.Height;

        CurrentImageModel = new ImageModel(CurrentImage);
        OriginalImageModel = new ImageModel(OriginalImage);
    }

    [RelayCommand]
    private void Save()
    {
        // TODO: Implement file save
    }

    [RelayCommand]
    private void Exit()
    {
        // TODO: Implement application exit
    }

    [RelayCommand]
    private void Undo()
    {
        if (_filter == null)
            return;

        Console.WriteLine("Undoing last filter operation...");

    }

    [RelayCommand]
    private void Redo()
    {
        // TODO: Implement redo
    }

    [RelayCommand]
    private void Apply()
    {
        _transformation.Gamma = Gamma;
        _transformation.Brightness = Brightness;
        _transformation.Contrast = Contrast;

        if (CurrentImage == null || CurrentImageModel == null || OriginalImageModel == null)
            return;

        _filter = new Correction(CurrentImageModel, OriginalImageModel, _transformation);
        _filter.Transform();
    }

    [RelayCommand]
    private void NegativeImage()
    {
        if (CurrentImage == null || CurrentImageModel == null || OriginalImageModel == null)
            return;

        _filter = new NegativeImage(CurrentImageModel, OriginalImageModel);
        _filter.Transform();
    }

    [RelayCommand]
    private void Greyscale()
    {
        if (CurrentImage == null || CurrentImageModel == null || OriginalImageModel == null)
            return;

        _filter = new Greyscale(CurrentImageModel, OriginalImageModel);
        _filter.Transform();
    }

    [RelayCommand]
    private void Correction()
    {
        if (CurrentImage == null || CurrentImageModel == null || OriginalImageModel == null)
            return;

        _transformation.Brightness = 2;

        _filter = new Correction(CurrentImageModel, OriginalImageModel, _transformation);
        _filter.Transform();
    }

}

