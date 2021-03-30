using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Faqs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (Page.IsPostBack)
        {

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
                Response.End();
            }
            else
            {


                Session.Clear();
                String networkId = CASP.Authenticate("https://www.ula.edu.mx/ncas/login",
                    "https://www.ula.edu.mx/ncas/validate", this);
                Session["CASNetworkID"] = networkId;


                if (Request.QueryString["CASNetworkID"] == null)
                {
                    if (this.Session["CASNetworkID"] != null &&
                        !(this.Session["CASNetworkID"].ToString().Equals("")))
                    {

                    }
                }

            }
            
        }
        Session["Reset"] = true;
        Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
        SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
        int timeout = ((int)section.Timeout.TotalMinutes - 3) * 1000 * 60;
        ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
    }
    
}