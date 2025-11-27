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
            if (string.IsNullOrWhiteSpace(txtCUITCUIL.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtPorcentaje.Text))
            {
                lblError.Text = "Todos los campos son obligatorios.";
                return;
            }

            if (!txtCUITCUIL.Text.All(char.IsDigit) || txtCUITCUIL.Text.Length != 11)
            {
                lblError.Text = "El CUIT/CUIL debe tener exactamente 11 dígitos numéricos.";
                return;
            }

            string nombre = txtNombre.Text.Trim();
            int cantidadLetras = nombre.Count(char.IsLetter);
            if (cantidadLetras < 2)
            {
                lblError.Text = "El nombre debe contener al menos 2 letras.";
                return;
            }

            string telefono = txtTelefono.Text.Trim();
            if (!telefono.All(char.IsDigit) || telefono.Length < 8 || telefono.Length > 15)
            {
                lblError.Text = "El teléfono debe contener entre 8 y 15 dígitos numéricos.";
                return;
            }

            try
            {
                var mail = new System.Net.Mail.MailAddress(txtMail.Text.Trim());
                if (mail.Address != txtMail.Text.Trim())
                {
                    lblError.Text = "El correo electrónico no tiene un formato válido.";
                    return;
                }
            }
            catch
            {
                lblError.Text = "El correo electrónico no tiene un formato válido.";
                return;
            }

            if (txtDireccion.Text.Trim().Length < 5)
            {
                lblError.Text = "La dirección debe tener al menos 5 caracteres.";
                return;
            }

            if (!decimal.TryParse(txtPorcentaje.Text, out decimal porcentaje) || porcentaje < 1 || porcentaje > 100)
            {
                lblError.Text = "El porcentaje debe ser un número válido entre 1 y 100.";
                return;
            }
            try
            {
                Proveedor proveedor = new Proveedor();
                ProveedoresNegocio negocio = new ProveedoresNegocio();

                proveedor.Nombre = txtNombre.Text;
                proveedor.CUIT_CUIL = txtCUITCUIL.Text;
                proveedor.Telefono = txtTelefono.Text;
                proveedor.Mail = txtMail.Text;
                proveedor.Direccion = txtDireccion.Text;
                proveedor.Porcentaje = decimal.Parse(txtPorcentaje.Text);
                proveedor.Estado = true;

                if (ViewState["IdProveedor"] != null)
                {
                    // Modo modificación
                    proveedor.IDProveedor = (int)ViewState["IdProveedor"];

                    if (negocio.ExisteProveedor(proveedor))
                    {
                        lblError.Text = "Ya existe otro proveedor con ese CUIT/CUIL o Mail.";
                        return;
                    }

                    negocio.modificar(proveedor);
                }
                else
                {
                    // Modo alta
                    if (negocio.ExisteProveedor(proveedor))
                    {
                        lblError.Text = "Ya existe un proveedor con ese CUIT/CUIL o Mail.";
                        return;
                    }

                    negocio.agregar(proveedor);
                }

                Response.Redirect("Proveedores.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Proveedores.aspx");
        }
    }
}