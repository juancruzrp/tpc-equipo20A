using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {
        public decimal Subtotal;

        public int IDDetalleCompra { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int IDCompra { get; set; }
        public int IDProducto { get; set; }
        public string NombreProducto { get; set; }

        public Usuario Usuario { get; set; }
    }
}

  
       

