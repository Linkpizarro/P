using PX.Base;
using PX.Models;
using PX.Services;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class ProductoViewModel : ViewModelBase
    {

        public ProductoViewModel()
        {
            if (Producto == null)
            {
                this.Producto = new Producto();
            }
        }

        private Producto _Producto;
        public Producto Producto
        {
            get { return this._Producto; }
            set
            {
                this._Producto = value;
                OnPropertyChange("Producto");
            }
        }

        //-----PRUEBA-----/
        private int _CantidadProducto;
        public int CantidadProducto
        {
            get { return this._CantidadProducto; }
            set
            {
                this._CantidadProducto = value;
                OnPropertyChange("CantidadProducto");
            }
        }

        public Command GuadarProducto
        {
            get
            {
                return new Command(async () => {
                    CarritoView view = new CarritoView();

                    SessionService session = App.Locator.SessionService;
                    List<Producto> articulos = session.Articulos;

                    ProductosViewModel viewmodel = App.Locator.ProductosViewModel;

                    if (viewmodel.Articulos.SingleOrDefault(p => p.IdProducto == this.Producto.IdProducto) == null)
                    {
                        this.Producto.Cantidad = 1;
                        this.Producto.Subtotal = this.Producto.Cantidad * (decimal)this.Producto.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        viewmodel.Articulos.Add(this.Producto);
                        viewmodel.TotalCarrito = viewmodel.Articulos.Sum(p => (int)p.Subtotal);
                    }
                    else
                    {
                        this.Producto.Cantidad++;
                        this.Producto.Subtotal = this.Producto.Cantidad * (decimal)this.Producto.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        viewmodel.TotalCarrito = viewmodel.Articulos.Sum(p => (int)p.Subtotal);
                    }

                    //NOTA: NO SE USA MessagingCenter!!!
                    //MessagingCenter.Send<ProductosViewModel>(App.Locator.ProductosViewModel, "INSERTAR");

                    //NOTA PERSONAL: PRUEBA de mostrar mensaje después de insertar un objeto!!!
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Articulo añadido al carrito correctamente", "OK");

                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    //await Application.Current.MainPage.Navigation.PopModalAsync(); //NOTA: "PopModalAsync()" cierra una VISTA volviendo a la anterior.
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        //-----FIN PRUEBA-----/
    }
}
