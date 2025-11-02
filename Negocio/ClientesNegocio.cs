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
    }
}
