using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> listarVentas()
        {
            List<Venta> lista = new List<Venta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IDVenta, Fecha, IDCliente, IDUsuario, Total FROM Ventas");
                
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.IDVenta = (int)datos.Lector["IDVenta"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];                                                            

                    lista.Add(venta);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int GuardarVenta(int idCliente, int idUsuario, DateTime fecha, List<DetalleVenta> detalles)
        {
            int idVentaGenerado = 0;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1️⃣ Guardar la venta principal
                string consultaVenta = "INSERT INTO Ventas (Fecha, IDCliente, IDUsuario, Total) " +
                                       "VALUES (@Fecha, @IDCliente, @IDUsuario, @Total); SELECT SCOPE_IDENTITY();";

                decimal total = 0;
                foreach (var det in detalles)
                    total += det.PrecioUnitario * det.Cantidad;

                datos.setearConsulta(consultaVenta);
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@IDCliente", idCliente);
                datos.setearParametro("@IDUsuario", idUsuario);
                datos.setearParametro("@Total", total);

                idVentaGenerado = Convert.ToInt32(datos.ejecutarScalar()); 
                                
                foreach (var det in detalles)
                {
                    datos.limpiarParametros();
                    datos.setearConsulta("INSERT INTO Detalle_Venta (IDVenta, IDProducto, Cantidad, PrecioUnitario) " +
                                         "VALUES (@IDVenta, @IDProducto, @Cantidad, @PrecioUnitario)");

                    datos.setearParametro("@IDVenta", idVentaGenerado);
                    datos.setearParametro("@IDProducto", det.Producto.IDProducto);
                    datos.setearParametro("@Cantidad", det.Cantidad);
                    datos.setearParametro("@PrecioUnitario", det.PrecioUnitario);

                    datos.ejecutarAccion();
                }

                return idVentaGenerado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



    }
}
