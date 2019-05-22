using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PX.Converters
{
    public class Subtotal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal _Subtotal = 0;
            if (value != null)
            {
                //_Subtotal = cantidad * precio;
                _Subtotal = (decimal)value * (int)parameter;
            }            
            return _Subtotal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
