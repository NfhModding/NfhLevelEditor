using Nfh.Editor.Commands.UiCommands;
using System;
using System.Globalization;
using System.Windows.Data;

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
                var gestureStr = uiCommand.GestureText ?? string.Empty;
                if (gestureStr.Length > 0)
                {
                    if (result.Length > 0) result += ' ';
                    result += '(' + gestureStr + ')';
                }
                return result;
            }
            throw new ArgumentException("Invalid command type!", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
