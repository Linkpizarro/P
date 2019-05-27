using PX.Base;
using PX.Models;
using PX.Services;
using PX.Views;
using System;
using System.Collections.Generic;
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

                //-----PRUEBA-----/
                //this.CantidadProducto = 1;
                //-----FIN PRUEBA-----/
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
                //return new Command(async (producto) => {
                return new Command(async () => {
                    CarritoView view = new CarritoView();

                    //await this.repo.InsertarDoctor(this.Doctor);
                    SessionService session = App.Locator.SessionService;
                    //List<Carrito> articulos = session.Articulos;
                    List<Producto> articulos = session.Articulos;
                    //Carrito articulo = new Carrito(this.Producto, this.CantidadProducto);

                    //this.Producto = producto as Producto;
                    //articulos.Add(this.Producto);

                    if (articulos.SingleOrDefault(p => p.IdProducto == this.Producto.IdProducto) == null)
                    {
                        this.Producto.Cantidad = 1;
                        this.Producto.Subtotal = this.Producto.Cantidad * (decimal)this.Producto.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        articulos.Add(this.Producto);
                        //this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);
                    }
                    else
                    {
                        this.Producto.Cantidad++;
                        this.Producto.Subtotal = this.Producto.Cantidad * (decimal)this.Producto.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        //this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);
                    }

                    //MessagingCenter.Send<CarritoViewModel>(App.Locator.CarritoViewModel, "INSERTAR");
                    MessagingCenter.Send<ProductosViewModel>(App.Locator.ProductosViewModel, "INSERTAR");

                    //NOTA PERSONAL: PRUEBA de mostrar mensaje después de insertar un objeto!!!
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Articulo añadido al carrito correctamente", "OK");

                    //await Application.Current.MainPage.Navigation.PopModalAsync(); //NOTA: "PopModalAsync()" cierra una VISTA volviendo a la anterior.
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        //-----FIN PRUEBA-----/
    }
}
