using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Syncfusion.SfPicker.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeometricalFractals : ContentPage
    {
        private SKCanvasView canvasView;
        private SkiaSharp.SKCanvas canvas;
        private SkiaSharp.SKSurface surface;
        private SKBitmap bitmap;
        private List<PointF> Initiator;

        // Angles and distances for the generator.
        private float ScaleFactor;
        private List<float> GeneratorDTheta;
        private bool isDragon = false;
        private int depth = 5;


        public GeometricalFractals()
        {
            InitializeComponent();
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
            canvasView = Content.FindByName<SKCanvasView>("Canvas");
            GeneratorDTheta = new List<float>();
            float pi_over_3 = (float)(Math.PI / 3f);
            GeneratorDTheta.Add(0);                 // Draw in the original direction.
            GeneratorDTheta.Add(-pi_over_3);        // Turn -60 degrees.
            GeneratorDTheta.Add(2 * pi_over_3);     // Turn 120 degrees.
            GeneratorDTheta.Add(-pi_over_3);        // Turn -60 degrees.

        }


        private void OpenPopup(object sender, EventArgs e)
        {
            //picker.IsOpen = true;
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }


        public void DragonCheckBoxClicked(object sender, StateChangedEventArgs e)
        {
            isDragon = (bool)e.IsChecked;
            DrawFractale();

        }

        public void KochCheckBoxClicked(object sender, StateChangedEventArgs e)
        {
            isDragon = !(bool)e.IsChecked;
            DrawFractale();
        }


        void OnCanvasViewPaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SkiaSharp.SKImageInfo info = args.Info;
            surface = args.Surface;
            canvas = surface.Canvas;

            canvas.Clear();
            if (bitmap != null)
                canvas.DrawBitmap(bitmap, 0, 0);
            else
                DrawFractale();

        }

        private void DrawDragonLine(SKBitmap bitmap, int level, Direction turn_towards, float x1, float y1, float dx, float dy)
        {

            if (level <= 0)
            {
                SkiaSharp.SKPaint thinLinePaint = new SkiaSharp.SKPaint
                {
                    Style = SkiaSharp.SKPaintStyle.Stroke,
                    Color = SkiaSharp.SKColors.Red,
                    StrokeWidth = 2
                };
                using (SKCanvas bitmapCanvas = new SKCanvas(bitmap))
                {
                    bitmapCanvas.DrawLine(x1, y1, x1 + dx, y1 + dy, thinLinePaint);
                }
            }
            else
            {
                float nx = (float)(dx / 2);
                float ny = (float)(dy / 2);
                float dx2 = -ny + nx;
                float dy2 = nx + ny;
                if (turn_towards == Direction.Right)
                {
                    // Turn to the right.
                    DrawDragonLine(bitmap, level - 1, Direction.Right,
                        x1, y1, dx2, dy2);
                    float x2 = x1 + dx2;
                    float y2 = y1 + dy2;
                    DrawDragonLine(bitmap, level - 1, Direction.Left,
                        x2, y2, dy2, -dx2);
                }
                else
                {
                    // Turn to the left.
                    DrawDragonLine(bitmap, level - 1, Direction.Right,
                        x1, y1, dy2, -dx2);
                    float x2 = x1 + dy2;
                    float y2 = y1 - dx2;
                    DrawDragonLine(bitmap, level - 1, Direction.Left,
                        x2, y2, dx2, dy2);
                }
            }
        }


        enum Direction
        {
            Right,
            Left
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            depth = (int)args.NewValue;
            DrawFractale();
        }

        private async void DrawFractale()
        {
            bitmap = new SKBitmap((int)canvasView.Width, (int)canvasView.Height);
            await Task.Run(() =>
            {
                if (isDragon)
                {
                    var start_x = canvasView.Width / 4;
                    var start_y = canvasView.Height / 4;
                    DrawDragonLine(bitmap, (int) depth, Direction.Right, (float) start_x, (float) start_y,
                        (float) canvasView.Width / 2.8f, (float) canvasView.Height / 2.8f);
                }
                else
                {
                    DrawSnowflake(bitmap, (int) depth);
                }
            });
            canvasView.InvalidateSurface();
            countView.Text = ((int)depth).ToString();
        }

        private void DrawSnowflake(SKBitmap bitmap, int depth)
        {

            Initiator = new List<PointF>();
            float height = (float)canvasView.Height;
            float width = (float)height;
            float y1 = (float)(canvasView.Height/1.5f);
            float x1 = 0;
            float x2 = (float)width * 1.2f;
            Initiator.Add(new PointF(x1, y1));
            Initiator.Add(new PointF(x2, y1));


            ScaleFactor = 1 / 2.8f;                   // Make subsegments 1/3 size.

            // Draw the snowflake.
            for (int i = 1; i < Initiator.Count; i++)
            {
                PointF p1 = Initiator[i - 1];
                PointF p2 = Initiator[i];

                float dx = p2.X - p1.X;
                float dy = p2.Y - p1.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                float theta = (float)Math.Atan2(dy, dx);
                DrawSnowflakeEdge(bitmap, depth, ref p1, theta, length);
            }
        }


        private void DrawSnowflakeEdge(SKBitmap bitmap, int depth, ref PointF p1, float theta, float dist)
        {
            if (depth == 0)
            {
                PointF p2 = new PointF(
                    (float)(p1.X + dist * Math.Cos(theta)),
                    (float)(p1.Y + dist * Math.Sin(theta)));
                SkiaSharp.SKPaint thinLinePaint = new SkiaSharp.SKPaint
                {
                    Style = SkiaSharp.SKPaintStyle.Stroke,
                    Color = SkiaSharp.SKColors.Red,
                    StrokeWidth = 2
                };
                using (SKCanvas bitmapCanvas = new SKCanvas(bitmap))
                {
                    bitmapCanvas.DrawLine(p1.X, p1.Y, p2.X, p2.Y, thinLinePaint);
                }
                p1 = p2;
                return;
            }

            // Recursively draw the edge.
            dist *= ScaleFactor;
            for (int i = 0; i < GeneratorDTheta.Count; i++)
            {
                theta += GeneratorDTheta[i];
                DrawSnowflakeEdge(bitmap, depth - 1, ref p1, theta, dist);
            }
        }

        
    }
}