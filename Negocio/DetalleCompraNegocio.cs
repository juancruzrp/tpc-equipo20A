using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleCompraNegocio
    {
       
       
           public List<DetalleCompra> ListarPorCompra(int idCompra)
        {
            List<DetalleCompra> lista = new List<DetalleCompra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
               
                datos.setearConsulta(@"
                    SELECT DC.IDDetalleCompra, DC.IDProducto, DC.Cantidad, DC.PrecioUnitario,
                           P.Nombre AS NombreProducto, P.Descripcion AS DescripcionProducto, P.Precio AS PrecioProducto
                    FROM Detalle_Compra DC
                    INNER JOIN Productos P ON P.IDProducto = DC.IDProducto
                    WHERE DC.IDCompra = @IDCompra");

                datos.setearParametro("@IDCompra", idCompra);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleCompra aux = new DetalleCompra();
                    aux.IDDetalleCompra = (int)datos.Lector["IDDetalleCompra"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                    aux.Producto = new Producto
                    {
                        IDProducto = (int)datos.Lector["IDProducto"],
                        Nombre = (string)datos.Lector["NombreProducto"], 
                        Descripcion = (string)datos.Lector["DescripcionProducto"], 
                        Precio = (decimal)datos.Lector["PrecioProducto"] 
                    };

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
              
                throw new Exception("Error al listar detalles de compra: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
