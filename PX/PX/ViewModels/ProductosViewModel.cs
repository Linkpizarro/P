using PX.Base;
using PX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PX.ViewModels
{
    public class ProductosViewModel : ViewModelBase
    {
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
    }
}
