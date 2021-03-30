using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Security.Cryptography;
using System.Text;
//using ulaDocumentos.ulaLogin;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;

public partial class _Default : System.Web.UI.Page
{
    applyWeb.Data.Data objUsuarios = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);


    protected void Page_Load(object sender, EventArgs e)
    {

        txtusername.Attributes.Add("placeholder", "Usuario");
        txtpassword.Attributes.Add("placeholder", "Password");
        txtusername.Attributes.Add("autocomplete", "off");
        txtpassword.Attributes.Add("autocomplete", "off");
    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        //Thread.Sleep(8000);
        if (txtusername.Text != "" && txtpassword.Text != "")
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "", "Entrar();", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "myFuncionAlerta", "Entrar();", true);
            if (autenticacion(txtusername.Text, txtpassword.Text))
            {

                Session["idUser"] = txtusername.Text;
                Session["Rol"] = "Alumno";
                FormsAuthentication.Initialize();
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                txtusername.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, Session["Rol"].ToString(), FormsAuthentication.FormsCookiePath);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

                Response.Redirect("Inicio.aspx?sesion=1");
            }
        }
    }

    private bool autenticacion(string username_text, string password_text)
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        MySqlCommand cmd = new MySqlCommand("Login_Alumno", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();

        cmd.Parameters.AddWithValue("@username", username_text);
        cmd.Parameters.AddWithValue("@password_text", password_text);
        cmd.Parameters.Add("@Result", MySqlDbType.Int32);
        cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
        try
        {
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean((Int32)cmd.Parameters["@Result"].Value);
        }
        catch (Exception ex)
        {
            registro_log(1, MethodBase.GetCurrentMethod().Name, ex.ToString(), ex.Message.ToString(), "Username: " + username_text + "/Password: " + password_text);
            return false;
        }
        finally
        {
            conn.Close();

        }


    }

    protected void registro_log(int debug_mode, string metodo, string var1, string var2, string var3)
    {
        if (debug_mode == 1)
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("~/logs/error/") + "Error(" + metodo + ")_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt", true);
            sw.WriteLine("---------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------");
            if (var1 != "") { sw.WriteLine(var1); }
            if (var2 != "") { sw.WriteLine(var2); }
            if (var3 != "") { sw.WriteLine(var3); }
            sw.WriteLine("------------------------------------------------------------------------------------------------------");
            sw.Close();
        }
        else if (debug_mode == 2)
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("~/logs/debug/") + "Debug(" + metodo + ")_" + DateTime.Now.ToString("dd_MM_yyyy") + ".txt", true);
            sw.WriteLine("---------------------------------------" + DateTime.Now.ToString() + "--------------------------------------------");
            if (var1 != "") { sw.WriteLine(var1); }
            if (var2 != "") { sw.WriteLine(var2); }
            if (var3 != "") { sw.WriteLine(var3); }
            sw.WriteLine("------------------------------------------------------------------------------------------------------");
            sw.Close();
        }


    }
}