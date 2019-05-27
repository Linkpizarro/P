using PX.Models;
using PX.Services;
using PX.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PX.Repositories
{
    public class RepositoryVentas
    {
        String token;
        public RepositoryVentas()
        {
            SessionService session = App.Locator.SessionService;
            this.token = session.Cadena;
        }
        public async Task<List<Venta>> GetVentasUsuario()
        {
            String peticion = Api.uriapi + "api/ComprasUsuario";
            List<Venta> ventas = await Api.CallApi<List<Venta>>(peticion, this.token);
            return ventas;
        }
        public async Task<ObservableCollection<DetallesVenta>> GetDetallesVentaUsuario(String idventa, String token)
        {
            ObservableCollection<DetallesVenta> detallesVentas = await Api.CallApi<ObservableCollection<DetallesVenta>>
                ("api/ComprasUsuarioDetalles/" + idventa, token);
            return detallesVentas;
        }
        ///api/InsertarVenta
        public async Task InsertarVenta(Venta v, String token)
        {
            String peticion = "/api/InsertarVenta";
            await Api.PostApi(v, peticion, token);
        }
    }
}
