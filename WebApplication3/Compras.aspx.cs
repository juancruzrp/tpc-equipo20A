using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio; 
using Negocio;

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
                CargarDropdowns();
                CargarUsuarioActual();
               
                txtFecha.Text = DateTime.Today.ToString("yyyy-MM-dd"); // Establecer fecha actual
            }
           
        }

        private void CargarDropdowns()
        {
            // Cargar DropDownList de Proveedores
            ProveedoresNegocio proveedorNegocio = new ProveedoresNegocio();
            ddlProveedor.DataSource = proveedorNegocio.listar();
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("-- Seleccione Proveedor --", "0"));

            // Cargar DropDownList de Productos
            ProductoNegocio productoNegocio = new ProductoNegocio();
            ddlProducto.DataSource = productoNegocio.listar();
            ddlProducto.DataBind();
            ddlProducto.Items.Insert(0, new ListItem("-- Seleccione Producto --", "0"));

            // Cargar precio unitario del primer producto si existe
            /*ddlProducto_SelectedIndexChanged(null, EventArgs.Empty);*/
        }

        private void CargarUsuarioActual()
        {
            // aca va un check para que tipo de usuario es.
            
            lblUsuario.Text = "Admin"; // Hardcodeado para el ejemplo
         
            Session["IDUsuarioActual"] = 1; // ID de un usuario de ejemplo
        }




    }
}