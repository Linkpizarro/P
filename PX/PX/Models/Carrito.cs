using System;
using System.Collections.Generic;
using System.Text;

namespace PX.Models
{
    public class Carrito
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public Carrito(Producto producto, int cantidad)
        {
            this.Producto = producto;
            this.Cantidad = cantidad;
        }

        private decimal _Subtotal;
        public decimal Subtotal
        {
            get
            {
                _Subtotal = this.Cantidad * (decimal)this.Producto.PrecioUnidad;
                return _Subtotal;
            }
        }
    }
}
