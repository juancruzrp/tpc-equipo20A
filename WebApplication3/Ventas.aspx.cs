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
        private List<Cliente> Clientes
        {
            get
            {
                if (Session["Clientes"] == null)
                {
                    ClientesNegocio negocio = new ClientesNegocio();
                    Session["Clientes"] = negocio.listar();
                }
                return (List<Cliente>)Session["Clientes"];
            }
        }

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

            foreach (var c in Clientes)
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
            var cliente = Clientes.FirstOrDefault(c => c.IDCliente == idCliente);

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

            ddlProveedor.DataSource = provNeg.listar();
            ddlProveedor.DataBind();
            ddlProveedor.Items.Insert(0, new ListItem("Todos los proveedores", ""));

            ddlCategoria.DataSource = catNeg.listar();
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas las categorias", ""));

            ddlMarca.DataSource = marcaNeg.listar();
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas las marcas", ""));
        }




    }




}


