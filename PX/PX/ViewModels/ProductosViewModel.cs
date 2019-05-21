using PX.Base;
using PX.Models;
using PX.Repositories;
using PX.Services;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            //if (Articulos == null)
            //{
            //    this.CargarArticulos();
            //}            
            //-----FIN PRUEBA-----/
        }

        

        //Destacados
        private void CargarDestacados()
        {
            List<Producto> destacados = new List<Producto>();
            Random r1 = new Random();
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                index = r1.Next(0, this.Productos.Count - 1);
                Producto producto = new Producto();
                producto = this.Productos[index];
                if(destacados.SingleOrDefault(d => d.IdProducto == producto.IdProducto) == null)
                {
                    destacados.Add(producto);
                }
                else
                {
                    i--;
                }
            }
            this.Destacados = new ObservableCollection<Producto>(destacados);
        }
        private ObservableCollection<Producto> _Destacados;
        public ObservableCollection<Producto> Destacados
        {
            get { return this._Destacados; }
            set
            {
                this._Destacados = value;
                OnPropertyChange("Destacados");
            }
        }

        //Novedades
        private void CargarNovedades()
        {
            this.Novedades = new ObservableCollection<Producto>(this.Productos.OrderBy(p => p.Fecha).Take(5).ToList());
        }
        private ObservableCollection<Producto> _Novedades;
        public ObservableCollection<Producto> Novedades
        {
            get { return this._Novedades; }
            set
            {
                this._Novedades = value;
                OnPropertyChange("Novedades");
            }
        }

        //Productos
        private async Task CargarProductos()
        {
            List<Producto> lista = await this.repo.GetProductos();
            this.Productos = new ObservableCollection<Producto>(lista);
            this.CargarNovedades();
            this.CargarDestacados();

            this.CargarArticulos();
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

        //Comandos
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
        private void CargarArticulos()
        {
            List<Producto> lista = App.Locator.SessionService.Articulos;
            this.Articulos = new ObservableCollection<Producto>(lista);
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

                    //-----NOTA: PRUEBA AÑADIR PRODUCTOS AL CARRITO-----/
                    //NOTA: FUNCIONA!!!
                    SessionService session = App.Locator.SessionService;
                    Producto prod = producto as Producto;
                    if (session.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
                    {
                        prod.Cantidad = 1;
                        session.Articulos.Add(prod);
                    }
                    else
                    {
                        prod.Cantidad++;
                    }

                    //NOTA: NO FUNCIONA!!!
                    //Producto prod = producto as Producto;
                    //if (this.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
                    //{
                    //    prod.Cantidad = 1;
                    //    this.Articulos.Add(prod);
                    //}
                    //else
                    //{
                    //    prod.Cantidad++;
                    //}
                    //-----FIN PRUEBA-----/

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

        //public Command EliminarProducto
        //{
        //    get
        //    {
        //        return new Command(async (producto) =>
        //        {
        //            SessionService session = App.Locator.SessionService;
        //            Producto prod = producto as Producto;
        //            if (session.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
        //            {
        //                session.Articulos.Remove(prod);
        //            }
        //        });
        //    }
        //}
        //-----FIN PRUEBA-----/
    }
}
