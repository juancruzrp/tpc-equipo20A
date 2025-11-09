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
            AplicarResaltadoFilaSeleccionada();
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
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvClientes, "RowClick$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer;";

                // visuales de hover
                e.Row.Attributes["onmouseover"] = "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#e0e0e0';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=this.originalstyle;";
            }
        }

        protected void dgvClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            // aplicamos el resaltado
            AplicarResaltadoFilaSeleccionada();
        }


        private void AplicarResaltadoFilaSeleccionada()
        {
            if (dgvClientes.SelectedIndex >= 0 && dgvClientes.Rows.Count > 0)
            {
                foreach (GridViewRow row in dgvClientes.Rows)
                {
                    if (row.RowIndex == dgvClientes.SelectedIndex)
                    {
                        row.CssClass = "selectedRowHighlight"; //  clase CSS 
                    }
                    else
                    {
                        // importante para que solo una fila esté resaltada a la vez
                        if (row.CssClass == "selectedRowHighlight")
                        {
                            row.CssClass = "";
                        }
                    }
                }
            }
            else
            {
                // Si no hay fila seleccionada
                foreach (GridViewRow row in dgvClientes.Rows)
                {
                    if (row.CssClass == "selectedRowHighlight")
                    {
                        row.CssClass = "";
                    }
                }
            }
        }


        // Sobreescribimos el método RaisePostBackEvent para capturar evento "RowClick$"
        protected override void RaisePostBackEvent(IPostBackEventHandler source, string eventArgument)
        {
            if (source == dgvClientes)
            {
                if (eventArgument.StartsWith("RowClick$"))
                {
                    int rowIndex = int.Parse(eventArgument.Replace("RowClick$", ""));

                    dgvClientes.SelectedIndex = rowIndex;

                    // Dispara manualmente el evento SelectedIndexChanged del GridView
                    // habilita el botón y llama a AplicarResaltadoFilaSeleccionada()
                    dgvClientes_SelectedIndexChanged(dgvClientes, EventArgs.Empty);

                    return;
                }
            }
            base.RaisePostBackEvent(source, eventArgument);
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