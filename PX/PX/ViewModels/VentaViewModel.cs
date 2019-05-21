using PX.Base;
using PX.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PX.ViewModels
{
    public class VentaViewModel : ViewModelBase
    {
        public VentaViewModel()
        {

        }
        private Venta _Venta;
        public Venta Venta
        {
            get { return this._Venta; }
            set
            {
                this._Venta = value;
                OnPropertyChange("Venta");
            }
        }
    }
}
