using PX.Base;
using PX.Models;
using PX.Repositories;
using PX.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PX.ViewModels
{
    public class VentasViewModel : ViewModelBase
    {
        RepositoryVentas repo;

        public VentasViewModel()
        {
            this.repo = new RepositoryVentas();
            //NOTA: "Task.Run()" carga en el CONSTRUCTOR un metodo ASINCRONO
            Task.Run(async () => {
                await this.CargarVentas();
            });
        }

        private async Task CargarVentas()
        {
            SessionService session = App.Locator.SessionService;
            string token = session.Cadena;
            List<Venta> lista = await this.repo.GetVentasUsuario(token);
            this.Ventas = new ObservableCollection<Venta>(lista);
        }

        private ObservableCollection<Venta> _Ventas;
        public ObservableCollection<Venta> Ventas
        {
            get { return this._Ventas; }
            set
            {
                this._Ventas = value;
                OnPropertyChange("Productos");
            }
        }
    }
}
