using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Compra
    {
        public int IDCompra { get; set; }
        public DateTime Fecha { get; set; }
        public Proveedor Proveedor { get; set; }
        public Usuario Usuario { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
    }
}
