using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Site : System.Web.UI.MasterPage
{
    public string baseUrl;
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpContext context = HttpContext.Current;
        baseUrl = context.Request.Url.AbsolutePath;
        string text = baseUrl.Substring(baseUrl.IndexOf("/", 1) + 1);
        switch (text)
        {
            case "Inicio.aspx":
                Label1.Text = "Bienvenido al Sistema de Entrega de Documentación Electrónica";
                break;
            case "Tipodocumentos.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Catálogo de Documentos";
                break;
            case "Usuarios.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Mantenimiento de Usuarios";
                break;
            case "Permisos.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Configuración de Permisos";
                break;
            case "ListadoAdministracion.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Administración de Alumnos";
                break;
            case "ListadoExpediente.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Administración de Expediente";
                break;
            case "Reporte_General.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Reporte General";
                break;
            case "Faqs.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Preguntas Frecuentes";
                break;
            case "CargaDocumentos.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Carga de Documentos";
                break;
            case "Privacidad.aspx":
                Label1.Text = "Sistema de Entrega de Documentación Electrónica<br/>Politica de Privacidad";
                page_all.Visible = false;
                break;
        }

        if (Session["user"] != null)
        {
            SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
            string strQuery_valida = "SELECT Count(*) FROM Usuario WHERE Login='" + Session["user"].ToString() + "'";
            ConexionSql.Open();
            SqlDataAdapter sqladapter = new SqlDataAdapter();
            DataSet dssql = new DataSet();
            SqlCommand commandsql = new SqlCommand(strQuery_valida, ConexionSql);
            sqladapter.SelectCommand = commandsql;
            sqladapter.Fill(dssql);
            sqladapter.Dispose();
            commandsql.Dispose();

            if (dssql.Tables[0].Rows[0][0].ToString().Trim() != "0")
            {
                string strQueryuser = "SELECT Nombre FROM Usuario WHERE Login='" + Session["user"].ToString() + "'";


                DataSet dssql1 = new DataSet();
                SqlCommand commandsql1 = new SqlCommand(strQueryuser, ConexionSql);
                sqladapter.SelectCommand = commandsql1;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                commandsql1.Dispose();
                // LoginName lblLogin = (LoginName)HeadLoginView.FindControl("HeadLoginName");
                // if (lblLogin != null)
                // lblLogin.FormatString = dssql1.Tables[0].Rows[0][0].ToString().Trim() + " </br>Perfil: " + Session["Rol"].ToString() + "</br>";
                lblUsuario.Text = dssql1.Tables[0].Rows[0][0].ToString().Trim();
                lblRol.Text = "Perfil: " + Session["Rol"].ToString();
            }
            else
            {
                // LoginName lblLogin = (LoginName)HeadLoginView.FindControl("HeadLoginName");
                // if (lblLogin != null)
                // lblLogin.FormatString = Session["user"].ToString();
                lblUsuario.Text = Session["user"].ToString();
                lblUsuario_Alumno.Text= Session["user"].ToString();
            }
            ConexionSql.Close();

        }
        else { lblUsuario.Visible = false; lblRol.Visible = false; }

        if (Session["Rol"] != null)
        {
            if (Session["Rol"].ToString().Equals("Alumno"))
            {
                Administrativo.Visible = false;
                Alumno.Visible = true;
                Inicio.Visible = true;
                Carga_Documentos.Visible = true;
                Configuracion1.Visible = false;
                Configuracion2.Visible = false;
                Tipos_Documentos.Visible = false;
                Usuarios.Visible = false;
                Permisos.Visible = false;
                Administracion.Visible = false;
                Reportes.Visible = false;
                FQS.Visible = true;
                Logout.Visible = true;
                sesion_admin.Visible = false;
                video2.Visible = false;
            }
            else
            {
                Administrativo.Visible = true;
                Alumno.Visible = false;
                Carga_Documentos.Visible = false;
                Logout.Visible = true;
                sesion_alumno.Visible = false;
                Contacto.Visible = false;
                video1.Visible = false;
                SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                ConexionSql.Open();
                string strQuery = "SELECT DISTINCT IDPrivilegio FROM Permisos_App " +
                                         "WHERE IDPrivilegio NOT IN(SELECT A.IDPrivilegio FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio= B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDPermiso=1 AND C.Nombre='" + Session["Rol"].ToString() + "') " +
                                         "AND IDPermiso = 1";
                SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int IDprivilegio = dr.GetInt32(0);

                    if (IDprivilegio == 1) { Inicio.Visible = false; }
                    else if (IDprivilegio == 2) { Configuracion1.Visible = false; Configuracion2.Visible = false; }
                    else if (IDprivilegio == 3) { Tipos_Documentos.Visible = false; }
                    else if (IDprivilegio == 7) { Usuarios.Visible = false; }
                    else if (IDprivilegio == 11) { Permisos.Visible = false; }
                    else if (IDprivilegio == 16) { Administracion.Visible = false; }
                    else if (IDprivilegio == 21) { Reportes.Visible = false; }
                    else if (IDprivilegio == 22) { FQS.Visible = false; }


                }
                ConexionSql.Close();
            }
            }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

}
