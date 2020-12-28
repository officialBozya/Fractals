using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Accord.Math;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AffineTransformation : ContentPage
    {
        private Point currentPoint = new Point(0, 0);
        private Point aPoint;
        private Point bPoint;
        private Point cPoint;
        private Point dPoint;
        private Point a1Point;
        private Point b1Point;
        private Point c1Point;
        private Point d1Point;
        private double aParam;
        private double bParam;
        private Line line;
        private Line abLine;
        private Line bcLine;
        private Line cdLine;
        private Line daLine;
        private Line ab1Line;
        private Line bc1Line;
        private Line cd1Line;
        private Line da1Line;


        public AffineTransformation()
        {
            InitializeComponent();
            BackButton.Clicked += ToStart;
            CanvasView.EnableTouchEvents = true;
            CanvasView.Touch += CanvasView_Touch;
            FirstOk.Clicked += FirstOk_Clicked;
            SecondOk.Clicked += SecondOk_Clicked;
            ThirdOk.Clicked += ThirdOk_Clicked;
            AEntry.TextChanged += AEntry_TextChanged;
            BEntry.TextChanged += BEntry_TextChanged;
            AxEntry.TextChanged += AxEntry_TextChanged;
            AyEntry.TextChanged += AyEntry_TextChanged;
            BxEntry.TextChanged += BxEntry_TextChanged;
            ByEntry.TextChanged += ByEntry_TextChanged;
            CxEntry.TextChanged += CxEntry_TextChanged;
            CyEntry.TextChanged += CyEntry_TextChanged;
            CanvasView.PaintSurface += CanvasView_PaintSurface;
        }

        private void MakeTransformation()
        {
            var matrix = new[,]
            {
                {aPoint.X, aPoint.Y, 1},
                {bPoint.X, bPoint.Y, 1},
                {cPoint.X, cPoint.Y, 1},
                {dPoint.X, dPoint.Y, 1}
            };
            var firstTranslateMatrix = new[,]
            {
                {1, 0, 0}, 
                {0, 1, 0}, 
                {0, -bParam, 1}
            };
            var radians = -Math.Atan(aParam);
            var firstRotationMatrix = new[,]
            {
                {Math.Cos(radians), Math.Sin(radians), 0}, 
                {-Math.Sin(radians), Math.Cos(radians), 0}, 
                {0, 0, 1}
            };
            var mirrorMatrix = new[,]
            {
                {1, 0, 0},
                {0, -1, 0},
                {0, 0, 1}
            };
            var secondRotationMatrix = new[,]
            {
                {Math.Cos(radians), -Math.Sin(radians), 0},
                {Math.Sin(radians), Math.Cos(radians), 0},
                {0, 0, 1}
            }; 
            var secondTranslateMatrix = new[,]
            {
                {1, 0, 0},
                {0, 1, 0},
                {0, bParam, 1}
            };
            var result = matrix.Dot(firstTranslateMatrix).Dot(firstRotationMatrix).Dot(mirrorMatrix)
                .Dot(secondRotationMatrix).Dot(secondTranslateMatrix);
            a1Point = new Point(result[0, 0], result[0, 1]);
            b1Point = new Point(result[1, 0], result[1, 1]);
            c1Point = new Point(result[2, 0], result[2, 1]);
            d1Point = new Point(result[3, 0], result[3, 1]);
            CanvasView.InvalidateSurface();
        }

        private void CanvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawLine(0, (float) GetYReverse(Function(GetX(0))), CanvasView.CanvasSize.Width,
                (float) GetYReverse(Function(GetX(CanvasView.CanvasSize.Width))), new SKPaint{Color = SKColors.Black});
            DrawParallelogram(canvas, aPoint, bPoint, cPoint, dPoint, SKColor.Parse("#333333")); 
            DrawParallelogram(canvas, a1Point, b1Point, c1Point, d1Point, SKColor.Parse("#FFD5D5"));
        }

        private void DrawParallelogram(SKCanvas canvas, Point a, Point b, Point c, Point d, SKColor color)
        {
            SKPoint aPoint = new SKPoint((float)GetXReverse(a.X), (float)GetYReverse(a.Y));
            SKPoint bPoint = new SKPoint((float)GetXReverse(b.X), (float)GetYReverse(b.Y));
            SKPoint cPoint = new SKPoint((float)GetXReverse(c.X), (float)GetYReverse(c.Y));
            SKPoint dPoint = new SKPoint((float)GetXReverse(d.X), (float)GetYReverse(d.Y));
            SKPaint paint = new SKPaint {Color = color};
            canvas.DrawLine(aPoint, bPoint, paint);
            canvas.DrawLine(bPoint, cPoint, paint);
            canvas.DrawLine(cPoint, dPoint, paint);
            canvas.DrawLine(dPoint, aPoint, paint);
        }
        private double GetX(double x) => x - CanvasView.CanvasSize.Width / 2;

        private double GetXReverse(double x) => x + CanvasView.CanvasSize.Width / 2;

        private double GetY(double y) => -(y - CanvasView.CanvasSize.Height / 2);

        private double GetYReverse(double y) => CanvasView.CanvasSize.Height / 2 - y;

        private double Function(double x) => aParam * x + bParam;

        private void CyEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CyEntry.Text, out int value))
            {
                cPoint.Y = value;
                PointChanged();
            }
            else
            {
                CyEntry.Text = "";
            }
        }

        private void CxEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(CxEntry.Text, out int value))
            {
                cPoint.X = value;
                PointChanged();
            }
            else
            {
                CxEntry.Text = "";
            }

        }

        private void ByEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(ByEntry.Text, out int value))
            {
                bPoint.Y = value;
                PointChanged();
            }
            else
            {
                ByEntry.Text = "";
            }
        }

        private void BxEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(BxEntry.Text, out int value))
            {
                bPoint.X = value;
                PointChanged();
            }
            else
            {
                BxEntry.Text = "";
            }
        }

        private void AyEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AyEntry.Text, out int value))
            {
                aPoint.Y = value;
                PointChanged();
            }
            else
            {
                AyEntry.Text = "";
            }
        }

        private void AxEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(AxEntry.Text, out int value))
            {
                aPoint.X = value;
                PointChanged();
            }
            else
            {
                AxEntry.Text = "";
            }
        }

        private void BEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(BEntry.Text, out double value))
            {
                bParam = value;
                PointChanged();
            }
            else
            {
                BEntry.Text = "";
            }
        }

        private void AEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(AEntry.Text, out double value))
            {
                aParam = value;
                PointChanged();
            }
            else
            {
                AEntry.Text = "";
            }
        }

        private void ThirdOk_Clicked(object sender, EventArgs e)
        {
            if (currentPoint != cPoint)
            {
                cPoint = currentPoint;
                CxEntry.Text = cPoint.X.ToString();
                CyEntry.Text = cPoint.Y.ToString();
            }
        }

        private void SecondOk_Clicked(object sender, EventArgs e)
        {
            if (currentPoint != bPoint)
            {
                bPoint = currentPoint;
                BxEntry.Text = bPoint.X.ToString();
                ByEntry.Text = bPoint.Y.ToString();
            }
        }

        private void FirstOk_Clicked(object sender, EventArgs e)
        {
            if (currentPoint != aPoint)
            {
                aPoint = currentPoint;
                AxEntry.Text = aPoint.X.ToString();
                AyEntry.Text = aPoint.Y.ToString();
            }
        }

        private void PointChanged()
        {
            if (aPoint != bPoint && aPoint != cPoint && bPoint != cPoint)
            {
                int xMid = (int) (aPoint.X + cPoint.X);
                int yMid = (int) (aPoint.Y + cPoint.Y);
                dPoint = new Point(xMid - bPoint.X, yMid - bPoint.Y);
                MakeTransformation();
            }
        }

        private void CanvasView_Touch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed)
            {
                int x = (int) GetX(e.Location.X);
                int y = (int) GetY(e.Location.Y);
                SelectedPointLabel.Text =
                    SelectedPointLabel.Text.Replace($"{currentPoint.X}, {currentPoint.Y}", $"{x}, {y}");
                currentPoint = new Point(x, y);
            }
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}