using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfPicker.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IfsFractals : ContentPage
    {
        private SfPicker picker;
        public IfsFractals()
        {
            InitializeComponent();
            picker = Content.FindByName<SfPicker>("PresetPicker");
            picker.ItemsSource = new List<String> {"koch", "dragon", "fern"};
            Content.FindByName<Button>("BackButton").Clicked += ToStart;
            Content.FindByName<SfButton>("Popup").Clicked += OpenPopup;
        }

        private void OpenPopup(object sender, EventArgs e)
        {
            picker.IsOpen = true;
        }

        private void ToStart(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}