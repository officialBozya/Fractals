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
    public partial class FAQ : ContentPage
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


        public FAQ()
        {
            InitializeComponent();
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
            Content.FindByName<Label>("detailsView").Text = "Fractals - a program for working with geometrical and IFS fractal. It works with color models based on: HSL and CMYK working with a fragment of the image to transform the model and change the color attribute. The realization of the motion of a parallelogram introduced along its vertices along the trajectory Y = AX + B with simultaneous scaling and mirroring relative to this line.";

        }


        private void OpenPopup(object sender, EventArgs e)
        {
            //picker.IsOpen = true;
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}