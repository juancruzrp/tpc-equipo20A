using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication3.Helpers;

namespace WebApplication3
{
    public partial class AgregarProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIDProducto.Enabled = false;

            try
            {
                if (!IsPostBack)
                {
                    // config inicial de pantalla
                    CategoriaNegocio catNegocio = new CategoriaNegocio();
                    MarcaNegocio marNegocio = new MarcaNegocio();

                    ddlCategoria.DataSource = catNegocio.listar();
                    ddlCategoria.DataValueField = "IDCategoria";
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataBind();

                    ddlMarca.DataSource = marNegocio.listar();
                    ddlMarca.DataValueField = "IDMarca";
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataBind();

                    // si viene un id, se modifica
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        cargarProducto(id);
                        btnAceptar.Text = "Guardar cambios";
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                throw;
            }

            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
            else if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                Session.Add("Error", "No tienes permisos para acceder a esta sección.");
                Response.Redirect("Error.aspx");
            }
        }

        private void cargarProducto(int id)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto seleccionado = negocio.listar().FirstOrDefault(p => p.IDProducto == id);

            if (seleccionado != null)
            {
                hfIDProducto.Value = seleccionado.IDProducto.ToString();
                txtIDProducto.Text = seleccionado.IDProducto.ToString();
                txtNombre.Text = seleccionado.Nombre;
                txtDescripcion.Text = seleccionado.Descripcion;
                txtPrecio.Text = seleccionado.Precio.ToString("0.00");
                txtStock.Text = seleccionado.Stock.ToString();
                ddlCategoria.SelectedValue = seleccionado.Categoria.IDCategoria.ToString();
                ddlMarca.SelectedValue = seleccionado.Marca.IDMarca.ToString();
                txtImagenUrl.Text = seleccionado.ImagenUrl;
                imgPreview.ImageUrl = seleccionado.ImagenUrl;
                chkEstado.Checked = seleccionado.Estado;
            }
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    lblMensaje.Text = "El precio no es válido.";
                    return;
                }
                producto.Precio = precio;

                if (!int.TryParse(txtStock.Text, out int stock))
                {
                    lblMensaje.Text = "El stock no es válido.";
                    return;
                }
                producto.Stock = stock;

                producto.Categoria = new Categoria();
                producto.Categoria.IDCategoria = int.Parse(ddlCategoria.SelectedValue);

                producto.Marca = new Marca();
                producto.Marca.IDMarca = int.Parse(ddlMarca.SelectedValue);

                producto.ImagenUrl = string.IsNullOrWhiteSpace(txtImagenUrl.Text)
                    ? "https://us.123rf.com/450wm/koblizeek/koblizeek2208/koblizeek220800128/190320173-no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment-placeholder.jpg"
                    : txtImagenUrl.Text;

                producto.Estado = chkEstado.Checked;

                // detectar si es modificación o alta
                if (!string.IsNullOrEmpty(hfIDProducto.Value))
                {
                    producto.IDProducto = int.Parse(hfIDProducto.Value);
                    negocio.modificar(producto);

                    lblMensaje.Text = "Producto modificado correctamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    negocio.agregar(producto);

                    lblMensaje.Text = "Producto agregado correctamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }

                // redirigir después de guardar
                Response.Redirect("Productos.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Productos.aspx");
        }
    }
}