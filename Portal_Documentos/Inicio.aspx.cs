using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        video_tutorial();
        try
        {
            if (Request.QueryString["sesion"].ToString() == "1")
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "video_modal('Video_F.aspx','Inicio.aspx');", true);
                video_tutorial_inicio();
            }
        }
        catch { }
        
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


                        //lblIntro.Text = "Hoy es: " + DateTime.Today.Date.ToString("dd/MM/yyyy");

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

    protected void video_tutorial()
    {
        try
        {
            if (Request.QueryString["video"].ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "video_modal('Video_F.aspx','Inicio.aspx');", true);
            }
            else if(Request.QueryString["video"].ToString() == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "video_modal('Video_F.aspx?rol=ula','Inicio.aspx');", true);
            }
        }
        catch { }
    }
    
    protected void video_tutorial_inicio()
    {
        string strQuery = "SELECT video FROM Alumno WHERE IDAlumno=@IDAlumno";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = Session["CASNetworkID"];
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            if (dt.Rows[0]["video"].ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "video_modal('Video_F.aspx','Inicio.aspx');", true);
            }
        }

    }

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        catch
        {
            return null;
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
    }
}