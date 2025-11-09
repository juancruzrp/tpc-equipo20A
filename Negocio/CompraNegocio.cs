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
                        
                    };
                   
                    aux.Proveedor.NombreProveedor = (string)datos.Lector["NombreProveedor"];


                    aux.Usuario = new Usuario
                    {
                        IDUsuario = (int)datos.Lector["IDUsuario"],
                       
                    };
                  
                    aux.Usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];


                    
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
