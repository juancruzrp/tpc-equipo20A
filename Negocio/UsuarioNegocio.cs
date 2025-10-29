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

    }
}
