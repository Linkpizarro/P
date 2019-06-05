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
        RepositoryVentas repoVentas;

        public ProductosViewModel()
        {
            this.repo = new RepositoryProductos();
            this.repoVentas = new RepositoryVentas();
            Task.Run(async () => {
                await this.CargarProductos();
                this.CargarNovedades();
                this.CargarDestacados();
                this.CargarArticulos();
                this.ValidarCarro();
            });
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

        private void CargarArticulos()
        {
            SessionService session = App.Locator.SessionService;
            List<Producto> lista = session.Articulos;
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
            get
            {
                return new Command(async (producto) => {
                    CarritoView view = new CarritoView();

                    //-----NOTA: PRUEBA AÑADIR PRODUCTOS AL CARRITO-----/
                    //NOTA: YA NO FUNCIONA: para ello en IoCConfiguration tiene que estar builder.RegisterType<ProductosViewModel>().SingleInstance();!!!
                    Producto prod = producto as Producto;
                    if (this.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
                    {
                        prod.Cantidad = 1;
                        prod.Subtotal = prod.Cantidad * (decimal)prod.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        this.Articulos.Add(prod);
                        this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);
                    }
                    else
                    {
                        prod.Cantidad++;                        
                        prod.Subtotal = prod.Cantidad * (decimal)prod.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);
                    }
                    //-----FIN PRUEBA-----/
                    this.ValidarCarro();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);

                    //NOTA: NO SE USA MessagingCenter!!!
                    //MessagingCenter.Subscribe<ProductosViewModel>(this, "INSERTAR", async (sender) =>
                    //{
                    //    //this.CargarArticulos();
                    //});

                });
            }
        }

        public Command EliminarProducto
        {
            get
            {
                return new Command(async (producto) =>
                {
                    Producto prod = producto as Producto;
                    this.Articulos.Remove(prod);                    
                    this.TotalCarrito = Articulos.Sum(p => (int)p.Subtotal);
                    this.ValidarCarro();
                });
            }
        }

        public Command SumarProducto
        {
            get
            {
                return new Command(async (producto) =>
                {
                    //-----NOTA: PRUEBA AÑADIR PRODUCTOS AL CARRITO-----/
                    //NOTA: YA NO FUNCIONA: para ello en IoCConfiguration tiene que estar builder.RegisterType<ProductosViewModel>().SingleInstance();!!!
                    Producto prod = producto as Producto;
                    if (this.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
                    {

                    }
                    else
                    {
                        prod.Cantidad++;
                        //this.CantidadProducto = prod.Cantidad;
                        prod.Subtotal = prod.Cantidad * (decimal)prod.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        //this.SubtotalProducto = prod.Subtotal;

                        this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);

                    }
                    
                    CarritoView view = new CarritoView();
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                    //-----FIN PRUEBA-----/
                });
            }
        }

        public Command RestarProducto
        {
            get
            {
                return new Command(async (producto) =>
                {
                    //-----NOTA: PRUEBA AÑADIR PRODUCTOS AL CARRITO-----/
                    //NOTA: YA NO FUNCIONA: para ello en IoCConfiguration tiene que estar builder.RegisterType<ProductosViewModel>().SingleInstance();!!!
                    Producto prod = producto as Producto;
                    if (this.Articulos.SingleOrDefault(p => p.IdProducto == prod.IdProducto) == null)
                    {
                        
                    }
                    else
                    {
                        if (prod.Cantidad > 1)
                        {
                            prod.Cantidad--;
                        }
                        
                        //this.CantidadProducto = prod.Cantidad;
                        prod.Subtotal = prod.Cantidad * (decimal)prod.PrecioUnidad; //NOTA: Añadido porque se ha comentado en "Producto.cs" la propiedad extendida "Subtotal".
                        //this.SubtotalProducto = prod.Subtotal;
                        this.TotalCarrito = this.Articulos.Sum(p => (int)p.Subtotal);

                        CarritoView view = new CarritoView();
                        await Application.Current.MainPage.Navigation.PopModalAsync();
                        await Application.Current.MainPage.Navigation.PushModalAsync(view);
                    }
                    //-----FIN PRUEBA-----/
                });
            }
        }

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

        private decimal _SubtotalProducto;
        public decimal SubtotalProducto
        {
            get { return this._SubtotalProducto; }
            set
            {
                this._SubtotalProducto = value;
                OnPropertyChange("SubtotalProducto");
            }

        }
 

        private int _TotalCarrito;
        public int TotalCarrito
        {
            get { return this._TotalCarrito; }
            set
            {
                this._TotalCarrito = value;
                OnPropertyChange("TotalCarrito");
            }

        }

        public Command ComprarCarrito
        {
            get
            {
                return new Command(async () =>
                {
                    if (this.Articulos != null)
                    {
                        String token = App.Locator.SessionService.Cadena;
                        if (token == null)
                        {
                            bool respuesta = await Application.Current.MainPage.DisplayAlert("Pregunta...", "Tiene que iniciar sesión para comprar ¿Quieres iniciar sesión?", "Si", "No");
                            if (respuesta == true)
                            {
                                LoginView view = new LoginView();
                                await Application.Current.MainPage.Navigation.PushModalAsync(view);
                            }
                            //else
                            //{
                            //    await Application.Current.MainPage.Navigation.PopModalAsync();
                            //}
                            ////await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                        else
                        {
                            await this.repoVentas.InsertarVenta(token);

                            List<DetallesVenta> detallesVenta = new List<DetallesVenta>();
                            foreach (Producto p in this.Articulos)
                            {
                                DetallesVenta detalles = new DetallesVenta();
                                detalles.IdProducto = p.IdProducto;
                                detalles.Cantidad = p.Cantidad;
                                detallesVenta.Add(detalles);
                            }
                            await this.repoVentas.InsertarDetallesVenta(detallesVenta, token);

                            this.Articulos.Clear();
                            this.TotalCarrito = 0;

                            await Application.Current.MainPage.DisplayAlert("Alerta", "Carrito comprado correctamente", "OK");
                            
                            await Application.Current.MainPage.Navigation.PopModalAsync();
                        }
                    }
                    
                });
            }
        }

        private String _Mensaje;
        public String Mensaje
        {
            get { return this._Mensaje; }
            set { this._Mensaje = value;OnPropertyChange("Mensaje"); }
        }

        private Boolean _CompraHabilitada;
        public Boolean CompraHabilitada
        {
            get { return this._CompraHabilitada; }
            set { this._CompraHabilitada = value; OnPropertyChange("CompraHabilitada"); }
        }

        private void ValidarCarro()
        {
            if (this.Articulos.Count == 0)
            {
                this.Mensaje = "No tienes ningún producto en tu carrito...";
                this.CompraHabilitada = false;
            }
            else
            {
                this.Mensaje = "";
                this.CompraHabilitada = true;
            }
        }

        public Command Filtrar
        {
            get
            {
                return new Command(async (filtro) =>
                {
                    await this.CargarProductos();
                    ProductosView view = new ProductosView();
                    ProductosViewModel viewmodel =App.Locator.ProductosViewModel;
                    if (filtro as string  != "todo")
                    {
                        viewmodel.Productos = new ObservableCollection<Producto>(this.Productos.Where(p => p.Tipo == filtro as string).ToList());
                    }
                    else
                    {
                        viewmodel.Productos = new ObservableCollection<Producto>(this.Productos.ToList());
                    }   
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
