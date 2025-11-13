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


    }
}