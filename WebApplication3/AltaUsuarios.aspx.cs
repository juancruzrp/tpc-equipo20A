using Acceso;
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
    public partial class AltaUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdUsuario.Enabled = false;

            if (!IsPostBack)
            {
                try
                {
                    CargarDropDown();

                    // 🔹 Si viene un id por query, es modificación
                    if (Request.QueryString["id"] != null)
                    {
                        int id = int.Parse(Request.QueryString["id"]);
                        CargarUsuario(id);
                    }
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("Error.aspx");
                }
            }
        }

        private void CargarDropDown()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            ddlTipoUsuario.DataSource = usuarioNegocio.listarTipo();
            ddlTipoUsuario.DataTextField = "Descripcion";
            ddlTipoUsuario.DataValueField = "IDTipoUsuario";
            ddlTipoUsuario.DataBind();
            ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccione un tipo de usuario --", "0"));
        }

        private void CargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.buscarPorId(id);

            if (usuario != null)
            {
                txtIdUsuario.Text = usuario.IDUsuario.ToString();
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtContraseña.Text = usuario.Contraseña;
                ddlTipoUsuario.SelectedValue = usuario.TipoUsuario.IDTipoUsuario.ToString();
                CheckEstado.Checked = usuario.Estado;

                ViewState["IdUsuario"] = id;
            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFecha.Text))
            {
                lblError.Text = "Debe ingresar una fecha de alta.";
                return;
            }

            DateTime fechaAlta;
            if (!DateTime.TryParse(txtFecha.Text, out fechaAlta))
            {
                lblError.Text = "La fecha de alta no es válida.";
                return;
            }

            if (fechaAlta.Date < DateTime.Now.Date)
            {
                lblError.Text = "La fecha de alta no puede ser anterior a la actual.";
                return;
            }

            if (ddlTipoUsuario.SelectedValue == "0")
            {
                lblError.Text = "Debe seleccionar un tipo de usuario válido.";
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

                usuario.NombreUsuario = txtNombreUsuario.Text;
                usuario.Contraseña = txtContraseña.Text;
                usuario.TipoUsuario = new TipoUsuario();
                usuario.TipoUsuario.IDTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                usuario.Estado = CheckEstado.Checked;
                usuario.FechaAlta = DateTime.Now;

                // 🔹 Si existe el ID en ViewState, es una modificación
                if (ViewState["IdUsuario"] != null)
                {
                    usuario.IDUsuario = (int)ViewState["IdUsuario"];
                    negocio.modificar(usuario);
                }
                else
                {
                    // 🔹 Si no, es un alta nueva
                    negocio.agregar(usuario);
                }

                Response.Redirect("Usuarios.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }


    }
}