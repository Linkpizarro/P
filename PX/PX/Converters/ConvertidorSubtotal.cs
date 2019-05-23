using PX.Models;
using PX.Services;
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
            //if (value != null)
            //{
            //    if (value.ToString() != null)
            //    {
            //        decimal subtotal = 0;
            //        //string resultado = string.Empty;
            //        int cantidad = int.Parse(value.ToString());
            //        //subtotal = cantidad * precio;
            //        //subtotal = (decimal)value * (int)parameter;
            //        subtotal = cantidad * 2; //NOTA: FUNCIONA SIN "parameter"

            //        //subtotal = cantidad * int.Parse(((Label)parameter).Text);
            //        //subtotal = cantidad * int.Parse(parameter.ToString());

            //        return subtotal;                    
            //    }
            //    else
            //    {
            //        return 0;
            //    }                
            //}
            //else
            //{
            //    return "";
            //}


            //ENLACES DE INTERES: 
            //https://stackoverflow.com/questions/37647893/xamarin-forms-converterparameter-how-to-reference-to-current-item-property
            //https://forums.xamarin.com/discussion/71810/pass-binding-to-converterparameter
            if (value != null)
            {
                //if (value.ToString() != null)
                //{
                    decimal subtotal = 0;

                    int cantidad = int.Parse(((Label)parameter).Text);
                    int precio = ((Producto)value).PrecioUnidad;

                    subtotal = cantidad * precio;

                    //Producto prod = value as Producto; //NOTA: NO FUNCIONA PORQUE NO SE PASA EL OBJETO
                    //SessionService session = App.Locator.SessionService;
                    //int indice = session.Articulos.IndexOf(prod);
                    //session.Articulos[indice].Subtotal = subtotal;

                    return subtotal;
                //}
                //else
                //{
                //    return 0;
                //}
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
