using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Nfh.Editor.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool b))
            {
                throw new ArgumentException("Wrong value type for converter!", nameof(value));
            }
            return b ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility v))
            {
                throw new ArgumentException("Wrong value type for converter!", nameof(value));
            }
            return v == Visibility.Visible;
        }
    }
}
