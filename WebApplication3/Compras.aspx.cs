using Dominio; 
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Compras : System.Web.UI.Page
    {
        // Propiedad para almacenar los detalles de la compra en la sesión
       
        private List<DetalleCompra> CarritoCompras
        {
            get
            {
                if (Session["CarritoCompras"] == null)
                {
                    Session["CarritoCompras"] = new List<DetalleCompra>();
                }
                return (List<DetalleCompra>)Session["CarritoCompras"];
            }
            set { Session["CarritoCompras"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarUsuarioActual();
               
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd"); 
            }
           
        }

        private void CargarProveedores()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            List<Proveedor> lista = negocio.listar(); 
            string html = "";
            foreach (var item in lista)
            {
                html += $"<a href='#' class='dropdown-item' onclick='seleccionarProveedor({item.IDProveedor}, \"{item.Nombre}\", \"{item.CUIT_CUIL}\"); return false;'>{item.Nombre}</a>";
            }
            litProveedores.Text = html;
        }

        protected void btnCargarCuit_Click(object sender, EventArgs e)
        {
            string idStr = hfIDProveedor.Value;

            if (!string.IsNullOrEmpty(idStr))
            {
                int id = int.Parse(idStr);
                ProveedoresNegocio negocio = new ProveedoresNegocio();
                List<Proveedor> lista = negocio.listar();
                Proveedor seleccionado = lista.Find(x => x.IDProveedor == id);

                if (seleccionado != null)
                {
                    txtCuit.Text = seleccionado.CUIT_CUIL;
                    ActualizarGrid();
                }
            }
        }




        private void ActualizarGrid()
        {
            dgvDetalleCompra.DataSource = CarritoCompras;
            dgvDetalleCompra.DataBind();

            decimal total = CarritoCompras.Sum(x => x.Cantidad * x.PrecioUnitario);
            lblTotalCompra.Text = total.ToString("0.00");
        }

        private void CargarUsuarioActual()
        {
            
            lblUsuario.Text = "Admin"; 
         
            Session["IDUsuarioActual"] = 1; 
        }




    }
}