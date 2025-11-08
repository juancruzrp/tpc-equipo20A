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
    public partial class Ventas : System.Web.UI.Page
    {
        private List<Producto> productos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                Session["DetalleVenta"] = new List<(Producto, int)>();
            }
        }

        private void CargarProductos(string filtro = "")
        {
            ProductoNegocio negocio = new ProductoNegocio();
            productos = negocio.listar();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();

                productos = productos.Where(p =>
                    (p.Nombre != null && p.Nombre.ToLower().Contains(filtro)) ||
                    (p.Marca != null && p.Marca.Nombre != null && p.Marca.Nombre.ToLower().Contains(filtro)) ||
                    p.IDProducto.ToString().Contains(filtro)
                ).ToList();
            }

            dgvProductos.DataSource = productos;
            dgvProductos.DataBind();
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarProductos(txtBuscar.Text);
        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AgregarProducto")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = ((Button)e.CommandSource).NamingContainer as GridViewRow;
                TextBox txtCantidad = (TextBox)row.FindControl("txtCantidad");

                int cantidad = 1;
                int.TryParse(txtCantidad.Text, out cantidad);

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.listar().FirstOrDefault(p => p.IDProducto == idProducto);

                if (producto != null)
                {
                    List<(Producto, int)> detalleVenta = Session["DetalleVenta"] as List<(Producto, int)> ?? new List<(Producto, int)>();

                    var existente = detalleVenta.FirstOrDefault(d => d.Item1.IDProducto == idProducto);
                    if (existente.Item1 != null)
                    {
                        detalleVenta.Remove(existente);
                        detalleVenta.Add((existente.Item1, existente.Item2 + cantidad));
                    }
                    else
                    {
                        detalleVenta.Add((producto, cantidad));
                    }

                    Session["DetalleVenta"] = detalleVenta;
                    ActualizarDetalleVenta();
                }
            }
        }

        protected void dgvDetalleVenta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "QuitarProducto")
            {
                int idProducto = Convert.ToInt32(e.CommandArgument);
                var detalleVenta = Session["DetalleVenta"] as List<(Producto, int)>;
                detalleVenta.RemoveAll(d => d.Item1.IDProducto == idProducto);
                Session["DetalleVenta"] = detalleVenta;
                ActualizarDetalleVenta();
            }
        }

        private void ActualizarDetalleVenta()
        {
            var detalleVenta = Session["DetalleVenta"] as List<(Producto, int)>;
            if (detalleVenta == null || detalleVenta.Count == 0)
            {
                dgvDetalleVenta.DataSource = null;
                dgvDetalleVenta.DataBind();
                lblTotal.Text = "Total: $0,00";
                return;
            }

            var listaMostrar = detalleVenta.Select(d => new
            {
                IDProducto = d.Item1.IDProducto,
                Nombre = d.Item1.Nombre,
                Precio = d.Item1.Precio,
                Cantidad = d.Item2,
                Subtotal = d.Item1.Precio * d.Item2
            }).ToList();

            dgvDetalleVenta.DataSource = listaMostrar;
            dgvDetalleVenta.DataBind();

            decimal total = listaMostrar.Sum(d => d.Subtotal);
            lblTotal.Text = "Total: " + total.ToString("C");
        }

        protected void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            
        }
    }
}