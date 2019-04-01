using System;
using System.Globalization;

namespace WindowChromes
{
    /// <inheritdoc href="MarkupConverter" />
    /// <summary>
    /// Return boolean True or False value
    /// </summary>
    internal class IsNan : MarkupConverter
    {
        public IsNan()
        {
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            return (double.IsNaN(((double)value)));
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Method ConvertBack not implement in IsNan");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
