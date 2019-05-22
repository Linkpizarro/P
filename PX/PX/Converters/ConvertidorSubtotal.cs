using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PX.Converters
{
    public class ConvertidorSubtotal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            if (value != null)
            {
                if (value.ToString() != null)
                {
                    decimal _Subtotal = 0;
                    string resultado = string.Empty;
                    _Subtotal = decimal.Parse(value.ToString());
                    //_Subtotal = cantidad * precio;
                    //_Subtotal = (decimal)value * (int)parameter;
                    _Subtotal = _Subtotal * 2; //NOTA: FUNCIONA SIN "parameter"

                    //_Subtotal = _Subtotal * int.Parse(((Label)parameter).Text);
                    //_Subtotal = _Subtotal * int.Parse(parameter.ToString());

                    return _Subtotal;
                    
                }
                else
                {
                    return 0;
                }                
            }
            else
            {
                return "";
            }
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
