using System;
using System.Globalization;
using System.Windows.Data;

namespace CMusicPlayer.Util.Converters
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "0:00";
            var ts = TimeSpan.FromSeconds((int)value);
            return $"{ts.Minutes}:{ts.Seconds:D2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
