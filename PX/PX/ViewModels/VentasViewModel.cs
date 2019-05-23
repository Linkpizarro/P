using PX.Base;
using PX.Models;
using PX.Repositories;
using PX.Services;
using PX.Tools;
using PX.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class VentasViewModel : ViewModelBase
    {
        RepositoryVentas repo;
        RepositoryProductos repoProductos;

        public VentasViewModel()
        {
            this.repo = new RepositoryVentas();
            this.repoProductos = new RepositoryProductos();
            //NOTA: "Task.Run()" carga en el CONSTRUCTOR un metodo ASINCRONO
            Task.Run(async () => {
                await this.CargarVentas();
            });
        }

        private async Task CargarVentas()
        {
            SessionService session = App.Locator.SessionService;
            string token = session.Cadena;
            List<Venta> lista = await this.repo.GetVentasUsuario();
            this.Ventas = new ObservableCollection<Venta>(lista);
            //this.Ventas[0].Fecha.DayOfWeek.ToString();
            if (this.Ventas == null || this.Ventas.Count==0)
            {
                this.Mensaje = "No has realizado ninguna compra...";
            }
        }

        private ObservableCollection<Venta> _Ventas;
        public ObservableCollection<Venta> Ventas
        {
            get { return this._Ventas; }
            set
            {
                this._Ventas = value;
                OnPropertyChange("Ventas");
            }
        }
        private string _Mensaje;
        public string Mensaje
        {
            get { return this._Mensaje; }
            set
            {
                this._Mensaje = value;
                OnPropertyChange("Mensaje");
            }
        }
        public Command MostrarCompra
        {
            get
            {
                return new Command(async (venta) => {
                    DetallesCompraView view = new DetallesCompraView();
                    DetallesVentaViewModel viewmodel = new DetallesVentaViewModel();
                    Venta v = venta as Venta;
                    String token = App.Locator.SessionService.Cadena;

                    ObservableCollection<DetallesVenta> detallesventa = await this.repo.GetDetallesVentaUsuario(v.IdVenta, token);
                    DetallesCompra prodscompra = new DetallesCompra();
                    int totalcompra = 0;
                    ObservableCollection<Carrito> comprados = new ObservableCollection<Carrito>();
                    foreach(DetallesVenta dv in detallesventa)
                    {
                        Producto p = await this.repoProductos.BuscarProducto(dv.IdProducto);
                        Carrito pcomprado = new Carrito(p, dv.Cantidad);
                        comprados.Add(pcomprado);
                        totalcompra += p.PrecioUnidad * dv.Cantidad;
                    }
                    prodscompra.Carrito = comprados;
                    prodscompra.TotalCompra = totalcompra;
                    viewmodel.DetallesCompra = prodscompra;
                    view.BindingContext = viewmodel;
                    await Application.Current.MainPage.Navigation.PushModalAsync(view);
                });
            }
        }
    }
}
