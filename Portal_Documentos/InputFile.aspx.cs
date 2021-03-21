using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class InputFile : System.Web.UI.Page
{
    public string formato;
    public string tamano_min;
    public string tamano_max;
    public string IDTipoDocumento;
    public string IDDocumento;
    public string IDAlumno;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        if (Session["Rol"] == null)
        {
            Response.Redirect("Default.aspx");
        }

        IDTipoDocumento = Convert.ToString(Request.QueryString["IDTipoDocumento"]);
        IDDocumento= Convert.ToString(Request.QueryString["IDDocumento"]);
        IDAlumno= Convert.ToString(Request.QueryString["IDAlumno"]);

        if (IDTipoDocumento == null||IDDocumento==null||IDAlumno==null)
        {
            Response.Redirect("Inicio.aspx");
        }
        else {
            try
            {
                SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
                string strQuery = "SELECT LOWER('\"'+REPLACE(Formato,',','\",\"')+'\"')Formato,TamanoMinimo,TamanoMaximo FROM TipoDocumento WHERE IDTipoDocumento='" + IDTipoDocumento + "'";
                ConexionSql.Open();
                SqlDataAdapter sqladapter = new SqlDataAdapter();
                DataSet dssql = new DataSet();
                SqlCommand commandsql = new SqlCommand(strQuery, ConexionSql);
                sqladapter.SelectCommand = commandsql;
                sqladapter.Fill(dssql);
                sqladapter.Dispose();
                commandsql.Dispose();
                ConexionSql.Close();
                formato = dssql.Tables[0].Rows[0][0].ToString();
                tamano_min = dssql.Tables[0].Rows[0][1].ToString();
                tamano_max = dssql.Tables[0].Rows[0][2].ToString();
            }catch(Exception ex)
            {
                DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
                StreamWriter sw = new StreamWriter(virtualDirPath + "error_input_file"+DateTime.Now+".txt", true);
                sw.WriteLine(IDAlumno);
                sw.WriteLine(IDTipoDocumento);
                sw.WriteLine(IDDocumento);
                sw.WriteLine(ex.ToString());
                sw.Close();
            }
        }
    }

    
}