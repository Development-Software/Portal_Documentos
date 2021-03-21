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

public partial class Privacidad : System.Web.UI.Page
{
    applyWeb.Data.Data objAlumno = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
    public string Aviso = @"<embed src=\"" http://www3.ula.edu.mx/UAT/Repositorio_UAT/Content/Otros/Aviso_Privacidad.pdf#toolbar=0"" width=""100%"" height=""500px"">";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
       
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        //if (Session["Rol"] == null || !Session["Rol"].ToString().Equals("Alumno"))
        if (Session["Rol"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        if (valida_alumno())
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "terms()", true);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "valida_alumno()", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "valida_alumno();", true);
        }
        


        //Menu mnuPrincupal = (Menu)Master.FindControl("NavigationMenu");
        //mnuPrincupal.Enabled = false;
        //Response.Redirect("AdministraAlumno.aspx");
    }

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
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
    protected bool valida_alumno()
    {
        string strQuery = "SELECT DISTINCT Count(*)Indicador FROM Alumno WHERE IDAlumno='" + Session["idUser"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = GetData(cmd);
        if (dt.Rows[0]["Indicador"].ToString() == "0")
            return false;
        else
            return true;


    }
}