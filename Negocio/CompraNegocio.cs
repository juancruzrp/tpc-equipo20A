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
            SELECT 
                C.IDCompra, C.Fecha, C.Total,
                P.IDProveedor, P.Nombre AS NombreProveedor,
                U.IDUsuario, U.NombreUsuario,
                TU.TipoUsuario AS TipoUsuario
            FROM Compras C
            INNER JOIN Proveedores P ON P.IDProveedor = C.IDProveedor
            INNER JOIN Usuarios U ON U.IDUsuario = C.IDUsuario
            INNER JOIN TiposUsuario TU ON TU.IDTipoUsuario = U.IDTipoUsuario");

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
                        Nombre = datos.Lector["NombreProveedor"].ToString()
                    };

                    aux.Usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        NombreUsuario = datos.Lector["NombreUsuario"].ToString(),
                        TipoUsuario = new TipoUsuario
                        {
                            Descripcion = datos.Lector["TipoUsuario"].ToString()
                        }
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


        public Compra ObtenerPorID(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
            SELECT C.IDCompra, C.Fecha, C.Total,
                   P.IDProveedor, P.Nombre as NombreProv, P.CUIT_CUIL,
                   U.IDUsuario, U.NombreUsuario
            FROM Compras C
            INNER JOIN Proveedores P ON P.IDProveedor = C.IDProveedor
            INNER JOIN Usuarios U ON U.IDUsuario = C.IDUsuario
            WHERE C.IDCompra = @id");

                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Compra compra = null;

                if (datos.Lector.Read())
                {
                    compra = new Compra();
                    compra.IDCompra = (int)datos.Lector["IDCompra"];
                    compra.Fecha = (DateTime)datos.Lector["Fecha"];
                    compra.Total = (decimal)datos.Lector["Total"];

                    compra.Proveedor = new Proveedor();
                    compra.Proveedor.IDProveedor = (int)datos.Lector["IDProveedor"];
                    compra.Proveedor.Nombre = (string)datos.Lector["NombreProv"];
                    if (!(datos.Lector["CUIT_CUIL"] is DBNull))
                        compra.Proveedor.CUIT_CUIL = (string)datos.Lector["CUIT_CUIL"];

                    compra.Usuario = new Usuario();
                    compra.Usuario.IDUsuario = (int)datos.Lector["IDUsuario"];
                    compra.Usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    DetalleCompraNegocio detalleNegocio = new DetalleCompraNegocio();
                    compra.Detalles = detalleNegocio.ListarPorCompra(compra.IDCompra);
                }

                return compra;
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
