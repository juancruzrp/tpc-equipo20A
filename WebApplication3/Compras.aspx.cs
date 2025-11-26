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
                CargarMarcas();


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
                if (item.Estado)
                {
                    html += $"<a href='#' class='dropdown-item' onclick='seleccionarProveedor({item.IDProveedor}, \"{item.Nombre}\", \"{item.CUIT_CUIL}\"); return false;'>{item.Nombre}</a>";
                }
            }
            litProveedores.Text = html;
        }
        private void CargarMarcas()
        {
            
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listar(); 
            ddlMarca.DataSource = lista;
            ddlMarca.DataTextField = "Nombre"; 
            ddlMarca.DataValueField = "IDMarca";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("-- Todas --", "0"));
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
                int idMarca = item.Marca != null ? item.Marca.IDMarca : 0;
                string nombreSeguro = item.Nombre
                                        .Replace("'", "&#39;")
                                        .Replace("\"", "\\\"");

                html += $"<a href='#' class='dropdown-item' onclick='seleccionarProducto({item.IDProducto}, \"{nombreSeguro}\", \"{precioFormateado}\"); return false;'>{item.Nombre}</a>";
            }

            if (string.IsNullOrEmpty(html))
            {
                html = "<span class='dropdown-item disabled'>Este proveedor no tiene productos asignados.</span>";
            }

            litProductos.Text = html;
        }

        private void CargarUsuarioActual()
        {
            Usuario usuarioLogueado = (Usuario)Session["Usuario"];

            if (usuarioLogueado != null)
            {
               // lblUsuario.Text = usuarioLogueado.NombreUsuario;
            }
            else
            {
                Response.Redirect("~/Login.aspx", false);
            }
        }

        private int ObtenerCantidad()
        {
            if (int.TryParse(txtCantidad.Text, out int cant) && cant > 0)
                return cant;

            return 1;
        }

        private decimal ObtenerPrecio()
        {
            string texto = txtPrecioUnitario.Text.Replace(",", ".");

            if (decimal.TryParse(texto,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out decimal pre))
                return pre;

            return 0;
        }

        private decimal CalcularTotal()
        {
            return CarritoCompras.Sum(x => x.PrecioUnitario * x.Cantidad);
        }


        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {

           if (string.IsNullOrEmpty(hfIDProducto.Value))
                return;

            DetalleCompra detalle = new DetalleCompra()
            {
                Producto = new Producto()
                {
                    IDProducto = int.Parse(hfIDProducto.Value),
                    Nombre = txtBuscarProducto.Text
                },
                Cantidad = ObtenerCantidad(),
                PrecioUnitario = ObtenerPrecio()
            };

            CarritoCompras.Add(detalle);

            ActualizarGrilla();
            LimpiarCamposProducto();
        }


        private void ActualizarGrilla()
        {
            dgvDetalleCompra.DataSource = CarritoCompras;
            dgvDetalleCompra.DataBind();

            lblTotalCompra.Text = CalcularTotal().ToString("C");
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
            CarritoCompras.RemoveAt(e.RowIndex);
            ActualizarGrilla();
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpia para que no queden productos viejos
            Session["CarritoCompras"] = null;
            Response.Redirect("ComprasListado.aspx");
        }

        protected void btnGuardarCompra_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["Usuario"] == null)
                {
                    Response.Write("<script>alert('La sesión expiró. Iniciá sesión nuevamente.');</script>");
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                Usuario usuarioLogueado = (Usuario)Session["Usuario"];

                if (string.IsNullOrEmpty(hfIDProveedor.Value))
                {
                    Response.Write("<script>alert('Debe seleccionar un proveedor.');</script>");
                    return;
                }

                if (CarritoCompras == null || CarritoCompras.Count == 0)
                {
                    Response.Write("<script>alert('El carrito de compras está vacío.');</script>");
                    return;
                }


                Compra nuevaCompra = new Compra();

                nuevaCompra.Proveedor = new Proveedor();
                nuevaCompra.Proveedor.IDProveedor = int.Parse(hfIDProveedor.Value);

                nuevaCompra.Usuario = new Usuario();
                nuevaCompra.Usuario.IDUsuario = usuarioLogueado.IDUsuario; 
                nuevaCompra.Fecha = DateTime.Parse(txtFecha.Text);
                nuevaCompra.Total = CarritoCompras.Sum(x => x.PrecioUnitario * x.Cantidad);
                nuevaCompra.Detalles = CarritoCompras;

             
                CompraNegocio negocio = new CompraNegocio();
                int idGenerado = negocio.Agregar(nuevaCompra);

           
                Session["CarritoCompras"] = null;

                string script = "<script>alert('Compra guardada exitosamente.'); window.location='ComprasListado.aspx';</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }
    }
}