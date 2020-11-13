using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nfh.Editor.Converters
{
    public class TimeSpanToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new DateTime();
            if (value is TimeSpan span) return new DateTime(1, 1, 1, span.Hours, span.Minutes, span.Seconds);

            throw new ArgumentException("Invalid value type!", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new DateTime();
            if (value is DateTime dt) return dt.TimeOfDay;

            throw new ArgumentException("Invalid value type!", nameof(value));
        }
    }
}
