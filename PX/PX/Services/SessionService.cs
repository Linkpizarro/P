using PX.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PX.Services
{
    public class SessionService
    {
        public String Cadena { get; set; }
        public Usuario Usuario { get; set; }

        public SessionService()
        {
            this.Usuario = new Usuario();
        }
    }
}
