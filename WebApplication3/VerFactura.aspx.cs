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
    public partial class VerFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idCompra = int.Parse(idStr);
                    CargarDatos(idCompra);
                }
            }
        }

        private void CargarDatos(int id)
        {
            CompraNegocio negocio = new CompraNegocio();
            try
            {
                Compra compra = negocio.ObtenerPorID(id);

                if (compra != null)
                {
                    lblIDCompra.Text = compra.IDCompra.ToString("D8"); 
                    lblFecha.Text = compra.Fecha.ToShortDateString();

                    lblProveedor.Text = compra.Proveedor.Nombre;
                    lblCuit.Text = compra.Proveedor.CUIT_CUIL; 

                    lblUsuario.Text = compra.Usuario.NombreUsuario;

                    lblTotal.Text = compra.Total.ToString("C");

                    repDetalles.DataSource = compra.Detalles;
                    repDetalles.DataBind();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}