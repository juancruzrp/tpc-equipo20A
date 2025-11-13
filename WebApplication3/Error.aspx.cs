using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication3.Helpers;

namespace WebApplication3
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Error"] != null)
            {
                lblMensaje.Text = Session["Error"].ToString();
            }
        }

        protected void btnVolverInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inicio.aspx", false);
        }

        protected void btnIrLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", false);
        }
    }
}