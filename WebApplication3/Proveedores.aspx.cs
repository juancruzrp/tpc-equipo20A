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
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
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
            btnInactivar.Enabled = true;
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

       
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in dgvProveedores.Rows)
            {
                Page.ClientScript.RegisterForEventValidation(dgvProveedores.UniqueID, "Select$" + row.RowIndex);
            }
            base.Render(writer);
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            ProveedoresNegocio negocio = new ProveedoresNegocio();
            try
            {
                
                int idProveedor = Convert.ToInt32(dgvProveedores.SelectedDataKey.Value);
                negocio.eliminarLogico(idProveedor); // Llama al método de borrado lógico

               
                CargarGrilla();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Proveedor inactivado exitosamente.');", true);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al inactivar proveedor: " + ex.Message + "');</script>");
            }
        }


    }
    
}