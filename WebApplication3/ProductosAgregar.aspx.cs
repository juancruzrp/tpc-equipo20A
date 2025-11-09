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

                    
                }

                // config para modificar
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    cargarProducto(id);
                    btnAceptar.Text = "Guardar cambios";
                }

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                throw;
                //agregar redireccion?
            }

        }

        
        protected void btnAceptar_Click (object sender, EventArgs e)
        {
            try
            {
                Producto nuevo = new Producto();
                ProductoNegocio negocio = new ProductoNegocio();
                var lista = negocio.listar();
                if (lista.Any(p => p.Nombre.Equals(nuevo.Nombre, StringComparison.OrdinalIgnoreCase)
                    && p.Marca.IDMarca == nuevo.Marca.IDMarca))
                {
                    lblMensaje.Text = "Este producto ya existe.";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                
                if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    lblMensaje.Text = "El precio no es válido.";
                    return;
                }
                nuevo.Precio = precio;

                if (!int.TryParse(txtStock.Text, out int stock))
                {
                    lblMensaje.Text = "El stock no es válido.";
                    return;
                }
                nuevo.Stock = stock;

                nuevo.Categoria = new Categoria();
                nuevo.Categoria.IDCategoria = int.Parse(ddlCategoria.SelectedValue);
                                                
                nuevo.Marca = new Marca();
                nuevo.Marca.IDMarca = int.Parse(ddlMarca.SelectedValue);
                
                nuevo.ImagenUrl = txtImagenUrl.Text;
                if (string.IsNullOrWhiteSpace(txtImagenUrl.Text))
                    nuevo.ImagenUrl = "https://us.123rf.com/450wm/koblizeek/koblizeek2208/koblizeek220800128/190320173-no-image-vector-symbol-missing-available-icon-no-gallery-for-this-moment-placeholder.jpg";
                else
                    nuevo.ImagenUrl = txtImagenUrl.Text;

                nuevo.Estado = chkEstado.Checked;

                negocio.agregar(nuevo);

                lblMensaje.Text = "Producto agregado correctamente!";
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                                
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                txtPrecio.Text = "";
                txtStock.Text = "";
                txtImagenUrl.Text = "";
                ddlCategoria.SelectedIndex = 0;
                ddlMarca.SelectedIndex = 0;
                chkEstado.Checked = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al agregar producto: " + ex.Message;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}