using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace WebApplication3
{
    public partial class HistorialVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cargarVentas();
        }

        private void cargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();

            List<Venta> ventasConDetalles = negocio.ListarVentasConDetalles();

            repVentas.DataSource = ventasConDetalles;
            repVentas.DataBind();

        }

        

    }
}