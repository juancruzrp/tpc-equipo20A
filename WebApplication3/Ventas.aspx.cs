using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Ventas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarClientes();
        }

        private void CargarClientes()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> clientes = negocio.listar();

            StringBuilder sb = new StringBuilder();

            foreach (var c in clientes)
            {
                string nombreCompleto = HttpUtility.JavaScriptStringEncode(c.NombreCompleto);
                string cuit = HttpUtility.JavaScriptStringEncode(c.CUIT_CUIL ?? "");
                string id = c.IDCliente.ToString();

                sb.Append($@"
            <a href='#' class='dropdown-item'
               onclick=""seleccionarCliente('{nombreCompleto}', '{cuit}', '{id}')"">
                {nombreCompleto}
            </a>");
            }

            litClientes.Text = sb.ToString();
        }









    }
}