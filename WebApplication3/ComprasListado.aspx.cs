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
    public partial class ComprasListado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            CompraNegocio negocio = new CompraNegocio();
            try
            {
                List<Compra> listaCompras = negocio.Listar();
                repCompras.DataSource = listaCompras;
                repCompras.DataBind();
            }
            catch (Exception ex)
            {
                
                Response.Write("<script>alert('Error al cargar las compras: " + ex.Message + "');</script>");
               
            }
        }
    }
}