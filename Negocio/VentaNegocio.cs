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
                datos.setearConsulta(@"
                    SELECT v.IDVenta,
                           v.Fecha,
                           v.Total,
                           c.IDCliente,
                           c.Nombre AS NombreCliente,
                           c.Apellido AS ApellidoCliente,
                           u.IDUsuario,
                           u.NombreUsuario AS NombreUsuario
                    FROM Ventas v
                    INNER JOIN Clientes c ON v.IDCliente = c.IDCliente
                    INNER JOIN Usuarios u ON v.IDUsuario = u.IDUsuario
                    ORDER BY v.Fecha DESC");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta venta = new Venta();

                    venta.IDVenta = (int)datos.Lector["IDVenta"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];
                    venta.Cliente = new Cliente
                    {
                        IDCliente = (int)datos.Lector["IDCliente"],
                        Nombre = (string)datos.Lector["NombreCliente"],
                        Apellido = (string)datos.Lector["ApellidoCliente"]
                    };

                    venta.Usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"]
                    };

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
                //Guardar la venta principal
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

        public List<DetalleVenta> listarDetalleVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT dv.IDDetalleVenta,
                           p.Nombre AS NombreProducto,
                           dv.Cantidad,
                           dv.PrecioUnitario                           
                    FROM Detalle_Venta dv
                    INNER JOIN Productos p ON dv.IDProducto = p.IDProducto
                    WHERE dv.IDVenta = @IDVenta");

                datos.setearParametro("@IDVenta", idVenta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetalleVenta det = new DetalleVenta
                    {
                        Producto = new Producto
                        {
                            Nombre = (string)datos.Lector["NombreProducto"]
                        },
                        Cantidad = (int)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]                        
                    };

                    lista.Add(det);
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

        public List<Venta> ListarVentasConDetalles()
        {
            List<Venta> ventas = listarVentas(); 

            foreach (var venta in ventas)
            {
                venta.Detalles = listarDetalleVenta(venta.IDVenta); 
            }

            return ventas;
        }

        public Venta ObtenerVentaConDetalles(int idVenta)
        {
            Venta venta = new Venta();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Traer la venta principal
                datos.setearConsulta(@"
                    SELECT v.IDVenta, v.Fecha, v.Total,
                           c.Nombre AS NombreCliente, 
                           c.Apellido AS ApellidoCliente, 
                           c.CUIT_CUIL AS CUIT_CUIL, 
                           u.NombreUsuario AS NombreUsuario
                    FROM Ventas v
                    INNER JOIN Clientes c ON v.IDCliente = c.IDCliente
                    INNER JOIN Usuarios u ON v.IDUsuario = u.IDUsuario
                    WHERE v.IDVenta = @IDVenta");
                datos.setearParametro("@IDVenta", idVenta);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    venta.IDVenta = (int)datos.Lector["IDVenta"];
                    venta.Fecha = (DateTime)datos.Lector["Fecha"];
                    venta.Total = (decimal)datos.Lector["Total"];

                    venta.Cliente = new Cliente
                    {
                        Nombre = (string)datos.Lector["NombreCliente"],
                        Apellido = (string)datos.Lector["ApellidoCliente"],
                        CUIT_CUIL = (string)datos.Lector["Cuit_Cuil"]
                    };

                    venta.Usuario = new Usuario
                    {
                        NombreUsuario = (string)datos.Lector["NombreUsuario"]
                    };
                }
                datos.cerrarConexion();

                venta.Detalles = listarDetalleVenta(idVenta);

                return venta;
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
