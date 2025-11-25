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
    public partial class AltaCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
            else if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                Session.Add("Error", "No tienes permisos para acceder a esta sección.");
                Response.Redirect("Error.aspx");
            }
            
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int idCategoria))
                {
                    ViewState["IdCategoria"] = idCategoria;

                    txtIdCategoria.Visible = false;

                    CargarCategoria(idCategoria);
                }
                else
                {
                    txtIdCategoria.Visible = false;
                }
            }
        }

        private void CargarCategoria(int idCategoria)
        {            
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = negocio.obtenerPorId(idCategoria);

            if (categoria != null)
            {
                txtIdCategoria.Text = categoria.IDCategoria.ToString();
                txtNombreCategoria.Text = categoria.Nombre;
            }
        }





        protected void btnGuardarCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria categoria = new Categoria();
                CategoriaNegocio negocio = new CategoriaNegocio();

                categoria.Nombre = txtNombreCategoria.Text;

                if (ViewState["IdCategoria"] != null)
                {
                    categoria.IDCategoria = (int)ViewState["IdCategoria"];
                    negocio.modificar(categoria);
                }
                else
                {

                    negocio.agregar(categoria);
                }

                Response.Redirect("MarcasyCategorias.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}