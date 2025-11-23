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
    public partial class MarcasYCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMarcas();
                CargarCategorias();
            }
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }

        }
        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listar();

            if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                lista = lista.Where(m => m.Estado).ToList();
            }

            dgvMarcas.DataSource = lista;
            dgvMarcas.DataBind();
        }

        protected void dgvMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvMarcas, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvMarcas.Rows)
                row.CssClass = row.RowIndex == dgvMarcas.SelectedIndex ? "selectedRowHighlight" : "";

            btnModificar.Enabled = true;
            btnInactivar.Enabled = true;
            int IDMarca = Convert.ToInt32(dgvMarcas.SelectedDataKey.Value);


            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lista = negocio.listar();

            // Buscar el proveedor
            Marca seleccionado = lista.Find(m => m.IDMarca == IDMarca);

            if (seleccionado != null)
            {
                btnInactivar.Text = seleccionado.Estado ? "Inactivar Marca" : "Activar Marca";
                btnInactivar.CssClass = seleccionado.Estado ? "btn btn-outline-danger" : "btn btn-outline-success";
                btnInactivar.OnClientClick = seleccionado.Estado
            ? "return confirm('¿Estás seguro de que quieres inactivar la marca seleccionada?');"
            : "return confirm('¿Estás seguro de que quieres activar la marca seleccionada?');";
            }
        }
        
        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                int idMarca = Convert.ToInt32(dgvMarcas.SelectedDataKey.Value);

                List<Marca> lista = negocio.listar();
                Marca seleccionado = lista.Find(m => m.IDMarca == idMarca);

                if (seleccionado != null)
                {
                    bool estadoAnterior = seleccionado.Estado;
                    seleccionado.Estado = !estadoAnterior;
                    CargarMarcas();
                    string mensaje = estadoAnterior ? "Marca inactivada exitosamente." : "Marca activada exitosamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{mensaje}');", true);

                    btnModificar.Enabled = false;
                    btnInactivar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado del proveedor: " + ex.Message, ex);
            }
        }
        

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            if (dgvMarcas.SelectedIndex >= 0)
            {
                int idMarca = Convert.ToInt32(dgvMarcas.DataKeys[dgvMarcas.SelectedIndex].Value);
                Response.Redirect("AltaMarcas.aspx?id=" + idMarca);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaMarcas.aspx");
        }
        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listar();

            if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                lista = lista.Where(m => m.Estado).ToList();
            }

            dgvCategorias.DataSource = lista;
            dgvCategorias.DataBind();
        }

        protected void dgvCategorias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvCategorias, "Select$" + e.Row.RowIndex);
                e.Row.Style["cursor"] = "pointer";
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in dgvCategorias.Rows)
                row.CssClass = row.RowIndex == dgvCategorias.SelectedIndex ? "selectedRowHighlight" : "";

            btnModificarCat.Enabled = true;
            btnEliminarCat.Enabled = true;

            int IDCategoria = Convert.ToInt32(dgvCategorias.SelectedDataKey.Value);

            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lista = negocio.listar();

            Categoria seleccionado = lista.Find(p => p.IDCategoria == IDCategoria);

            if (seleccionado != null)
            {
                btnEliminarCat.Text = seleccionado.Estado ? "Inactivar Categoria" : "Activar Categoria";
                btnEliminarCat.CssClass = seleccionado.Estado ? "btn btn-outline-danger" : "btn btn-outline-success";
                btnEliminarCat.OnClientClick = seleccionado.Estado
                    ? "return confirm('¿Estás seguro de que quieres inactivar la categoria seleccionada?');"
                    : "return confirm('¿Estás seguro de que quieres activar la categoria seleccionada?');";
            }
        }

        protected void btnEliminarCat_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                int idCategoria = Convert.ToInt32(dgvCategorias.SelectedDataKey.Value);

                List<Categoria> lista = negocio.listar();
                Categoria seleccionado = lista.Find(p => p.IDCategoria == idCategoria);

                if (seleccionado != null)
                {
                    bool estadoAnterior = seleccionado.Estado;
                    seleccionado.Estado = !estadoAnterior;
                    negocio.CambiarEstado(seleccionado);

                    CargarCategorias();

                    string mensaje = estadoAnterior ? "Categoria inactivada exitosamente." : "Categoria activada exitosamente.";
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"alert('{mensaje}');", true);

                    btnModificar.Enabled = false;
                    btnInactivar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar estado de la categoria: " + ex.Message, ex);
            }


        }


        protected void btnModificarCat_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedIndex >= 0)
            {
                string idCategoria = dgvCategorias.SelectedDataKey.Value.ToString();
                Response.Redirect("AltaCategorias.aspx?id=" + idCategoria);
            }
        }

        protected void btnAgregarCat_Click(object sender, EventArgs e)
        {
            Response.Redirect("AltaCategorias.aspx");
        }
    }
}