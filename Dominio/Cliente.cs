using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente
    {
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Mail { get; set; }
        public string Direccion { get; set; }

        public string CUIT_CUIL { get; set; }

        public string NombreCompleto => Nombre + " " + Apellido;

        public bool Estado { get; set; }
    }
}
