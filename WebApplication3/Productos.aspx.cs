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
    public partial class Productos : System.Web.UI.Page
    {
        private List<Producto> listaProductos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductos();
        }

        private void CargarProductos(string filtro = "")
        {           

            ProductoNegocio negocio = new ProductoNegocio();
            listaProductos = negocio.listar();

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
            CargarProductos(txtBuscar.Text.Trim());
        }
    }
}