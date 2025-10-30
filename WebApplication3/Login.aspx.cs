using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebApplication3
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContraseña.Text))
                {
                    lblError.Text = "Debe completar ambos campos."; 
                    return;
                }



                usuario.NombreUsuario = txtUsuario.Text;
                usuario.Contraseña = txtContraseña.Text;
                
                    if(negocio.Loguear(usuario))
                    {
                        Session.Add("Usuario", usuario);
                        Response.Redirect("~/Inicio.aspx",false);
                    }
                    else
                    {
                    Session.Add("Error", "Nombre de Usuario o Contraseña incorrectos.");
                    Response.Redirect("~/Error.aspx",false);
                }

            }
            catch (Exception ex )
            {

                Session.Add("Error", ex.ToString());
                Response.Redirect("~/Error.aspx");
            }
        }
    }
}