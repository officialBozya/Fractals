using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fractals
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Content.FindByName<Button>("ToIfsFractalsButton").Clicked += ToIfsFractals;
        }

        public void ToIfsFractals(object sender, EventArgs eventArgs)
        {
            Navigation.PushModalAsync(new IfsFractals());
        }
    }
}
