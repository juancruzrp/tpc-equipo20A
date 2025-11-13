using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;


namespace WebApplication3.Helpers
{
    public static class SesionHelper
    {
        public static bool EsUsuarioAdmin(HttpSessionState session)
        {
            var usuario = session["usuario"] as Usuario;
            return usuario != null && usuario.TipoUsuario.IDTipoUsuario == 2;
        }
    }
}