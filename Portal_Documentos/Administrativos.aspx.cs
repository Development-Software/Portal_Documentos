using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.DirectoryServices;
using MySql.Data.MySqlClient;
using System.IO;
using System.Reflection;

public partial class Administrativos : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsuario.Attributes.Add("placeholder", "Usuario");
        txtPassword.Attributes.Add("placeholder", "Contraseña");
        
    }

    protected void cmdEntrar_Click(object sender, EventArgs e)
    {

        if (autenticacion(txtUsuario.Text, txtPassword.Text))
        {

            Session["Rol"] = "";
            Session["user"] = txtUsuario.Text;

            MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            //Obtiene si el estudiante ya tiene registro
            string strQueryok = "";
            strQueryok = " SELECT COUNT(*) FROM Usuario WHERE Login='" + txtUsuario.Text + "'";

            string strQueryrol = "";
            strQueryrol = "SELECT b.Nombre FROM Roles_Usuarios a INNER JOIN Rol b ON a.IDRol=b.IDRol INNER JOIN Usuario c ON c.IDUsuario=a.IDUsuario WHERE c.Login='" + txtUsuario.Text + "'";

            ConexionMySql.Open();
            MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
            DataSet dsmysql = new DataSet();
            MySqlCommand cmdmysql = new MySqlCommand(strQueryok, ConexionMySql);
            mysqladapter.SelectCommand = cmdmysql;
            mysqladapter.Fill(dsmysql);
            mysqladapter.Dispose();
            cmdmysql.Dispose();
            ConexionMySql.Close();
            if (dsmysql.Tables[0].Rows[0][0].ToString() != "0")
            {
                ConexionMySql.Open();

                MySqlDataAdapter mysqladapter1 = new MySqlDataAdapter();
                DataSet dsmysql1 = new DataSet();
                MySqlCommand cmdmysql1 = new MySqlCommand(strQueryrol, ConexionMySql);
                mysqladapter1.SelectCommand = cmdmysql1;
                mysqladapter1.Fill(dsmysql1);
                mysqladapter1.Dispose();
                cmdmysql1.Dispose();
                ConexionMySql.Close();

                Session["Rol"] = dsmysql1.Tables[0].Rows[0][0].ToString().Trim();
                //Session["clave_campus"] = dssql1.Tables[0].Rows[0][1].ToString().Trim();

                FormsAuthentication.Initialize();
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                        txtUsuario.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Repositorio", FormsAuthentication.FormsCookiePath);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));

                Response.Redirect("Inicio.aspx");
            }
            else
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>swal('Usuario no existe','Su usuario no existe favor de contartar al Administrador', 'error')</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "", "no_existe();", true);
            }

        }
        else
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Usuario y/o Contraseña incorrecta','Favor de validar los datos ingresados', 'warning')</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "", "datos();", true);
        }

    }

    private bool autenticacion(string username_text, string password_text)
    {
        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        MySqlCommand cmd = new MySqlCommand("Login", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        conn.Open();

        cmd.Parameters.AddWithValue("@username", username_text);
        cmd.Parameters.AddWithValue("@password_text", password_text);
        cmd.Parameters.Add("@Result", MySqlDbType.Int32);
        cmd.Parameters["@Result"].Direction= ParameterDirection.Output;
        try
        {
            cmd.ExecuteNonQuery();
            return Convert.ToBoolean((Int32)cmd.Parameters["@Result"].Value);
        }
        catch(Exception ex)
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