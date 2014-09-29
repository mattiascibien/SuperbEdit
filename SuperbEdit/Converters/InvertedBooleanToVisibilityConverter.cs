using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SuperbEdit.Converters
{
    public sealed class InveretedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool) value;
            }

            if (parameter != null)
            {
                if (bool.Parse((string) parameter))
                {
                    flag = !flag;
                }
            }
            if (flag)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool back = ((value is Visibility) && (((Visibility) value) == Visibility.Collapsed));
            if (parameter != null)
            {
                if ((bool) parameter)
                {
                    back = !back;
                }
            }
            return back;
        }
    }
}