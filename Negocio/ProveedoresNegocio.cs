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


        public Proveedor obtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Incluimos CUIT_CUIL en la consulta
                datos.setearConsulta("SELECT IDProveedor, Nombre, CUIT_CUIL, Telefono, Mail, Direccion FROM PROVEEDORES WHERE IDProveedor = @IDProveedor");
                datos.setearParametro("@IDProveedor", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.IDProveedor = (int)datos.Lector["IDProveedor"];
                    aux.Nombre = !(datos.Lector["Nombre"] is DBNull) ? (string)datos.Lector["Nombre"] : string.Empty;
                    aux.CUIT_CUIL = !(datos.Lector["CUIT_CUIL"] is DBNull) ? (string)datos.Lector["CUIT_CUIL"] : string.Empty;
                    aux.Telefono = !(datos.Lector["Telefono"] is DBNull) ? (string)datos.Lector["Telefono"] : string.Empty;
                    aux.Mail = !(datos.Lector["Mail"] is DBNull) ? (string)datos.Lector["Mail"] : string.Empty;
                    aux.Direccion = !(datos.Lector["Direccion"] is DBNull) ? (string)datos.Lector["Direccion"] : string.Empty;
                    return aux;
                }
                return null;
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



        public void agregar(Proveedor nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Incluimos CUIT_CUIL en el INSERT
                datos.setearConsulta(@"
                    INSERT INTO Proveedores (Nombre, CUIT_CUIL, Telefono, Mail, Direccion)
                    VALUES (@Nombre, @CUIT_CUIL, @Telefono, @Mail, @Direccion)");

                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@CUIT_CUIL", nuevo.CUIT_CUIL ?? (object)DBNull.Value);
                datos.setearParametro("@Telefono", nuevo.Telefono ?? (object)DBNull.Value);
                datos.setearParametro("@Mail", nuevo.Mail ?? (object)DBNull.Value);
                datos.setearParametro("@Direccion", nuevo.Direccion ?? (object)DBNull.Value);

                datos.ejecutarAccion();
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

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Incluimos CUIT_CUIL en el UPDATE
                datos.setearConsulta(@"
                    UPDATE Proveedores
                    SET Nombre = @Nombre,
                        CUIT_CUIL = @CUIT_CUIL,
                        Telefono = @Telefono,
                        Mail = @Mail,
                        Direccion = @Direccion
                    WHERE IDProveedor = @IDProveedor");

                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@CUIT_CUIL", proveedor.CUIT_CUIL ?? (object)DBNull.Value);
                datos.setearParametro("@Telefono", proveedor.Telefono ?? (object)DBNull.Value);
                datos.setearParametro("@Mail", proveedor.Mail ?? (object)DBNull.Value);
                datos.setearParametro("@Direccion", proveedor.Direccion ?? (object)DBNull.Value);
                datos.setearParametro("@IDProveedor", proveedor.IDProveedor);

                datos.ejecutarAccion();
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
