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

        private ObservableCollection<DetallesVenta> _DetallesVenta;
        public ObservableCollection<DetallesVenta> DetallesVenta
        {
            get { return this._DetallesVenta; }
            set
            {
                this._DetallesVenta = value;
                OnPropertyChange("DetallesVenta");
            }
        }
    }
}
