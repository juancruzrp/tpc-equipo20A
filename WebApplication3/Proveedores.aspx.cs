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
            List<Proveedor> lista = negocio.listar();

            // Si el usuario NO es administrador, mostrar solo proveedores activos
            if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                lista = lista.Where(p => p.Estado).ToList();
            }

            dgvProveedores.DataSource = lista;
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

            int idProveedor = Convert.ToInt32(dgvProveedores.SelectedDataKey.Value);


            ProveedoresNegocio negocio = new ProveedoresNegocio();
            List<Proveedor> listaProveedores = negocio.listar();

            // Buscar el proveedor
            Proveedor seleccionado = listaProveedores.Find(p => p.IDProveedor == idProveedor);

            if (seleccionado != null)
            {
                btnInactivar.Text = seleccionado.Estado ? "Inactivar Proveedor" : "Activar Proveedor";
                btnInactivar.CssClass = seleccionado.Estado ? "btn btn-outline-danger" : "btn btn-outline-success";
                btnInactivar.OnClientClick = seleccionado.Estado
            ? "return confirm('¿Estás seguro de que quieres inactivar el proveedor seleccionado?');"
            : "return confirm('¿Estás seguro de que quieres activar el proveedor seleccionado?');";
            }
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

                // Obtener el proveedor actual
                List<Proveedor> lista = negocio.listar();
                Proveedor seleccionado = lista.Find(p => p.IDProveedor == idProveedor);

                if (seleccionado != null)
                {
                    bool estadoAnterior = seleccionado.Estado;

                    // Invertir estado
                    seleccionado.Estado = !estadoAnterior;

                    // Aplicar cambio
                    negocio.CambiarEstado(seleccionado);

                    // Refrescar grilla
                    CargarGrilla();

                    // Mostrar mensaje dinámico
                    string mensaje = estadoAnterior ? "Proveedor inactivado exitosamente." : "Proveedor activado exitosamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{mensaje}');", true);

                    // Resetear botones
                    btnModificar.Enabled = false;
                    btnInactivar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado del proveedor: " + ex.Message, ex);
            }


        }
    }

}