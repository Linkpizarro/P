using PX.Models;
using PX.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PX.Repositories
{
    public class RepositoryVentas
    {
        public async Task<List<Venta>> GetVentasUsuario(String token)
        {
            List<Venta> ventas = await Api.CallApi<List<Venta>>("api/ComprasUsuario", token);
            return ventas;
        }
        public async Task<List<DetallesVenta>> GetVentasDetallesUsuario(String idventa, String token)
        {
            List<DetallesVenta> detallesVentas = await Api.CallApi<List<DetallesVenta>>
                ("api/ComprasUsuarioDetalles/" + idventa, token);
            return detallesVentas;
        }
    }
}
