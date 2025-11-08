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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }

            AplicarResaltadoFilaSeleccionada();
        }

        private void CargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.listar();
            dgvUsuarios.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaUsuarios.aspx");
        }


        protected void dgvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvUsuarios, "RowClick$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer;";

                // visuales de hover
                e.Row.Attributes["onmouseover"] = "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#e0e0e0';";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=this.originalstyle;";
            }
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnModificar.Enabled = true;
            // aplicamos el resaltado
            AplicarResaltadoFilaSeleccionada();
        }

     
        private void AplicarResaltadoFilaSeleccionada()
        {
            if (dgvUsuarios.SelectedIndex >= 0 && dgvUsuarios.Rows.Count > 0)
            {
                foreach (GridViewRow row in dgvUsuarios.Rows)
                {
                    if (row.RowIndex == dgvUsuarios.SelectedIndex)
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
                foreach (GridViewRow row in dgvUsuarios.Rows)
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
            if (source == dgvUsuarios)
            {
                if (eventArgument.StartsWith("RowClick$"))
                {
                    int rowIndex = int.Parse(eventArgument.Replace("RowClick$", ""));

                    dgvUsuarios.SelectedIndex = rowIndex; 

                    // Dispara manualmente el evento SelectedIndexChanged del GridView
                    // habilita el botón y llama a AplicarResaltadoFilaSeleccionada()
                    dgvUsuarios_SelectedIndexChanged(dgvUsuarios, EventArgs.Empty);

                    return; 
                }
            }
            base.RaisePostBackEvent(source, eventArgument);
        }


       


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedIndex >= 0)
            {
                string idUsuario = dgvUsuarios.SelectedDataKey.Value.ToString();
                Response.Redirect("AltaUsuarios.aspx?id=" + idUsuario);
            }
        }


    }
}