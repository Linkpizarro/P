using PX.Models;
using PX.Services;
using PX.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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
        public async Task InsertarVenta(String token)
        {
            using (HttpClient client = new HttpClient())
            {
                String peticion = "api/InsertarVenta";
                client.BaseAddress = new Uri(Api.uriapi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Api.headerjson);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                StringContent content =
                    new StringContent(""
                    , System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(peticion, content);
            }
        }
    }
}
