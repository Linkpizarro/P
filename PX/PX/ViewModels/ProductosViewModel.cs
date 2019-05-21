using PX.Base;
using PX.Models;
using PX.Repositories;
using PX.Services;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class ProductosViewModel : ViewModelBase
    {
        RepositoryProductos repo;

        public ProductosViewModel()
        {
            this.repo = new RepositoryProductos();
            //NOTA: "Task.Run()" carga en el CONSTRUCTOR un metodo ASINCRONO
            Task.Run(async () => {
                await this.CargarProductos();                
            });


            //-----PRUEBA-----/
            if (Articulos == null)
            {
                this.CargarArticulos();
            }            
            //-----FIN PRUEBA-----/
        }

        private async Task CargarProductos()
        {
            List<Producto> lista = await this.repo.GetProductos();
            this.Productos = new ObservableCollection<Producto>(lista);
        }

        private ObservableCollection<Producto> _Productos;
        public ObservableCollection<Producto> Productos
        {
            get { return this._Productos; }
            set
            {
                this._Productos = value;
                OnPropertyChange("Productos");
            }
        }

        public Command MostrarDetalles
        {
            get
            {
                return new Command(async (producto) => {
                    DetallesProductoView view = new DetallesProductoView();
                    ProductoViewModel viewmodel = new ProductoViewModel();

                    viewmodel.Producto = producto as Producto;
                    view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });                
            }
        }


        //-----PRUEBA-----/
        private async Task CargarArticulos()
        {
            List<Producto> lista = App.Locator.SessionService.Articulos;
            this.Productos = new ObservableCollection<Producto>(lista);
        }

        private ObservableCollection<Producto> _Articulos;
        public ObservableCollection<Producto> Articulos
        {
            get { return this._Articulos; }
            set
            {
                this._Articulos = value;
                OnPropertyChange("Articulos");
            }
        }

        public Command AnadirProducto
        {
            //get
            //{
            //    return new Command(async (producto) => {
            //        //DetallesProductoView view = new DetallesProductoView();
            //        //ProductoViewModel viewmodel = new ProductoViewModel();
            //        CarritoView view = new CarritoView();
            //        CarritoViewModel viewmodel = new CarritoViewModel();

            //        //viewmodel.Producto = producto as Producto;
            //        //view.BindingContext = viewmodel;
            //        Carrito articulo = new Carrito((Producto)producto, 1);
            //        viewmodel.Articulos.Add(articulo);

            //        await Application.Current.MainPage.Navigation.PushModalAsync(view);
            //    });
            //}

            get
            {
                return new Command(async (producto) => {
                //return new Command(async () => {
                    CarritoView view = new CarritoView();
                    //ProductosViewModel viewmodel = new ProductosViewModel();
                    //ProductosViewModel viewmodel = App.Locator.ProductosViewModel;

                    SessionService session = App.Locator.SessionService;
                    session.Articulos.Add(producto as Producto);

                    //view.BindingContext = viewmodel;

                    await Application.Current.MainPage.Navigation.PushModalAsync(view);                    

                    MessagingCenter.Subscribe<ProductosViewModel>(this, "INSERTAR", async (sender) => {
                        this.CargarArticulos();
                    });

                    //viewmodel.Producto = producto as Producto;
                    //viewmodel.Articulos.Add(producto as Producto);
                    //view.BindingContext = viewmodel;

                    //await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
        //-----FIN PRUEBA-----/
    }
}
