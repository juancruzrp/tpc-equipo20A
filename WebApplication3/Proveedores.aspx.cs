using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Proveedores : System.Web.UI.Page
    {
       
            protected void Page_Load(object sender, EventArgs e)
        {
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            dgvProveedores.DataSource = negocio.listar();
            dgvProveedores.DataBind();
        }
    }
    
}