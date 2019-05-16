using PX.Base;
using PX.Models;
using PX.Repositories;
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
    }
}
