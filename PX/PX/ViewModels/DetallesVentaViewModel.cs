using PX.Base;
using PX.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PX.ViewModels
{
    public class DetallesVentaViewModel : ViewModelBase
    {
        public DetallesVentaViewModel()
        {

        }

        private DetallesCompra _DetallesCompra;
        public DetallesCompra DetallesCompra
        {
            get { return this._DetallesCompra; }
            set
            {
                this._DetallesCompra = value;
                OnPropertyChange("DetallesCompra");
            }
        }
    }
}
