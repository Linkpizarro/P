using PX.Models;
using PX.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PX.Repositories
{
    public class RepositoryUsuarios
    {
        public async Task RegistrarUsuario(Usuario user)
        {
            String peticion = Api.uriapi + "api/Usuarios";
            await Api.PostApi(user, peticion, null);
        }
    }
}
