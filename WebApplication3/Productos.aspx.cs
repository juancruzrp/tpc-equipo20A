using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebApplication3
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           CargarProductos();            
        }

        private void CargarProductos()
        {
            ProductoNegocio negocio = new Negocio.ProductoNegocio();
            dgvProductos.DataSource = negocio.listar();
            dgvProductos.DataBind();

                        
        }

    }
}