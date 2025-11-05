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

            CargarDropDown();
        }

        private void CargarDropDown()
        {
            // Cargar DropDownList de Proveedores
            UsuarioNegocio UsuarioNegocio = new UsuarioNegocio();
            ddlTipoUsuario.DataSource = UsuarioNegocio.listarTipo();
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccione un Tipo de usuario --", "0"));
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}