using Acceso;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class AltaUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdUsuario.Enabled = false;
            if (!IsPostBack)
            {
                try
                {
                    CargarDropDown();
                    
                }
                catch (Exception ex)
                {

                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
        }

        private void CargarDropDown()
        {
            // Cargar DropDownList de Tipos de usuario
            UsuarioNegocio UsuarioNegocio = new UsuarioNegocio();
            ddlTipoUsuario.DataSource = UsuarioNegocio.listarTipo();
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccione un Tipo de usuario --", "0"));
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.NombreUsuario = txtNombreUsuario.Text;
                usuario.Contraseña = txtContraseña.Text;

                usuario.TipoUsuario = new TipoUsuario();
                usuario.TipoUsuario.IDTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);

                usuario.FechaAlta = DateTime.Now;
                usuario.Estado = CheckEstado.Checked;
                negocio.agregar(usuario);
                Response.Redirect("Usuarios.aspx",false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
             
        }
    }
}