using System;
using System.Windows;
using System.Windows.Data;

namespace GHIssues.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visible = (bool)value;
            return visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return visibility == Visibility.Visible ? true : false;
        }
    }
}
