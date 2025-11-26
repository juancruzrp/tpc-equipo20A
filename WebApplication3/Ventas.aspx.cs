using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                CargarClientes();
                CargarFiltros();

                ProductoNegocio negocio = new ProductoNegocio();
                var productos = negocio.listar();

                litProductos.Text = $"<script>const productos = {Newtonsoft.Json.JsonConvert.SerializeObject(productos)};</script>";

            }
        }

        private void CargarClientes()
        {
            ddlClientes.Items.Clear();

            ddlClientes.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccionar cliente...", ""));

            ClientesNegocio negocio = new ClientesNegocio();
            var lista = negocio.listar();

            foreach (var c in lista)
            {
                ddlClientes.Items.Add(new System.Web.UI.WebControls.ListItem(c.NombreCompleto, c.IDCliente.ToString()));
            }

            ddlClientes.SelectedIndex = 0; 
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlClientes.SelectedValue))
            {
                txtCUIT.Text = "";
                hdnIDCliente.Value = "";
                lblError.Text = "Debe seleccionar un cliente.";
                return;
            }

            int idCliente = int.Parse(ddlClientes.SelectedValue);

            ClientesNegocio negocio = new ClientesNegocio();
            var cliente = negocio.listar().FirstOrDefault(c => c.IDCliente == idCliente);

            if (cliente != null)
            {
                txtCUIT.Text = cliente.CUIT_CUIL;
                hdnIDCliente.Value = cliente.IDCliente.ToString();
                lblError.Text = "";
            }
            else
            {
                txtCUIT.Text = "";
                hdnIDCliente.Value = "";
                lblError.Text = "Cliente no encontrado.";
            }
        }

        protected void Filtros_Changed(object sender, EventArgs e)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            var lista = negocio.listar();

            if (!string.IsNullOrEmpty(ddlProveedor.SelectedValue))
                lista = lista.Where(p => p.IDProveedor.ToString() == ddlProveedor.SelectedValue).ToList();

            if (!string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                lista = lista.Where(p => p.Categoria.IDCategoria.ToString() == ddlCategoria.SelectedValue).ToList();

            if (!string.IsNullOrEmpty(ddlMarca.SelectedValue))
                lista = lista.Where(p => p.Marca.IDMarca.ToString() == ddlMarca.SelectedValue).ToList();

            // actualizar el array productos de JavaScript
            litProductos.Text = $"<script>const productos = {Newtonsoft.Json.JsonConvert.SerializeObject(lista)};</script>";
        }

        private void CargarFiltros()
        {
            ProveedoresNegocio provNeg = new ProveedoresNegocio();
            CategoriaNegocio catNeg = new CategoriaNegocio();
            MarcaNegocio marcaNeg = new MarcaNegocio();

            var todosProv = provNeg.listar();

            var proveedoresActivos = todosProv.Where(p => p.Estado).ToList();

            ddlProveedor.DataSource = proveedoresActivos;
            ddlProveedor.DataTextField = "Nombre"; // valor que se muestra
            ddlProveedor.DataValueField = "IDProveedor"; // valor que se guarda
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Todos los proveedores", ""));

            ddlCategoria.DataSource = catNeg.listar();
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas las categorias", ""));

            ddlMarca.DataSource = marcaNeg.listar();
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas las marcas", ""));
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hdnIDCliente.Value))
            {
                lblError.Text = "Debe seleccionar un cliente.";
                return;
            }

            int idCliente = int.Parse(hdnIDCliente.Value);

            Usuario usuario = (Usuario)Session["Usuario"];
            int idUsuario = usuario.IDUsuario;            

            DateTime fecha = DateTime.Now;

            List<ProductoVenta> productosVenta = new List<ProductoVenta>();
            if (!string.IsNullOrEmpty(hdnProductosVenta.Value))
            {
                productosVenta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductoVenta>>(hdnProductosVenta.Value);
            }

            if (productosVenta.Count == 0)
            {
                lblError.Text = "Debe agregar al menos un producto a la venta.";
                return;
            }

            // lista de detalles de venta
            List<DetalleVenta> detalles = productosVenta.Select(p => new DetalleVenta
            {
                Producto = new Producto { IDProducto = p.IDProducto },
                Cantidad = p.Cantidad,
                PrecioUnitario = p.Precio
            }).ToList();

            // Guardar venta en la base de datos
            VentaNegocio ventasNeg = new VentaNegocio();
            int idVenta = ventasNeg.GuardarVenta(idCliente, idUsuario, fecha, detalles);

            

            
            hdnProductosVenta.Value = "";

            string script = "alert('Venta registrada con éxito.');";
            ClientScript.RegisterStartupScript(this.GetType(), "VentaExitosa", script, true);
        }

    }
}


