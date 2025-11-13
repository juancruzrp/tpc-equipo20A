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
    public partial class Productos : System.Web.UI.Page
    {
        private List<Producto> listaProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                cargarProductos();
            if (Session["Usuario"] == null)
            {
                Session.Add("Error", "Debe iniciar sesión para acceder al sistema.");
                Response.Redirect("~/Error.aspx", false);
            }
        }

        public void cargarProductos(string filtro = "")
        {           

            ProductoNegocio negocio = new ProductoNegocio();
            listaProductos = negocio.listar();

            if (!SesionHelper.EsUsuarioAdmin(Session))
            {
                listaProductos = listaProductos.Where(p => p.Estado).ToList();
            }

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                listaProductos = listaProductos.Where(p =>
                    p.Nombre.ToLower().Contains(filtro) ||
                    p.Marca.Nombre.ToLower().Contains(filtro)
                ).ToList();
            }

            repProductos.DataSource = listaProductos;
            repProductos.DataBind();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            cargarProductos(txtBuscar.Text.Trim());
        }

        protected void repProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Modificar")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("ProductosAgregar.aspx?id=" + idProducto);
            }
            else if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                ProductoNegocio negocio = new ProductoNegocio();
                negocio.CambiarEstado(id);
                Response.Redirect("Productos.aspx", false);

            }
        }

        
    }
}