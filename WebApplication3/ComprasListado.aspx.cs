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
    public partial class ComprasListado : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras(List<Compra> listaFiltrada = null)
        {
            CompraNegocio negocio = new CompraNegocio();
            try
            {
                if (listaFiltrada == null)
                {
                    listaFiltrada = negocio.Listar();
                }
                listaFiltrada = listaFiltrada.OrderByDescending(x => x.IDCompra).ToList();

                repCompras.DataSource = listaFiltrada;
                repCompras.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al cargar las compras: " + ex.Message + "');</script>");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CompraNegocio negocio = new CompraNegocio();
            try
            {
                List<Compra> lista = negocio.Listar();

                if (!string.IsNullOrEmpty(txtFiltroProveedor.Text))
                {
                    lista = lista.FindAll(x => x.Proveedor.Nombre.ToUpper().Contains(txtFiltroProveedor.Text.ToUpper()));
                }

                if (!string.IsNullOrEmpty(txtFiltroFecha.Text))
                {
                    string inputFecha = txtFiltroFecha.Text.Trim();

                    if (inputFecha.Contains("/"))
                    {
                        string[] partes = inputFecha.Split('/');

                        if (partes.Length == 2)
                        {
                            int mes, anio;
                            if (int.TryParse(partes[0], out mes) && int.TryParse(partes[1], out anio))
                            {
                                lista = lista.FindAll(x => x.Fecha.Month == mes && x.Fecha.Year == anio);
                            }
                            else
                            {
                                Response.Write("<script>alert('El formato de fecha debe ser numérico (ej: 09/2022).');</script>");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Use el formato Mes/Año (ej: 09/2022).');</script>");
                    }
                }

                CargarCompras(lista);

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al filtrar: " + ex.Message + "');</script>");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroProveedor.Text = "";
            txtFiltroFecha.Text = "";
            CargarCompras();
        }

    }
}