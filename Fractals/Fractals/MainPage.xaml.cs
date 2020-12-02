using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace Fractals
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ((SfButton)Content.FindByName("ToIfsFractalsButton")).Clicked += ToIfsFractals;
            ((SfButton)Content.FindByName("ToFractalsButton")).Clicked += ToFractals;
        }

        public void ToIfsFractals(object sender, EventArgs eventArgs)
        {
            Navigation.PushModalAsync(new IfsFractals());
        } 
        
        public void ToFractals(object sender, EventArgs eventArgs)
        {
            Navigation.PushModalAsync(new GeometricalFractals());
        }
    }
}
