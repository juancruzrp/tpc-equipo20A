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
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
            if(Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
            else if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                Session.Add("Error", "No tienes permisos para acceder a esta sección.");
                Response.Redirect("Error.aspx");
            }

            //AplicarResaltadoFilaSeleccionada();
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
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvUsuarios, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvUsuarios.Rows)
                row.CssClass = row.RowIndex == dgvUsuarios.SelectedIndex ? "selectedRowHighlight" : "";
            
            btnModificar.Enabled = true;
            btnInactivar.Enabled = true;

            int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedDataKey.Value);


            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> listaUsuarios = negocio.listar();

            // Buscar el Usuario
            Usuario seleccionado = listaUsuarios.Find(u => u.IDUsuario == idUsuario);

            if (seleccionado != null)
            {
                btnInactivar.Text = seleccionado.Estado ? "Inactivar Usuario" : "Activar Usuario";
                btnInactivar.CssClass = seleccionado.Estado ? "btn btn-outline-danger" : "btn btn-outline-success";
                btnInactivar.OnClientClick = seleccionado.Estado
            ? "return confirm('¿Estás seguro de que quieres inactivar el usuario seleccionado?');"
            : "return confirm('¿Estás seguro de que quieres activar el usuario seleccionado?');";
            }
        }


        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedIndex >= 0)
            {
                string idUsuario = dgvUsuarios.SelectedDataKey.Value.ToString();
                Response.Redirect("AltaUsuarios.aspx?id=" + idUsuario);
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            try
            {
                int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedDataKey.Value);

                // Obtener el usuario actual
                List<Usuario> lista = negocio.listar();
                Usuario seleccionado = lista.Find(p => p.IDUsuario == idUsuario);

                if (seleccionado != null)
                {
                    bool estadoAnterior = seleccionado.Estado;

                    // Invertir estado en caso de estar activo o inactivo
                    seleccionado.Estado = !estadoAnterior;
                    if (!seleccionado.Estado)
                    {
                        // Si lo inactivo, seteo la fecha de baja
                        seleccionado.FechaBaja = DateTime.Now;
                    }
                    else
                    {
                        // Si lo activo de nuevo, limpio la fecha de baja
                        seleccionado.FechaBaja = null;
                    }

                    // Aplicar cambio de estado
                    negocio.CambiarEstado(seleccionado);

                    // Recargar grilla de proveedores
                    CargarUsuarios();

                    // Mensaje que cambia segun el estado
                    string mensaje = estadoAnterior ? "Usuario inactivado exitosamente." : "Usuario activado exitosamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{mensaje}');", true);

                    // Resetear botones
                    btnModificar.Enabled = false;
                    btnInactivar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado del usuario: " + ex.Message, ex);
            }
        }
    }
}