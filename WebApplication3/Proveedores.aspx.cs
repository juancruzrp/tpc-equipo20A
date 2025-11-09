using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();
        }

        private void CargarGrilla()
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            dgvProveedores.DataSource = negocio.listar();
            dgvProveedores.DataBind();
        }

        protected void dgvProveedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvProveedores, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        protected void dgvProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvProveedores.Rows)
                row.CssClass = row.RowIndex == dgvProveedores.SelectedIndex ? "selectedRowHighlight" : "";

            btnModificar.Enabled = true;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaProveedores.aspx");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvProveedores.SelectedDataKey.Value);
            Response.Redirect("AltaProveedores.aspx?id=" + id);
        }

        // 🔒 Método que registra los postbacks válidos (soluciona el error)
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in dgvProveedores.Rows)
            {
                Page.ClientScript.RegisterForEventValidation(dgvProveedores.UniqueID, "Select$" + row.RowIndex);
            }
            base.Render(writer);
        }
    }
    
}