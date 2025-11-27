using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
            }
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
        }

        private void CargarClientes()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            dgvClientes.DataSource = negocio.listar();
            dgvClientes.DataBind();

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaClientes.aspx");
        }
        protected void dgvClientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvClientes, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            foreach (GridViewRow row in dgvClientes.Rows)
                row.CssClass = row.RowIndex == dgvClientes.SelectedIndex ? "selectedRowHighlight" : "";

            btnModificar.Enabled = true;
            btnInactivar.Enabled = true; 

            int idCliente = Convert.ToInt32(dgvClientes.SelectedDataKey.Value);

            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> lista = negocio.listar();
            Cliente seleccionado = lista.Find(x => x.IDCliente == idCliente);

            if (seleccionado != null)
            {
                if (seleccionado.Estado)
                {
                    btnInactivar.Text = "Inactivar Cliente";
                    btnInactivar.CssClass = "btn btn-outline-danger";
                    btnInactivar.OnClientClick = "return confirm('¿Estás seguro de que quieres inactivar a este cliente?');";
                }
                else
                {
                    btnInactivar.Text = "Activar Cliente";
                    btnInactivar.CssClass = "btn btn-outline-success";
                    btnInactivar.OnClientClick = "return confirm('¿Estás seguro de que quieres activar a este cliente?');";
                }
            }
        }
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in dgvClientes.Rows)
            {
                Page.ClientScript.RegisterForEventValidation(dgvClientes.UniqueID, "Select$" + row.RowIndex);
            }
            base.Render(writer);
        }




        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedIndex >= 0)
            {
                string idUsuario = dgvClientes.SelectedDataKey.Value.ToString();
                Response.Redirect("AltaClientes.aspx?id=" + idUsuario);
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCliente = Convert.ToInt32(dgvClientes.SelectedDataKey.Value);

                ClientesNegocio negocio = new ClientesNegocio();
                List<Cliente> lista = negocio.listar();
                Cliente seleccionado = lista.Find(x => x.IDCliente == idCliente);

                if (seleccionado.Estado)
                {
                    negocio.eliminarLogico(idCliente);
                }
                else
                {
                    negocio.activar(idCliente);
                }

                CargarClientes();

                btnModificar.Enabled = false;
                btnInactivar.Enabled = false;
                btnInactivar.Text = "Inactivar Cliente"; 
                btnInactivar.CssClass = "btn btn-outline-danger";
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.Message);
                Response.Redirect("Error.aspx");
            }
        }



    }
}