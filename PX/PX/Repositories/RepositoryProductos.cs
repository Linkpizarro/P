using PX.Models;
using PX.Services;
using PX.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PX.Repositories
{
    public class RepositoryProductos
    {
        public RepositoryProductos()
        {
            SessionService session = App.Locator.SessionService;
            String token = session.Cadena;
        }

        public async Task<List<Producto>> GetProductos()
        {
            String peticion = Api.uriapi + "api/productos";
            List<Producto> productos = await Api.CallApi<List<Producto>>(peticion, null);
            return productos;
        }
    }
}
