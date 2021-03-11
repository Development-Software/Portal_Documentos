using System;
using System.Web;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.DirectoryServices;

public partial class Administrativos : System.Web.UI.Page
{
    applyWeb.Data.Data objUsuarios = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsuario.Attributes.Add("placeholder", "Usuario");
        txtPassword.Attributes.Add("placeholder", "Contraseña");
    }

    protected void cmdEntrar_Click(object sender, EventArgs e)
    {

        if (directorio(txtUsuario.Text, txtPassword.Text))
        {

            Session["Rol"] = "";
            Session["user"] = txtUsuario.Text;

            //if (UserIsValid(txtUsuario.Text))

            SqlConnection ConexionSql =
            new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

            //Obtiene si el estudiante ya tiene registro
            string strQueryok = "";
            strQueryok = " SELECT COUNT(*) FROM Usuario WHERE Login='" + txtUsuario.Text + "'";

            string strQueryrol = "";
            strQueryrol = "SELECT b.Nombre FROM Roles_Usuarios a INNER JOIN Rol b ON a.IDRol=b.IDRol INNER JOIN Usuario c ON c.IDUsuario=a.IDUsuario WHERE c.Login='" + txtUsuario.Text + "'";

            ConexionSql.Open();

            SqlDataAdapter sqladapter = new SqlDataAdapter();
            DataSet dssql = new DataSet();
            SqlCommand commandsql = new SqlCommand(strQueryok, ConexionSql);
            sqladapter.SelectCommand = commandsql;
            sqladapter.Fill(dssql);
            sqladapter.Dispose();
            commandsql.Dispose();
            ConexionSql.Close();
            if (dssql.Tables[0].Rows[0][0].ToString() != "0")
            {
                ConexionSql.Open();

                SqlDataAdapter sqladapter1 = new SqlDataAdapter();
                DataSet dssql1 = new DataSet();
                SqlCommand commandsql1 = new SqlCommand(strQueryrol, ConexionSql);
                sqladapter1.SelectCommand = commandsql1;
                sqladapter1.Fill(dssql1);
                sqladapter1.Dispose();
                commandsql1.Dispose();
                ConexionSql.Close();

                Session["Rol"] = dssql1.Tables[0].Rows[0][0].ToString().Trim();
                //Session["clave_campus"] = dssql1.Tables[0].Rows[0][1].ToString().Trim();

                FormsAuthentication.Initialize();
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                        txtUsuario.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Reportador", FormsAuthentication.FormsCookiePath);
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

    private Boolean directorio(string username, string password)
    {
        bool respuesta = false;
        //logueamos a los directivos a través de LDAP
        DirectoryEntry entry = new DirectoryEntry(System.Configuration.ConfigurationManager.AppSettings["LDAP_Domain"].ToString(), username, password);
        try
        {
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(" + System.Configuration.ConfigurationManager.AppSettings["LDAP_FilterPart"].ToString() + "=" + username + ")";
            search.PropertiesToLoad.Add(System.Configuration.ConfigurationManager.AppSettings["LDAP_PropertyPart"].ToString());
            SearchResult result = search.FindOne();
            DirectoryEntry elementAD = result.GetDirectoryEntry();

            if (result.Path != "")
            {
                Session["FullName"] = "";
                Session["Extension"] = "";
                Session["mail"] = "";

                if (elementAD.Properties["displayName"].Value != null)
                    Session["FullName"] = elementAD.Properties["displayName"].Value.ToString();

                if (elementAD.Properties["telephoneNumber"].Value != null)
                    Session["Extension"] = elementAD.Properties["telephoneNumber"].Value.ToString();

                if (elementAD.Properties["mail"].Value != null)
                    Session["mail"] = elementAD.Properties["mail"].Value.ToString();

                respuesta = true;
            }
        }
        catch
        { respuesta = false; }
        return respuesta;
    }
}