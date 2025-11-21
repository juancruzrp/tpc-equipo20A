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
                int idProveedor = int.Parse(idStr);
                ProveedoresNegocio negocioProv = new ProveedoresNegocio();
                List<Proveedor> listaProv = negocioProv.listar();
                Proveedor seleccionado = listaProv.Find(x => x.IDProveedor == idProveedor);

                if (seleccionado != null)
                {
                    txtCuit.Text = seleccionado.CUIT_CUIL;

                    // --- NUEVO: Cargar los productos de este proveedor ---
                    CargarProductosDelProveedor(idProveedor);

                    // Habilitar el buscador de productos ahora que hay proveedor
                    txtBuscarProducto.Enabled = true;
                    txtBuscarProducto.Text = ""; // Limpiar búsqueda anterior
                    hfIDProducto.Value = "";     // Limpiar ID anterior
                    txtPrecioUnitario.Text = ""; // Limpiar precio anterior
                }
            }
        }

        // Método auxiliar para generar el HTML de productos
        private void CargarProductosDelProveedor(int idProveedor)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            // Llamamos al método nuevo que filtra por SQL
            List<Producto> lista = negocio.listarPorProveedor(idProveedor);

            string html = "";
            foreach (var item in lista)
            {
                // Pasamos ID, Nombre y PRECIO al JavaScript directamente
                // Nota: item.Precio.ToString() usa la cultura del servidor, a veces conviene reemplazar la coma por punto para JS
                string precioFormateado = item.Precio.ToString("0.00");

                html += $"<a href='#' class='dropdown-item' onclick='seleccionarProducto({item.IDProducto}, \"{item.Nombre}\", \"{precioFormateado}\"); return false;'>{item.Nombre}</a>";
            }

            if (string.IsNullOrEmpty(html))
            {
                html = "<span class='dropdown-item disabled'>Este proveedor no tiene productos asignados.</span>";
            }

            litProductos.Text = html;
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