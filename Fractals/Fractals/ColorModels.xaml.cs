using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Skybrud.Colors;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorModels : ContentPage
    {
        private Task currectTask;
        private string openedFileName;
        private SKBitmap bitmap1 = new SKBitmap();
        private SKBitmap bitmap2 = new SKBitmap();
        private SKPoint firstPoint = new SKPoint(0, 0);
        private SKPoint secondPoint = new SKPoint(0, 0);
        private SKPoint currectPoint = new SKPoint(0, 0);
        public ColorModels()
        {
            InitializeComponent();
            Open.Clicked += OpenClicked;
            CanvasViewInput.PaintSurface += CanvasViewInput_PaintSurface;
            CanvasViewOutput.PaintSurface += CanvasViewOutput_PaintSurface;
            CanvasViewInput.EnableTouchEvents = true;
            CanvasViewInput.Touch += CanvasViewInput_Touch;
            FirstOk.Clicked += FirstOk_Clicked;
            SecondOk.Clicked += SecondOk_Clicked;
            YellowSaturationSlider.ValueChanged += YellowSaturationSlider_ValueChanged;
            BrightnessSlider.ValueChanged += BrightnessSlider_ValueChanged;
            Save.Clicked += Save_Clicked;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                (openedFileName + DateTime.Now.Ticks).Replace(".", string.Empty) + ".png");
            using (var stream = File.Create(fileName))
            {
                using (SKWStream wStream = new SKManagedWStream(stream))
                {
                    bitmap2.Encode(wStream, SKEncodedImageFormat.Png, Int32.MaxValue);
                }
            }
        }

        private void BrightnessSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                currectTask?.Dispose();
            }
            catch
            {
                BrightnessSlider.Value = e.OldValue;
                return;
            }
            currectTask = Task.Run(() =>
            {
                if (e.NewValue > 0)
                {
                    for (int i = (int) firstPoint.Y; i < secondPoint.Y; i++)
                    {
                        for (int j = (int) firstPoint.X; j < secondPoint.X; j++)
                        {
                            var color = bitmap1.GetPixel(j, i);
                            var rgbColor = new RgbColor(color.Red, color.Green, color.Blue);
                            var hslColor = rgbColor.ToHsl();
                            double right = (1 - hslColor.Lightness) / 100;

                            hslColor = new HslColor(hslColor.Hue, hslColor.Saturation,
                                (hslColor.Lightness + right * e.NewValue));

                            rgbColor = hslColor.ToRgb();

                            SKColor newColor = new SKColor(rgbColor.Red, rgbColor.Green, rgbColor.Blue);

                            bitmap2.SetPixel(j, i, newColor);
                        }
                    }
                }

                else if (e.NewValue < 0)
                {
                    for (int i = (int) firstPoint.Y; i < secondPoint.Y; i++)
                    {
                        for (int j = (int) firstPoint.X; j < secondPoint.X; j++)
                        {
                            var color = bitmap1.GetPixel(j, i);
                            var rgbColor = new RgbColor(color.Red, color.Green, color.Blue);
                            var hslColor = rgbColor.ToHsl();
                            double left = hslColor.Lightness / 100;


                            hslColor = new HslColor(hslColor.Hue, hslColor.Saturation,
                                (hslColor.Lightness + left * e.NewValue));

                            rgbColor = hslColor.ToRgb();

                            SKColor newColor = new SKColor(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                            bitmap2.SetPixel(j, i, newColor);
                        }
                    }
                }

                else if (e.NewValue == 0)
                {
                    bitmap2 = bitmap1;
                }

                CanvasViewOutput.InvalidateSurface();
            });
        }

        private void YellowSaturationSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                currectTask?.Dispose();
            }
            catch
            {
                YellowSaturationSlider.Value = e.OldValue;
                return;
            }

            currectTask = Task.Run(() =>
            {
                if (e.NewValue > 0)
                {
                    for (int i = (int)firstPoint.Y; i < secondPoint.Y; i++)
                    {
                        for (int j = (int)firstPoint.X; j < secondPoint.X; j++)
                        {
                            var color = bitmap1.GetPixel(j, i);
                            var rgbColor = new RgbColor(color.Red, color.Green, color.Blue);
                            var cmyColor = rgbColor.ToCmy();
                            double right = (1 - cmyColor.Y) / 100;

                            cmyColor = new CmyColor(cmyColor.C, cmyColor.M,
                                (cmyColor.Y + right * e.NewValue));

                            rgbColor = cmyColor.ToRgb();

                            SKColor newColor = new SKColor(rgbColor.Red, rgbColor.Green, rgbColor.Blue);

                            bitmap2.SetPixel(j, i, newColor);
                        }
                    }
                }

                else if (e.NewValue < 0)
                {
                    for (int i = (int)firstPoint.Y; i < secondPoint.Y; i++)
                    {
                        for (int j = (int)firstPoint.X; j < secondPoint.X; j++)
                        {
                            var color = bitmap1.GetPixel(j, i);
                            var rgbColor = new RgbColor(color.Red, color.Green, color.Blue);
                            var cmyColor = rgbColor.ToCmy();
                            double left = cmyColor.Y / 100;

                            cmyColor = new CmyColor(cmyColor.C, cmyColor.M,
                                (cmyColor.Y + left * e.NewValue));

                            rgbColor = cmyColor.ToRgb();

                            SKColor newColor = new SKColor(rgbColor.Red, rgbColor.Green, rgbColor.Blue);
                            bitmap2.SetPixel(j, i, newColor);
                        }
                    }
                }

                else if (e.NewValue == 0)
                {
                    bitmap2 = bitmap1;
                }

                CanvasViewOutput.InvalidateSurface();
            });
        }

        private void SecondOk_Clicked(object sender, EventArgs e)
        {
            if (currectPoint != secondPoint)
            {
                SecondPointLabel.Text = SecondPointLabel.Text.Replace($"{secondPoint.X}, {secondPoint.Y}",$"{currectPoint.X}, {currectPoint.Y}"); 
                secondPoint = currectPoint;
            }
        }

        private void FirstOk_Clicked(object sender, EventArgs e)
        {
            if (currectPoint != firstPoint)
            {
                FirstPointLabel.Text = FirstPointLabel.Text.Replace($"{firstPoint.X}, {firstPoint.Y}",$"{currectPoint.X}, {currectPoint.Y}");
                firstPoint = currectPoint;
            }
        }

        private void CanvasViewInput_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed && e.Location.X < bitmap1.Width &&
                e.Location.Y < bitmap1.Height)
            {
                int x = (int) e.Location.X;
                int y = (int) e.Location.Y;
                SelectedPointLabel.Text =
                    SelectedPointLabel.Text.Replace($"{currectPoint.X}, {currectPoint.Y}", $"{x}, {y}");
                currectPoint = new SKPoint(x, y);
                OnSelected();
            }
        }

        private void CanvasViewOutput_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(bitmap2, 0, 0);
        }

        private void CanvasViewInput_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(bitmap1, 0, 0);
        }

        private async void OpenClicked(object sender, EventArgs e)
        {
            SKBitmap bitmap;
            try
            {
                FileResult file = await FilePicker.PickAsync(PickOptions.Images);
                using (Stream stream = await file.OpenReadAsync())
                {
                    bitmap = SKBitmap.Decode(stream);
                }

                openedFileName = Path.ChangeExtension(file.FileName, string.Empty);
            }
            catch (Exception ex)
            {
                return;
            }

            double scale = bitmap.Height > bitmap.Width
                ? CanvasViewInput.CanvasSize.Height / bitmap.Height
                : CanvasViewInput.CanvasSize.Width / bitmap.Width;
            bitmap1 = bitmap.Resize(new SKSizeI((int)(bitmap.Width * scale),
                (int)(bitmap.Height * scale)), SKFilterQuality.None);
            bitmap2 = bitmap1.Copy();
            CanvasViewInput.InvalidateSurface();
            CanvasViewOutput.InvalidateSurface();

            int x = bitmap1.Width - 1;
            int y = bitmap1.Height - 1;
            SecondPointLabel.Text =
                SecondPointLabel.Text.Replace($"{secondPoint.X}, {secondPoint.Y}", $"{x}, {y}");

            secondPoint = new SKPoint(x, y);

            BrightnessSlider.IsEnabled = true;
            YellowSaturationSlider.IsEnabled = true;
            FirstOk.IsEnabled = true;
            SecondOk.IsEnabled = true;
        }

        void OnSelected()
        {
            var color = bitmap1.GetPixel((int) currectPoint.X, (int) currectPoint.Y);
            RgbColor rgbColor = new RgbColor(color.Red, color.Green, color.Blue);
            var hslColor = rgbColor.ToHsl();
            var cmykColor = rgbColor.ToCmyk();
            HSLLabel.Text =
                $"{(int) (hslColor.Hue * 360)}° {(int) (hslColor.Lightness * 100)}% {(int) (hslColor.Saturation * 100)}%";
            CMYKLabel.Text =
                $"{(int) (cmykColor.C * 100)}% {(int) (cmykColor.M * 100)}% {(int) (cmykColor.Y * 100)}% {(int) (cmykColor.K * 100)}%";
        }
    }
}