using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WindowChromes
{
    /// <inheritdoc cref="MarkupExtension" />
    /// <inheritdoc  cref="IValueConverter" />
    /// Abstract class inherit from both IValueConverter and MarkupExtension.
    /// <summary>
    /// </summary>
    internal abstract class MarkupConverter : MarkupExtension, IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract override object ProvideValue(IServiceProvider serviceProvider);
    }
}
