using System;
using System.Windows.Data;

namespace GHIssues.Converters
{
    public class NumberToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var intValue = (int)value;
            return intValue == 0 ? string.Empty : intValue.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var str = (string)value;
            return string.IsNullOrEmpty(str) ? 0 : int.Parse(str);
        }
    }
}
