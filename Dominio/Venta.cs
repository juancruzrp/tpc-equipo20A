using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Venta
    {
        public int IDVenta { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; }

    }
}
