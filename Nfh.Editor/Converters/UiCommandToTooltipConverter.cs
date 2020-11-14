using Nfh.Editor.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Nfh.Editor.Converters
{
    public class UiCommandToTooltipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (value is UiCommand uiCommand)
            {
                var result = uiCommand.Name ?? string.Empty;
                if (uiCommand.Gesture is KeyGesture keyGesture)
                {
                    var gestureStr = keyGesture.GetDisplayStringForCulture(CultureInfo.CurrentCulture);
                    if (gestureStr.Length > 0)
                    {
                        if (result.Length > 0) result += ' ';
                        result += '(' + gestureStr + ')';
                    }
                }
                return result;
            }
            throw new ArgumentException("Invalid command type!", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
