﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PX.Models
{
    public class Producto
    {
        [JsonProperty("IdProducto")]
        public String IdProducto { get; set; }
        [JsonProperty("Nombre")]
        public String Nombre { get; set; }
        [JsonProperty("PrecioUnidad")]
        public int PrecioUnidad { get; set; }
        [JsonProperty("Descripcion")]
        public String Descripcion { get; set; }
        [JsonProperty("UrlImagen")]
        public String UrlImagen { get; set; }
        [JsonProperty("Tipo")]
        public String Tipo { get; set; }
        [JsonProperty("Fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty("Marca")]
        public String Marca { get; set; }
        [JsonProperty("Color")]
        public String Color { get; set; }
        [JsonProperty("Estado")]
        public String Estado { get; set; }
    }
}
