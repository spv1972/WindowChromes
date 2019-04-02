using System;
using System.Globalization;
using System.Windows.Data;

namespace WindowChromes
{
    internal class RatioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return null;
            double dv;
            double dp;
            try
            {
                dv = value.ToString().ToDouble();
                dp = parameter.ToString().ToDouble();
            }
            catch (Exception) // if value or parameter incorrect
            {
                return value;
            }

            return dv * dp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return null;
            double dv;
            double dp;
            try
            {
                dv = value.ToString().ToDouble();
                dp = parameter.ToString().ToDouble();
            }
            catch (Exception) // if value or parameter incorrect
            {
                return value;
            }

            return dv / dp;
        }

    }
}
