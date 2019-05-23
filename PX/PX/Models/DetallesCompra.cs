using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PX.Models
{
    public class DetallesCompra
    {
        public ObservableCollection<Carrito> Carrito { get; set; }
        public int TotalCompra { get; set; }
    }
}
