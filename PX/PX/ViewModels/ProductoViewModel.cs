using PX.Base;
using PX.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
