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
                string consulta = "SELECT IDProveedor, Nombre, Telefono, Mail, Direccion, CUIT_CUIL,  Estado FROM PROVEEDORES";

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
                    aux.Estado = (bool)datos.Lector["Estado"]; // Leemos el estado


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
                
                datos.setearConsulta("SELECT IDProveedor, Nombre, CUIT_CUIL, Telefono, Mail, Direccion, Estado FROM PROVEEDORES WHERE IDProveedor = @IDProveedor");
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
                    aux.Estado = (bool)datos.Lector["Estado"]; 
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
                
                datos.setearConsulta(@"
                    INSERT INTO Proveedores (Nombre, CUIT_CUIL, Telefono, Mail, Direccion, Estado)
                    VALUES (@Nombre, @CUIT_CUIL, @Telefono, @Mail, @Direccion, 1)");

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
                
                datos.setearConsulta(@"
                    UPDATE Proveedores
                    SET Nombre = @Nombre,
                        CUIT_CUIL = @CUIT_CUIL,
                        Telefono = @Telefono,
                        Mail = @Mail,
                        Direccion = @Direccion
                        Estado = @Estado
                    WHERE IDProveedor = @IDProveedor");

                datos.setearParametro("@Nombre", proveedor.Nombre);
                datos.setearParametro("@CUIT_CUIL", proveedor.CUIT_CUIL ?? (object)DBNull.Value);
                datos.setearParametro("@Telefono", proveedor.Telefono ?? (object)DBNull.Value);
                datos.setearParametro("@Mail", proveedor.Mail ?? (object)DBNull.Value);
                datos.setearParametro("@Direccion", proveedor.Direccion ?? (object)DBNull.Value);
                datos.setearParametro("@Estado", proveedor.Estado);
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

        public void CambiarEstado(Proveedor proveedor) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Estado = @estado WHERE IDProveedor = @id"); // Cambia Estado a 0 (inactivo)
                datos.setearParametro("@Estado", proveedor.Estado);
                datos.setearParametro("@id", proveedor.IDProveedor);
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
