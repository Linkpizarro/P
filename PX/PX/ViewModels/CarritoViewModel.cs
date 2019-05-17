using PX.Base;
using PX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PX.ViewModels
{
    public class CarritoViewModel : ViewModelBase
    {

        public CarritoViewModel()
        {
            if (Articulos == null)
            {
                //this.Articulos = new ObservableCollection<Carrito>();
            }

            //this.repo = new RepositoryDoctores();
            //NOTA: "Task.Run()" carga en el CONSTRUCTOR un metodo ASINCRONO
            Task.Run(async () => {
                await this.CargarArticulos();
            });

            MessagingCenter.Subscribe<CarritoViewModel>(this, "INSERTAR", async (sender) => {
                await this.CargarArticulos();
            });
        }


        private async Task CargarArticulos()
        {
            //List<Carrito> lista = App.Locator.SessionService.Articulos;
            //this.Articulos = new ObservableCollection<Carrito>(lista);

            List<Producto> lista = App.Locator.SessionService.Articulos;
            this.Articulos = new ObservableCollection<Producto>(lista);
        }


        //private ObservableCollection<Carrito> _Articulos;
        //public ObservableCollection<Carrito> Articulos
        //{
        //    get { return this._Articulos; }
        //    set
        //    {
        //        this._Articulos = value;
        //        OnPropertyChange("Articulos");
        //    }
        //}

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
    }
}
