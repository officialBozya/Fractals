using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fractals
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzYwMjU1QDMxMzgyZTMzMmUzMFFQZHkxNHRFU04zTVhFS0UrYzk2QzQ4UEtaaE41ZmFRdTlMYmM3a0xibUE9");

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
