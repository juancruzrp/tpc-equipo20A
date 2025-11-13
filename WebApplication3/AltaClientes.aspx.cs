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
                 
                    // 🔹 Si viene un id por query, es modificación
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
                // 🔹 Si existe el ID en ViewState, es una modificación
                if (ViewState["IdCliente"] != null)
                {
                    nuevo.IDCliente = (int)ViewState["IdCliente"];
                    negocio.modificar(nuevo);
                }
                else
                {
                    // 🔹 Si no, es un alta nueva
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
    }
}