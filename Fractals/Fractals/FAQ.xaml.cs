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
      

        public FAQ()
        {
            InitializeComponent();
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
            OpenIntroInfo(null, null);
            introBtn.Clicked += OpenIntroInfo;
            fractalBtn.Clicked += OpenFractalInfo;
            colorSchemesBtn.Clicked += OpenColorSchemaInfo;
            parallelogramBtn.Clicked += OpenParallelogramInfo;
            whatIsAFractalBtn.Clicked += OpenWhatIsFractalInfo;
            whatIsAColorSchemaBtn.Clicked += OpenWhatIsColorSchemaInfo;
            whatIsAParallelogramBtn.Clicked += OpenWhatIsParallelogramInfo;
        }

        private void OpenIntroInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "Fractals - a program for working with geometrical and IFS fractal. It works with color models based on: HSL and CMYK working with a fragment of the image to transform the model and change the color attribute. Implementation of the affine transformation of a parallelogram introduced along its vertices into a mirror image to the line Y = AX + B.";
        }


        private void OpenFractalInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "A fractal is a rough or fragmented geometric shape that can be subdivided in parts, each of which is (at least approximately) a reduced-size copy of the whole. Fractals are generally self-similar and independent of scale. There are many mathematical structures that are fractals; e.g. Sierpinski triangle, Koch snowflake, Peano curve, Mandelbrot set, and Lorenz attractor. Benoît B. Mandelbrot gives a mathematical definition of fractal as a set for which the Hausdorff Besicovich dimension strictly exceeds the topological dimension";
        }

        private void OpenColorSchemaInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "Thinking of a color palette for your work can be overwhelming and time-consuming. Luckily, you don’t have to sit for hours trying out every color combination to find one that looks good. You can use tried-and-true color schemes to find a combination that works, or you can use this color scheme tool and select from a vast range of hues and find its monochromatic, complementary, analogous, or triadic counterparts.";
        }
        private void OpenParallelogramInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "Special cases of parallelogram: \nRhomboid – A quadrilateral whose opposite sides are parallel and adjacent sides are unequal, and whose angles are not right angles. \nRectangle – A parallelogram with four angles of equal size(right angles). \nRhombus – A parallelogram with four sides of equal length. \nSquare – A parallelogram with four sides of equal length and angles of equal size(right angles).";
        }

        private void OpenWhatIsFractalInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "According to Mandelbrot, who invented the term: I coined fractal from the Latin adjective fractus. The corresponding Latin verb frangere means “to break”: to create irregular fragments. It is therefore sensible - and how appropriate for our needs!-that, in addition to “fragmented” (as in fraction or refraction), fractus should also mean “irregular,” both meanings being preserved in fragment.";
        }

        private void OpenWhatIsColorSchemaInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "A color scheme consists of a combination of colors used in a range of design disciplines, from fine art to interior design to graphic design. Each color scheme consists of one or more of the twelve colors present on the color wheel. By pairing different colors with each other, you can create endless color palettes to use in any composition. Different color combinations evoke different moods or tones by using color theory and color psychology.";
        }
        private void OpenWhatIsParallelogramInfo(object sender, EventArgs e)
        {
            Content.FindByName<Label>("detailsView").Text = "In Euclidean geometry, a parallelogram is a simple quadrilateral with two pairs of parallel sides. The opposite or facing sides of a parallelogram are of equal length and the opposite angles of a parallelogram are of equal measure. The congruence of opposite sides and opposite angles is a direct consequence of the Euclidean parallel postulate and neither condition can be proven without appealing to the Euclidean parallel postulate or one of its equivalent formulations.";
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

    }
}