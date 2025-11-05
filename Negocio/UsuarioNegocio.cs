using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Acceso;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDUsuario, IDTipoUsuario FROM Usuarios where NombreUsuario = @user AND Contraseña = @pass");


                datos.setearParametro("@user", usuario.NombreUsuario);
                datos.setearParametro("@pass", usuario.Contraseña);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    usuario.IDUsuario = (int)datos.Lector["IDUsuario"];
                    usuario.TipoUsuario = new TipoUsuario();
                    usuario.TipoUsuario.IDTipoUsuario = Convert.ToInt32(datos.Lector["IDTipoUsuario"]);
                    return true;
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

        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
                   SELECT 
                   U.IDUsuario,
                   U.NombreUsuario,
                   U.Contraseña,
                   TU.TipoUsuario as Tipo,
                   U.FechaAlta,
                   U.FechaBaja,
                   U.Estado
                   FROM Usuarios U
                   INNER JOIN TiposUsuario TU ON U.IDTipoUsuario = TU.IDTipoUsuario";


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IDUsuario = (int)datos.Lector["IDUsuario"];

                    if (!(datos.Lector["NombreUsuario"] is DBNull))
                        aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];

                    if (!(datos.Lector["Contraseña"] is DBNull))
                        aux.Contraseña = (string)datos.Lector["Contraseña"];

                    aux.TipoUsuario = new TipoUsuario();
                    aux.TipoUsuario.Descripcion = datos.Lector["Tipo"] is DBNull ? "Sin tipo" : (string)datos.Lector["Tipo"];

                    if (!(datos.Lector["FechaAlta"] is DBNull))
                        aux.FechaAlta = (DateTime)datos.Lector["FechaAlta"];

                    if (!(datos.Lector["FechaBaja"] is DBNull))
                        aux.FechaBaja = (DateTime)datos.Lector["FechaBaja"];

                    if (!(datos.Lector["Estado"] is DBNull))
                        aux.Estado = (bool)datos.Lector["Estado"];

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

        public List<TipoUsuario> listarTipo()
        {
            List<TipoUsuario> lista = new List<TipoUsuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IDTipoUsuario, TipoUsuario FROM TiposUsuario");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    TipoUsuario tipo = new TipoUsuario();
                    tipo.IDTipoUsuario = (int)datos.Lector["IDTipoUsuario"];
                    tipo.Descripcion = (string)datos.Lector["TipoUsuario"];
                    lista.Add(tipo);
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

        public void agregar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (NombreUsuario, Contraseña, IDTipoUsuario, FechaAlta, Estado) VALUES (@nombre, @contraseña, @idTipo, @fechaAlta, @estado)");
                datos.setearParametro("@nombre", nuevo.NombreUsuario);
                datos.setearParametro("@contraseña", nuevo.Contraseña);
                datos.setearParametro("@idTipo", nuevo.TipoUsuario.IDTipoUsuario);
                datos.setearParametro("@fechaAlta", nuevo.FechaAlta);
                datos.setearParametro("@estado", nuevo.Estado);
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
