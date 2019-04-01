using System;
using System.Globalization;

namespace WindowChromes
{
    /// <inheritdoc href="MarkupConverter" />
    /// <summary>
    /// Return boolean True or False value
    /// </summary>
    internal class IsNull : MarkupConverter
    {
        public IsNull() { }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Method ConvertBack not implement");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
