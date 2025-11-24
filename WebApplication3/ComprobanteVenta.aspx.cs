using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class ComprobanteVenta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int idVenta = int.Parse(idStr);
                    CargarDatos(idVenta);
                }
            }
        }

        private void CargarDatos(int idVenta)
        {
            VentaNegocio negocio = new VentaNegocio();
            Venta venta = negocio.ObtenerVentaConDetalles(idVenta);

            if (venta != null)
            {
                lblIDVenta.Text = venta.IDVenta.ToString("D8");
                lblFecha.Text = venta.Fecha.ToString("dd/MM/yyyy HH:mm");
                lblCliente.Text = venta.Cliente.Nombre + " " + venta.Cliente.Apellido;
                lblUsuario.Text = venta.Usuario.NombreUsuario;
                lblClienteCuit.Text = venta.Cliente.CUIT_CUIL;
                lblTotal.Text = venta.Total.ToString("C2");

                repDetalles.DataSource = venta.Detalles;
                repDetalles.DataBind();
            }
        }
    }
}