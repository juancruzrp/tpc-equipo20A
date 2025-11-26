using Acceso;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClientesNegocio
    {
        public List<Cliente> listar()
        {
            List<Cliente> lista = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                string consulta = "SELECT IDCliente, Nombre, Apellido, Telefono, Mail, Direccion, CUIT_CUIL FROM Clientes";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IDCliente = (int)datos.Lector["IDCliente"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Apellido"] is DBNull))
                        aux.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                        aux.Telefono = (string)datos.Lector["Telefono"];

                    if (!(datos.Lector["Mail"] is DBNull))
                        aux.Mail = (string)datos.Lector["Mail"];

                    if (!(datos.Lector["Direccion"] is DBNull))
                        aux.Direccion = (string)datos.Lector["Direccion"];


                    if (!(datos.Lector["CUIT_CUIL"] is DBNull))
                        aux.CUIT_CUIL = (string)datos.Lector["CUIT_CUIL"];
                    else
                        aux.CUIT_CUIL = "";

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al listar clientes: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Cliente nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "INSERT INTO Clientes (Nombre, Apellido, Telefono, Mail, Direccion, CUIT_CUIL) " +
                                  "VALUES (@Nombre, @Apellido, @Telefono, @Mail, @Direccion, @CUIT_CUIL)";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Apellido", nuevo.Apellido);
                datos.setearParametro("@Telefono", nuevo.Telefono);
                datos.setearParametro("@Mail", nuevo.Mail);
                datos.setearParametro("@Direccion", nuevo.Direccion);
                datos.setearParametro("@CUIT_CUIL", nuevo.CUIT_CUIL);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar cliente: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public Cliente buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Cliente cliente = null;
            try
            {
                string consulta = "SELECT IDCliente, Nombre, Apellido, Telefono, Mail, Direccion, CUIT_CUIL " +
                                  "FROM Clientes WHERE IDCliente = @IDCliente";
                datos.setearConsulta(consulta);
                datos.setearParametro("@IDCliente", id);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    cliente = new Cliente();
                    cliente.IDCliente = (int)datos.Lector["IDCliente"];
                    cliente.Nombre = (string)datos.Lector["Nombre"];
                    cliente.Apellido = (string)datos.Lector["Apellido"];
                    cliente.Telefono = (string)datos.Lector["Telefono"];
                    cliente.Mail = (string)datos.Lector["Mail"];
                    cliente.Direccion = (string)datos.Lector["Direccion"];
                    cliente.CUIT_CUIL = (string)datos.Lector["CUIT_CUIL"];
                }
                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar cliente por ID: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Cliente> Buscar(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Cliente> lista = new List<Cliente>();

            datos.setearConsulta("SELECT IDCliente, Nombre, Apellido, CUIT_CUIL FROM Clientes WHERE Nombre LIKE @nombre + '%' ");
            datos.setearParametro("@nombre", nombre);
            datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                Cliente c = new Cliente();
                c.IDCliente = (int)datos.Lector["IDCliente"];
                c.Nombre = datos.Lector["Nombre"].ToString();
                c.Apellido = datos.Lector["Apellido"].ToString();
                c.CUIT_CUIL = datos.Lector["CUIT_CUIL"].ToString();
                lista.Add(c);
            }

            return lista;
        }

        public void modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Telefono = @Telefono, " +
                                  "Mail = @Mail, Direccion = @Direccion, CUIT_CUIL = @CUIT_CUIL WHERE IDCliente = @IDCliente";
                datos.setearConsulta(consulta);
                datos.setearParametro("@Nombre", cliente.Nombre);
                datos.setearParametro("@Apellido", cliente.Apellido);
                datos.setearParametro("@Telefono", cliente.Telefono);
                datos.setearParametro("@Mail", cliente.Mail);
                datos.setearParametro("@Direccion", cliente.Direccion);
                datos.setearParametro("@CUIT_CUIL", cliente.CUIT_CUIL);
                datos.setearParametro("@IDCliente", cliente.IDCliente);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cliente: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteCliente(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM Clientes WHERE CUIT_CUIL = @cuit");
                datos.setearParametro("@cuit", cliente.CUIT_CUIL);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int count = (int)datos.Lector[0];
                    return count > 0;
                }
                return false;
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


