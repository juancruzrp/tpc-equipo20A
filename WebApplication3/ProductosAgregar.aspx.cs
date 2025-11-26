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
                    ProveedoresNegocio provNegocio = new ProveedoresNegocio();

                    ddlCategoria.DataSource = catNegocio.listar();
                    ddlCategoria.DataValueField = "IDCategoria";
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));

                    ddlMarca.DataSource = marNegocio.listar();
                    ddlMarca.DataValueField = "IDMarca";
                    ddlMarca.DataTextField = "Nombre";
                    ddlMarca.DataBind();
                    ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca", "0"));

                    ddlProveedor.DataSource = provNegocio.listar();
                    ddlProveedor.DataValueField = "IDProveedor";
                    ddlProveedor.DataTextField = "Nombre";
                    ddlProveedor.DataBind();
                    ddlProveedor.Items.Insert(0, new ListItem("Seleccione un proveedor", "0"));

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
                ddlProveedor.SelectedValue = seleccionado.IDProveedor.ToString();
                txtImagenUrl.Text = seleccionado.ImagenUrl;
                imgPreview.ImageUrl = seleccionado.ImagenUrl;
                
            }
        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    lblMensaje.Text = "El precio no es válido.";
                    return;
                }

                if (precio <= 0)
                {
                    lblMensaje.Text = "El precio debe ser mayor a 0.";
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int stock))
                {
                    lblMensaje.Text = "El stock no es válido.";
                    return;
                }

                if (stock <= 0)
                {
                    lblMensaje.Text = "El stock debe ser mayor a 0.";
                    return;
                }

                if (ddlCategoria.SelectedValue == "0")
                {
                    lblMensaje.Text = "Debe seleccionar una categoría.";
                    return;
                }

                if (ddlMarca.SelectedValue == "0")
                {
                    lblMensaje.Text = "Debe seleccionar una marca.";
                    return;
                }

                if (ddlProveedor.SelectedValue == "0")
                {
                    lblMensaje.Text = "Debe seleccionar un proveedor.";
                    return;
                }

                producto.Nombre = txtNombre.Text;
                producto.Descripcion = txtDescripcion.Text;
                producto.Precio = precio;
                producto.Stock = stock;

                producto.Categoria = new Categoria();
                producto.Categoria.IDCategoria = int.Parse(ddlCategoria.SelectedValue);

                producto.Marca = new Marca();
                producto.Marca.IDMarca = int.Parse(ddlMarca.SelectedValue);

                producto.IDProveedor = int.Parse(ddlProveedor.SelectedValue); 
                
                producto.ImagenUrl = string.IsNullOrWhiteSpace(txtImagenUrl.Text)
                    ? "https://us.123rf.com/450wm/koblizeek/koblizeek2208/koblizeek220800128/190320173-no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment-placeholder.jpg"
                    : txtImagenUrl.Text;

                producto.Estado = true;

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