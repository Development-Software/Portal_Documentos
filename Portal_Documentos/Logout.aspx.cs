using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        else
        {
            if (Session["Rol"].ToString().Equals("Alumno"))
            {
                Session.Clear();
                Response.Redirect("Default.aspx");
            }
            else
            {
                Session.Clear();
                Response.Redirect("Administrativos.aspx");
            }


        }

    }
}