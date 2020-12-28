using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcxNjI0QDMxMzgyZTM0MmUzMElZZlp5NFE1NUtPN3NlbzRvN3FKMTV6L3EvcGtNVFl3eFdkVXlvOTFrZDg9");

            Device.SetFlags(new string[]{ "Brush_Experimental"});
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
