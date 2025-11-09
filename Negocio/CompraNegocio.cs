using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CompraNegocio
    {


        public List<Compra> Listar()
        {
            List<Compra> lista = new List<Compra>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // *** CAMBIO AQUÍ EN LA CONSULTA SQL ***
                // Ahora seleccionamos P.Nombre y lo mantenemos con el nombre "Nombre"
                // Ya no necesitamos el alias "AS NombreProveedor"
                datos.setearConsulta(@"
                    SELECT C.IDCompra, C.Fecha, C.Total,
                           P.IDProveedor, P.Nombre,     -- <<--- ¡QUITAMOS el alias NombreProveedor!
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
                        // *** CAMBIO AQUÍ EN LA ASIGNACIÓN ***
                        // Ahora leemos directamente "Nombre" de la base de datos
                        // y lo asignamos a la propiedad Nombre del objeto Proveedor
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
