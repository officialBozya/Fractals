using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Fractals.Controls;
using Fractals.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using CornerRadius = Windows.UI.Xaml.CornerRadius;
using EntryRenderer = Xamarin.Forms.Platform.UWP.EntryRenderer;
using SolidColorBrush = Windows.UI.Xaml.Media.SolidColorBrush;
using Thickness = Windows.UI.Xaml.Thickness;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]
namespace Fractals.UWP.Renderers
{
    class StandardEntryRenderer : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement is StandardEntry entry)
            {
                if (Control == null)
                {
                    var textbox = new FormsTextBox();

                    textbox.CornerRadius = new CornerRadius(entry.CornerRadius);

                    textbox.BorderThickness = new Thickness(entry.BorderThickness);

                    textbox.BorderBrush = new SolidColorBrush(entry.BorderColor.ToWindowsColor());

                    SetNativeControl(textbox);
                }
                Control.CornerRadius = new CornerRadius(entry.CornerRadius);

                Control.BorderThickness = new Thickness(entry.BorderThickness);

                Control.BorderBrush = new SolidColorBrush(entry.BorderColor.ToWindowsColor());
            }

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is StandardEntry entry)
            {
                if (e.PropertyName == StandardEntry.CornerRadiusProperty.PropertyName)
                {
                    Control.CornerRadius = new CornerRadius(entry.CornerRadius);
                    UpdateBackground();
                }
                else if (e.PropertyName == StandardEntry.BorderThicknessProperty.PropertyName)
                {
                    Control.BorderThickness = new Thickness(entry.BorderThickness);
                    UpdateBackground();
                }
                else if (e.PropertyName == StandardEntry.BorderColorProperty.PropertyName)
                {
                    Control.BorderBrush = new SolidColorBrush(entry.BorderColor.ToWindowsColor());
                    UpdateBackground();
                }
            }

            base.OnElementPropertyChanged(sender, e);
        }

    }
}
