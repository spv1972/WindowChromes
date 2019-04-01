using System;
using System.Globalization;
using System.Windows;

namespace WindowChromes
{
    /// <inheritdoc href="MarkupConverter"/>
    /// <summary>
    /// Get CloseButton Foreground, in Hover and Pressed process.
    /// </summary>
    internal class GetCloseButtonForeground : MarkupConverter
    {
        public string HoverMode { set; get; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DependencyObject d)) throw new ArgumentException("GetCloseButtonForeground: Value must be not Null", nameof(value));

            if (HoverMode == "Pressed")
            {
                var pr = d.GetValue(WindowButtons.CloseButtonPressedForegroundProperty);  //at first directly
                return pr ?? (d.GetValue(WindowButtons.CloseButtonHoverForegroundProperty) //level1 hover 
                              ?? d.GetValue(WindowButtons.ButtonsPressedForegroundProperty)); //level2 common
            }
            else
                //if (HoverMode == "Hover")
            {
                var pr = d.GetValue(WindowButtons.CloseButtonHoverForegroundProperty);
                return pr ?? d.GetValue(WindowButtons.ButtonsHoverForegroundProperty);
            }


        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Method not implement in GetCloseButtonForeground.ConvertBack");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
