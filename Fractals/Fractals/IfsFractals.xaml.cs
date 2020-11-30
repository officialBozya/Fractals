using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IfsFractals : ContentPage
    {
        public IfsFractals()
        {
            InitializeComponent();
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}