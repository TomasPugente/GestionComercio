﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Codigo { get; set; }
        public String Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public String UrlImagen { get; set; }
        public decimal Precio { get; set; }
        [DisplayNameAttribute("Precio")]
        public String PrecioFormateado
        {
            get
            {
                return Precio.ToString("C", CultureInfo.CreateSpecificCulture("es-AR"));
            }
        }
        public Articulo()
        {
            Marca = new Marca();
            Categoria = new Categoria();
            return;
        }


    }
}
