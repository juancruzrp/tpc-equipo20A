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
                // **CAMBIO AQUÍ: P.Nombre en lugar de P.NombreProveedor**
                datos.setearConsulta(@"
                    SELECT C.IDCompra, C.Fecha, C.Total,
                           P.IDProveedor, P.Nombre AS NombreProveedor, -- Alias para que coincida con tu propiedad C#
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
                        // NombreProveedor = (string)datos.Lector["NombreProveedor"] // Esto ya es correcto con el alias
                    };
                    // Asegúrate de que la propiedad NombreProveedor exista en tu clase Dominio.Proveedor
                    aux.Proveedor.NombreProveedor = (string)datos.Lector["NombreProveedor"];


                    aux.Usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                        // NombreUsuario = (string)datos.Lector["NombreUsuario"] // Esto ya es correcto
                    };
                    // Asegúrate de que la propiedad NombreUsuario exista en tu clase Dominio.Usuario
                    aux.Usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];


                    // Carga los detalles de esa compra
                    // Asegúrate de que DetalleCompraNegocio y ListarPorCompra estén implementados correctamente
                    // y que la clase Compra tiene una propiedad para la lista de detalles.
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
