﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractals.Controls;
using SkiaSharp;
using Syncfusion.SfPicker.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IfsFractals : ContentPage
    {
        private SfPicker picker;
        private ObservableCollection<Params> paramsList;

        private Dictionary<string, List<Params>> Presets = new Dictionary<string, List<Params>>
        {
            {
                "fern",
                new List<Params>
                {
                    new Params(new List<double> {0.85, 0.04, -0.04, 0.85, 0, 1.6, 0.81}),
                    new Params(new List<double> {-0.15, 0.28, 0.26, 0.24, 0, 0.44, 0.07}),
                    new Params(new List<double> {0.2, -0.26, 0.23, 0.22, 0, 1.6, 0.07}),
                    new Params(new List<double> {0, 0, 0, 0.16, 0, 0, 0.01})
                }
            },
            {
                "dragon",
                new List<Params>
                {
                    new Params(new List<double>{0.824074,0.281428,-0.212346,0.864198,-1.88290,-0.110607,0.8}),
                    new Params(new List<double>{0.088272,0.520988,-0.463889,-0.377778,0.785360,8.095795,0.2})
                }
            }
        };
        private StackLayout content4ScrolleLayout = new StackLayout();
        private SKBitmap bitmap = new SKBitmap(838,635);
        Random r = new Random();

        private List<StandardEntry> entries = new List<StandardEntry>();
        public IfsFractals()
        {
            InitializeComponent();
            paramsList = new ObservableCollection<Params>();
            picker = Content.FindByName<SfPicker>("PresetPicker");
            picker.ItemsSource = new List<String> {"koch", "dragon", "fern"};
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
            Content.FindByName<SfButton>("Popup").Clicked += OpenPopup;
            Content.FindByName<Button>("Ok").Clicked += OkClicked;
            entries.Add(Content.FindByName<StandardEntry>("AEntry"));
            entries.Add(Content.FindByName<StandardEntry>("BEntry"));
            entries.Add(Content.FindByName<StandardEntry>("CEntry"));
            entries.Add(Content.FindByName<StandardEntry>("DEntry"));
            entries.Add(Content.FindByName<StandardEntry>("EEntry"));
            entries.Add(Content.FindByName<StandardEntry>("FEntry"));
            entries.Add(Content.FindByName<StandardEntry>("PEntry"));
            DataTemplate dataTemplate = new DataTemplate(() =>
            {

                Label labelA = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelA.SetBinding(Label.TextProperty, "A");
                Label labelB = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelB.SetBinding(Label.TextProperty, "B");
                Label labelC = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelC.SetBinding(Label.TextProperty, "C");
                Label labelD = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelD.SetBinding(Label.TextProperty, "D");
                Label labelE = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelE.SetBinding(Label.TextProperty, "E");
                Label labelF = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelF.SetBinding(Label.TextProperty, "F");
                Label labelP = new Label
                {
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    TextColor = Color.FromHex("#333333")
                };
                labelP.SetBinding(Label.TextProperty, "P");

                StackLayout horizontalStackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(20,5),
                    Orientation = StackOrientation.Horizontal,
                    Children = { labelA, labelB, labelC, labelD, labelE, labelF, labelP }
                };
                return horizontalStackLayout;
            });
            BindableLayout.SetItemsSource(content4ScrolleLayout, paramsList);
            BindableLayout.SetItemTemplate(content4ScrolleLayout, dataTemplate);
            
            ParamsScrollView.Content = content4ScrolleLayout;
            paramsList.Clear();
            Presets["dragon"].ForEach(p => paramsList.Add(p));
            BuildImage();
            CanvasView.PaintSurface += CanvasView_PaintSurface;
        }

        private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(bitmap, 0, 0);
        }

        private void OkClicked(object sender, EventArgs e)
        {
            try
            {
                paramsList.Add(new Params(entries.Select(entry => double.Parse(entry.Text)).ToList()));
                entries.ForEach(entry => entry.Text = string.Empty);
            }
            catch (Exception ex)
            {
                entries.ForEach(entry => entry.Text = string.Empty);
            }

            BuildImage();
        }

        private void OpenPopup(object sender, EventArgs e)
        {
            picker.IsOpen = true;
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async Task BuildImage()
        {
            //bitmap = bitmap.Resize(new SKSizeI((int) CanvasView.Width, (int) CanvasView.Height), SKFilterQuality.None);
            using (SKCanvas canvas = new SKCanvas(bitmap))
            {
                canvas.Clear(SKColors.Transparent);
            }
            double x = 1/Math.Sqrt(2);
            double y = x;
            await Task.Run(() =>
            {
                double xMin = bitmap.Width, xMax = 0, yMin = bitmap.Height, yMax = 0;
                for (int i = 0; i < bitmap.Width * bitmap.Height; i++)
                {
                    var params_ = GetParams();
                    double x1 = (x * params_.A + y * params_.B + params_.E);
                    double y1 = (x * params_.C + y * params_.D + params_.F);
                    x = x1;
                    y = y1;
                    if (xMin > x)
                        xMin = x;
                    if (xMax < x)
                        xMax = x;
                    if (yMin > y)
                        yMin = y;
                    if (yMax < y)
                        yMax = y;
                }

                int imgx;
                int imgy;
                var coef = (xMax - xMin) / (yMax - yMin);
                if (coef <= 1)
                {
                    imgx = (int)(bitmap.Width * coef);
                    imgy = bitmap.Height;
                }
                else
                {
                    imgx = bitmap.Width;
                    imgy = (int) (bitmap.Height / coef);
                }

                for (int j = 0; j < 5; j++)
                {
                    for (int i = 0; i < bitmap.Width * bitmap.Height; i++)
                    {
                        var params_ = GetParams();
                        double x1 = (x * params_.A + y * params_.B + params_.E);
                        double y1 = (x * params_.C + y * params_.D + params_.F);
                        x = x1;
                        y = y1;
                        if (xMin > x)
                            xMin = x;
                        if (xMax < x)
                            xMax = x;
                        if (yMin > y)
                            yMin = y;
                        if (yMax < y)
                            yMax = y;
                        x1 = (x - xMin) / (xMax - xMin) * (imgx - 1);
                        y1 = (imgy - 1) - (y - yMin) / (yMax - yMin) * (imgy - 1);
                        bitmap.SetPixel((int) x1, (int) y1, SKColor.Parse("#333333"));
                    }
                }
            });
        }

        private Params GetParams()
        {
            var rand = r.NextDouble();
            foreach (var params_ in paramsList)
            {
                if (rand < params_.P)
                    return params_;
                rand -= params_.P;
            }

            return new Params(new List<double>{0,0,0,0,0,0,0});
        }
    }

    internal class Params
    {
        public Params(List<double> list)
        {
            A = list[0];
            B = list[1];
            C = list[2];
            D = list[3];
            E = list[4];
            F = list[5];
            P = list[6];
        }
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double E { get; set; }
        public double F { get; set; }
        public double P { get; set; }

    }
}