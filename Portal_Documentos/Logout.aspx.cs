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
            if (HttpContext.Current.User.Identity.Name.Length >= 8)
            {
                Session.Clear();
                Response.Redirect("https://www.ula.edu.mx/ncas/logout");
                //Response.Redirect("https://www.ula-azure.ula.edu.mx/ncas/logout");
            }
            else
            {
                Session.Clear();
                Response.Redirect("Administrativos.aspx");
            }


        }

    }
}