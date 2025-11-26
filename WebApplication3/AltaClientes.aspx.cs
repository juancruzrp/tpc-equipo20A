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
    public partial class AltaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdCliente.Enabled = false;
            if (!IsPostBack)
            {
                try
                {                 
                    // Si viene un id por query, es modificación
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarClientes(id);
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
           
        }

        protected void btnGuardar_Click(object sender, EventArgs e)


        {
            lblError.Text = "";

           
            if (string.IsNullOrWhiteSpace(txtCUITCUIL.Text) ||
                string.IsNullOrWhiteSpace(txtNombreCliente.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoCliente.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtMail.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                lblError.Text = "Todos los campos son obligatorios.";
                return;
            }

            if (txtCUITCUIL.Text.Length != 11 || !txtCUITCUIL.Text.All(char.IsDigit))
            {
                lblError.Text = "El CUIT/CUIL debe tener exactamente 11 dígitos numéricos.";
                return;
            }

            if (txtNombreCliente.Text.Length < 2 || !txtNombreCliente.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                lblError.Text = "El nombre debe tener al menos 2 letras y no puede contener números ni símbolos.";
                return;
            }

            
            if (txtApellidoCliente.Text.Length < 2 || !txtApellidoCliente.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                lblError.Text = "El apellido debe tener al menos 2 letras y no puede contener números ni símbolos.";
                return;
            }


            
            if (txtTelefono.Text.Length < 8 || txtTelefono.Text.Length > 15 || !txtTelefono.Text.All(char.IsDigit))
            {
                lblError.Text = "El teléfono debe tener entre 8 y 15 dígitos numéricos.";
                return;
            }

           
            if (!txtMail.Text.Contains("@") || !txtMail.Text.Contains("."))
            {
                lblError.Text = "El correo electrónico no tiene un formato válido.";
                return;
            }

            if (txtDireccion.Text.Length < 5)
            {
                lblError.Text = "La dirección debe tener al menos 5 caracteres.";
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtDireccion.Text, @"^[a-zA-Z0-9\s\.,°-]+$"))
            {
                lblError.Text = "La dirección contiene caracteres inválidos. Solo se permiten letras, números, espacios y .,°-";
                return;
            }


            Cliente nuevo = new Cliente();
            ClientesNegocio negocio = new ClientesNegocio();

            nuevo.CUIT_CUIL = txtCUITCUIL.Text;
            nuevo.Nombre = txtNombreCliente.Text;
            nuevo.Apellido = txtApellidoCliente.Text;
            nuevo.Telefono = txtTelefono.Text;
            nuevo.Mail = txtMail.Text;
            nuevo.Direccion = txtDireccion.Text;
            try
            {
                
                if (ViewState["IdCliente"] != null)
                {
                    nuevo.IDCliente = (int)ViewState["IdCliente"];
                    negocio.modificar(nuevo);
                }
                else
                {
                    if (negocio.ExisteCliente(nuevo))
                    {
                        lblError.Text = "Ya existe un cliente con ese CUIT/CUIL.";
                        return;
                    }
                    negocio.agregar(nuevo);
                }
                Response.Redirect("Clientes.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
        private void CargarClientes(int id)
        {
            ClientesNegocio negocio = new ClientesNegocio();
            Cliente cliente = negocio.buscarPorId(id);
            if (cliente != null)
            {
                txtIdCliente.Text = cliente.IDCliente.ToString();
                txtCUITCUIL.Text = cliente.CUIT_CUIL;
                txtNombreCliente.Text = cliente.Nombre;
                txtApellidoCliente.Text = cliente.Apellido;
                txtTelefono.Text = cliente.Telefono;
                txtMail.Text = cliente.Mail;
                txtDireccion.Text = cliente.Direccion;

                ViewState["IdCliente"] = id;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Clientes.aspx", false);
        }
    }
}