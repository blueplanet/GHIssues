using System;
using System.Windows.Data;

namespace GHIssues.Converters
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var date = (DateTime)value;
            return date.ToString("yyyy/MM/dd");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dateStr = (string)value;
            return DateTime.Parse(dateStr);
        }
    }
}
