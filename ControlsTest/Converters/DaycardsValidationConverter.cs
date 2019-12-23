using System;
using System.Globalization;
using Xamarin.Forms;

namespace ControlsTest.Converters
{
    public class DaycardsValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ((int)value > 0)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (Exception e)
            {
                var message = e.Message;
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }
    }
}
