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
        }

        public void ToIfsFractals(object sender, EventArgs eventArgs)
        {
            Navigation.PushModalAsync(new IfsFractals());
        }
    }
}
