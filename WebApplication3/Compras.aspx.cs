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

                    CargarProductosDelProveedor(idProveedor);

                    txtBuscarProducto.Enabled = true;
                    txtBuscarProducto.Text = "";  
                    hfIDProducto.Value = "";    
                    txtPrecioUnitario.Text = ""; 
                }
            }
        }


        private void CargarProductosDelProveedor(int idProveedor)
        {
            ProductoNegocio negocio = new ProductoNegocio();

            
            List<Producto> lista = negocio.listarPorProveedor(idProveedor);

            string html = "";
            foreach (var item in lista)
            {
                string precioFormateado = item.Precio.ToString("0.00");

                html += $"<a href='#' class='dropdown-item' onclick='seleccionarProducto({item.IDProducto}, \"{item.Nombre}\", \"{precioFormateado}\"); return false;'>{item.Nombre}</a>";
            }

            if (string.IsNullOrEmpty(html))
            {
                html = "<span class='dropdown-item disabled'>Este proveedor no tiene productos asignados.</span>";
            }

            litProductos.Text = html;
        }


        private void CargarUsuarioActual()
        {
            
            lblUsuario.Text = "Admin"; 
         
            Session["IDUsuarioActual"] = 1; 
        }


        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(hfIDProducto.Value))
            {
            
                return;
            }

            int cantidad;
            decimal precio;

            if (!int.TryParse(txtCantidad.Text, out cantidad) || cantidad < 1) cantidad = 1;

            if (!decimal.TryParse(txtPrecioUnitario.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out precio))
            {
              
                decimal.TryParse(txtPrecioUnitario.Text, out precio);
            }

            DetalleCompra detalle = new DetalleCompra();
            detalle.Producto = new Producto();

            detalle.Producto.IDProducto = int.Parse(hfIDProducto.Value);
            detalle.Producto.Nombre = txtBuscarProducto.Text; 

            detalle.Cantidad = cantidad;
            detalle.PrecioUnitario = precio;

            List<DetalleCompra> carrito = CarritoCompras; 
            carrito.Add(detalle);
            CarritoCompras = carrito; 

            ActualizarGrilla();
            LimpiarCamposProducto();
        }

        private void ActualizarGrilla()
        {
            dgvDetalleCompra.DataSource = CarritoCompras;
            dgvDetalleCompra.DataBind();

            decimal totalCompra = 0;
            foreach (var item in CarritoCompras)
            {
                totalCompra += item.PrecioUnitario * item.Cantidad;
            }

            lblTotalCompra.Text = totalCompra.ToString("C"); 
        }

        private void LimpiarCamposProducto()
        {
            txtBuscarProducto.Text = "";
            hfIDProducto.Value = "";
            txtPrecioUnitario.Text = "";
            txtCantidad.Text = "1";
            txtBuscarProducto.Focus();
        }

        protected void dgvDetalleCompra_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index = e.RowIndex;

            List<DetalleCompra> carrito = CarritoCompras;
            carrito.RemoveAt(index);
            CarritoCompras = carrito;

            ActualizarGrilla();
        }



    }
}