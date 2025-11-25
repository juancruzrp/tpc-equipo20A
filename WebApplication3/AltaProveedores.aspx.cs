using Acceso;
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
    public partial class AltaProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdProveedor.Enabled = false;

            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (id != null)
                {
                    ProveedoresNegocio negocio = new ProveedoresNegocio();
                    Proveedor seleccionado = negocio.obtenerPorId(int.Parse(id));

                    if (seleccionado != null)
                    {
                        txtIdProveedor.Text = seleccionado.IDProveedor.ToString();
                        txtNombre.Text = seleccionado.Nombre;
                        txtCUITCUIL.Text = seleccionado.CUIT_CUIL;
                        txtTelefono.Text = seleccionado.Telefono;
                        txtMail.Text = seleccionado.Mail;
                        txtDireccion.Text = seleccionado.Direccion;
                        txtPorcentaje.Text = seleccionado.Porcentaje.ToString();
                    }
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

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            Proveedor proveedor = new Proveedor();

            proveedor.Nombre = txtNombre.Text;
            proveedor.CUIT_CUIL = txtCUITCUIL.Text;
            proveedor.Telefono = txtTelefono.Text;
            proveedor.Mail = txtMail.Text;
            proveedor.Direccion = txtDireccion.Text;
            proveedor.Porcentaje = decimal.Parse(txtPorcentaje.Text);
            proveedor.Estado = true;

            if (string.IsNullOrEmpty(txtIdProveedor.Text))
                negocio.agregar(proveedor);
            else
            {
                proveedor.IDProveedor = int.Parse(txtIdProveedor.Text);
                negocio.modificar(proveedor);
            }

            Response.Redirect("Proveedores.aspx");
        }

    }
}