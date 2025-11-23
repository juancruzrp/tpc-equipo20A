using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {

        public int Agregar(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
        try
            {
                datos.Conexion.Open();

                SqlTransaction transaccion = datos.Conexion.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = datos.Conexion;
                    cmd.Transaction = transaccion;
                    cmd.CommandType = System.Data.CommandType.Text;

                    // IDCompra  generado automáticamente
                    cmd.CommandText = "INSERT INTO Compras (IDProveedor, IDUsuario, Fecha, Total) " +
                                      "OUTPUT INSERTED.IDCompra " +
                                      "VALUES (@IDProv, @IDUsu, @Fecha, @Total)";

                    // Asignamos los parámetros
                    cmd.Parameters.AddWithValue("@IDProv", compra.Proveedor.IDProveedor);
                    cmd.Parameters.AddWithValue("@IDUsu", compra.Usuario.IDUsuario);
                    cmd.Parameters.AddWithValue("@Fecha", compra.Fecha);
                    cmd.Parameters.AddWithValue("@Total", compra.Total);

                   
                    int idCompraGenerado = (int)cmd.ExecuteScalar();

                    foreach (var item in compra.Detalles)
                    {
                        SqlCommand cmdDetalle = new SqlCommand();
                        cmdDetalle.Connection = datos.Conexion;
                        cmdDetalle.Transaction = transaccion; 
                        cmdDetalle.CommandType = System.Data.CommandType.Text;
                        cmdDetalle.CommandText = "INSERT INTO Detalle_Compra (IDCompra, IDProducto, Cantidad, PrecioUnitario) " +
                                                 "VALUES (@IDComp, @IDProd, @Cant, @Precio)";

                        cmdDetalle.Parameters.AddWithValue("@IDComp", idCompraGenerado);
                        cmdDetalle.Parameters.AddWithValue("@IDProd", item.Producto.IDProducto);
                        cmdDetalle.Parameters.AddWithValue("@Cant", item.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Precio", item.PrecioUnitario);

                        cmdDetalle.ExecuteNonQuery();
                    }

                    // confirmamos todo en la base de datos
                    transaccion.Commit();
                    return idCompraGenerado;
                }
                catch (Exception ex)
                {
                   // deshacemos todos los cambios 
                    transaccion.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cerramos la conexión manualmente
                if (datos.Conexion.State == System.Data.ConnectionState.Open)
                    datos.Conexion.Close();
            }
        }


        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
              
                datos.setearConsulta(@"
                    SELECT C.IDCompra, C.Fecha, C.Total,
                           P.IDProveedor, P.Nombre,
                           U.IDUsuario, U.NombreUsuario
                    FROM Compras C
                    INNER JOIN Proveedores P ON P.IDProveedor = C.IDProveedor
                    INNER JOIN Usuarios U ON U.IDUsuario = C.IDUsuario");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.IDCompra = (int)datos.Lector["IDCompra"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    aux.Proveedor = new Proveedor
                    {
                        IDProveedor = (int)datos.Lector["IDProveedor"],
                        
                        Nombre = (string)datos.Lector["Nombre"]
                    };

                    aux.Usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"]
                    };

                    DetalleCompraNegocio detalleNegocio = new DetalleCompraNegocio();
                    aux.Detalles = detalleNegocio.ListarPorCompra(aux.IDCompra);

                    lista.Add(aux);
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
    }
}
