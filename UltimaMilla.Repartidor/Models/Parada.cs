using System;
using System.Collections.Generic;
using System.Text;

namespace UltimaMilla.Repartidor.Models
{
    public class Parada
    {
        public int Numero { get; set; }

        public string Cliente { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
    }
}
