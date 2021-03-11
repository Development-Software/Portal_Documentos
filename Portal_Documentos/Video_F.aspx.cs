using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Video_F : System.Web.UI.Page
{
    public string ruta_video;
    //string IDAlumno;
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        string baseUrl = context.Request.Url.Authority + context.Request.ApplicationPath.TrimEnd('/');
        ruta_video = "http://" + baseUrl + "/Images/Portal_Doc.mp4";
        
        try
        {
            if (Request.QueryString["rol"].ToString() == "ula")
            {
                updpnl1.Visible = false;
                
            }else if (Session["CASNetworkID"].ToString() == null)
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch { }
    }


    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {


        if (CheckBox1.Checked)
        {
            checkbox_checked();
        }
        else
        {
            checkbox_no_checked();
        }

    }

    protected void checkbox_checked()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        string strQuery = "";
        strQuery = "UPDATE Alumno SET video='1' WHERE IDAlumno='" + Session["CASNetworkID"].ToString() + "'";
        ConexionSql.Open();
        SqlCommand commandsql = new SqlCommand(strQuery, ConexionSql);
        commandsql.ExecuteNonQuery();
    }
    protected void checkbox_no_checked()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        string strQuery = "";
        strQuery = "UPDATE Alumno SET video='0' WHERE IDAlumno='" + Session["CASNetworkID"].ToString() + "'";
        ConexionSql.Open();
        SqlCommand commandsql = new SqlCommand(strQuery, ConexionSql);
        commandsql.ExecuteNonQuery();
    }
}