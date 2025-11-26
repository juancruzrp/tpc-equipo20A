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

            if (string.IsNullOrWhiteSpace(txtCUITCUIL.Text))
            {
                lblError.Text = "El CUIT/CUIL es obligatorio.";
                return;
            }

            if (!txtCUITCUIL.Text.All(char.IsDigit))
            {
                lblError.Text = "El CUIT/CUIL debe contener solo números.";
                return;
            }

            if (txtCUITCUIL.Text.Length != 11)
            {
                lblError.Text = "El CUIT/CUIL debe tener 11 dígitos.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblError.Text = "El nombre del proveedor es obligatorio.";
                return;
            }

            string nombre = txtNombre.Text.Trim();
            if (nombre.Length < 2)
            {
                lblError.Text = "El nombre debe tener al menos 2 caracteres.";
                return;
            }

            int cantidadLetras = nombre.Count(char.IsLetter);
            if (cantidadLetras < 2)
            {
                lblError.Text = "El nombre debe contener al menos 2 letras.";
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPorcentaje.Text))
            {
                lblError.Text = "El porcentaje es obligatorio.";
                return;
            }

            if (!decimal.TryParse(txtPorcentaje.Text, out decimal porcentaje))
            {
                lblError.Text = "El porcentaje debe ser un número válido.";
                return;
            }

            if (porcentaje < 0 || porcentaje > 100)
            {
                lblError.Text = "El porcentaje debe estar entre 0 y 100.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                // solo numeros
                if (!txtTelefono.Text.All(char.IsDigit))
                {
                    lblError.Text = "El teléfono debe contener solo números.";
                    return;
                }

                // minimo 7 digitos
                if (txtTelefono.Text.Length < 7)
                {
                    lblError.Text = "El teléfono debe tener al menos 7 dígitos.";
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtMail.Text))
            {
                try
                {
                    var mail = new System.Net.Mail.MailAddress(txtMail.Text);
                    if (mail.Address != txtMail.Text)
                    {
                        lblError.Text = "El mail ingresado no es válido.";
                        return;
                    }
                }
                catch
                {
                    lblError.Text = "El mail ingresado no es válido.";
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                if (txtDireccion.Text.Trim().Length < 5)
                {
                    lblError.Text = "La dirección debe tener al menos 5 caracteres.";
                    return;
                }
            }

            // crear el proveedor
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            Proveedor proveedor = new Proveedor();

            proveedor.Nombre = txtNombre.Text;
            proveedor.CUIT_CUIL = txtCUITCUIL.Text;
            proveedor.Telefono = txtTelefono.Text;
            proveedor.Mail = txtMail.Text;
            proveedor.Direccion = txtDireccion.Text;
            proveedor.Porcentaje = porcentaje;
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