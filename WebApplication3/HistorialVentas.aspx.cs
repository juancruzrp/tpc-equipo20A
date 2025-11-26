using Dominio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Negocio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication3
{
    public partial class HistorialVentas : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFiltros();
                cargarVentas();
            }
        }

        private void cargarVentas()
        {
            VentaNegocio negocio = new VentaNegocio();

            List<Venta> lista = negocio.ListarVentasConDetalles();

            // filtro fecha
            if (!string.IsNullOrWhiteSpace(txtBuscarFecha.Text))
            {
                if (DateTime.TryParse(txtBuscarFecha.Text, out DateTime fechaBuscada))
                {
                    lista = lista.Where(v => v.Fecha.Date == fechaBuscada.Date).ToList();
                }
            }

            // filtro cliente
            if (ddlClientes.SelectedIndex > 0)
            {
                int idCliente = int.Parse(ddlClientes.SelectedValue);
                //lista = lista.Where(v => v.Cliente.IDCliente == idCliente).ToList();
                lista = lista.Where(v => v.Cliente != null && v.Cliente.IDCliente == idCliente).ToList();
            }

            // filtro usuario
            if (ddlUsuarios.SelectedIndex > 0)
            {
                int idUsuario = int.Parse(ddlUsuarios.SelectedValue);
                //lista = lista.Where(v => v.Usuario.IDUsuario == idUsuario).ToList();
                lista = lista.Where(v => v.Usuario != null && v.Usuario.IDUsuario == idUsuario).ToList();
            }

            repVentas.DataSource = lista;
            repVentas.DataBind();

        }

        private void cargarFiltros()
        {
            ClientesNegocio cliNeg = new ClientesNegocio();
            UsuarioNegocio usuNeg = new UsuarioNegocio();

            ddlClientes.DataSource = cliNeg.listar();
            ddlClientes.DataValueField = "IDCliente";
            ddlClientes.DataTextField = "NombreCompleto";  
            ddlClientes.DataBind();
            ddlClientes.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos los clientes", "0"));

            ddlUsuarios.DataSource = usuNeg.listar();
            ddlUsuarios.DataValueField = "IDUsuario";
            ddlUsuarios.DataTextField = "NombreUsuario";
            ddlUsuarios.DataBind();
            ddlUsuarios.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Todos los usuarios", "0"));
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            cargarVentas();
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarVentas();
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarVentas();
        }



    }
}