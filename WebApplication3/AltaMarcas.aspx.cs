using Acceso;
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
    public partial class AltaMarcas : System.Web.UI.Page
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

            if (Request.QueryString["id"] != null)
            {
                int idMarca = int.Parse(Request.QueryString["id"]);
                CargarMarca(idMarca);
                ViewState["IdMarca"] = idMarca;
            }
        }

        private void CargarMarca(int idMarca)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = negocio.obtenerPorId(idMarca);

            if (marca != null)
            {
                txtIdMarca.Text = marca.IDMarca.ToString();
                txtNombreMarca.Text = marca.Nombre;
            }
        }

        protected void btnGuardarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();

                marca.Nombre = txtNombreMarca.Text;

                if (ViewState["IdMarca"] != null)
                {
                    marca.IDMarca = (int)ViewState["IdMarca"];
                    negocio.modificar(marca);
                }
                else
                {

                    negocio.agregar(marca);
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