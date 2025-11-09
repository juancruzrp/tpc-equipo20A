using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso;

namespace Negocio
{
    public class ProveedoresNegocio
    {

        public List<Proveedor> listar()
        {
            List<Proveedor> lista = new List<Proveedor>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT IDProveedor, Nombre, Telefono, Mail, Direccion, CUIT_CUIL FROM PROVEEDORES";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.IDProveedor = (int)datos.Lector["IDProveedor"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];
                    
                    if (!(datos.Lector["Mail"] is DBNull))
                        aux.Mail = (string)datos.Lector["Mail"];

                    if (!(datos.Lector["Direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["Direccion"];

                    if (!(datos.Lector["CUIT_CUIL"] is DBNull))
                        aux.CUIT_CUIL = (string)datos.Lector["CUIT_CUIL"];
                    else
                        aux.CUIT_CUIL = string.Empty;

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
