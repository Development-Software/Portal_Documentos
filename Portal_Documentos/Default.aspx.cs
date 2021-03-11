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

public partial class _Default : System.Web.UI.Page
{
    applyWeb.Data.Data objUsuarios = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);


    protected void Page_Load(object sender, EventArgs e)
    {

        String networkId = CASP.Authenticate("https://www.ula.edu.mx/ncas/login", "http://www.ula.edu.mx/ncas/validate", this);
        //String networkId = CASP.Authenticate("https://ula-azure.ula.edu.mx/ncas/login", "http://www.ula-azure.ula.edu.mx/ncas/validate", this);

        if (!Page.IsPostBack)
        {

            //Es la matricula
            Session["CASNetworkID"] = networkId;
            String userId = Session["CASNetworkID"].ToString();


            if (networkId != null)
            {
                Session["userId"] = networkId;

                ArrayList arrParam = new ArrayList();
                arrParam.Add(new applyWeb.Data.Parametro("@IDAlumno", networkId));
                DataSet dsUsuario = objUsuarios.ExecuteSP("Obtener_Alumno", arrParam);
                if (dsUsuario.Tables[0].Rows.Count > 0)
                {
                    Session["idUser"] = networkId;
                    Session["user"] = dsUsuario.Tables[0].Rows[0]["Nombre"].ToString();
                    dsUsuario = null;
                }
            }

            else if (userId != "")
            {
                lblCas.Text = "ERROR DE AUTENTICACION POR CAS";
            }
            lblResult.Text = (String)Session["user"];
            TextBox1.Text = (String)Session["userId"];
            Ingresar_sitio();
            

        }
        else
        {
            //This code runs when it is postback request


        }


        if (Page.IsPostBack)
        {
            //This code also runs when it is postback request


        }


    }

    private void Ingresar_sitio()
    {
        if (!string.IsNullOrEmpty(TextBox1.Text))
        {
            string Usuario = "";
            //Service1 objLogin = new ulaDocumentos.ulaLogin.Service1();
            //ulaDocumentos.ulaLogin.spHeader secureHeader = new ulaDocumentos.ulaLogin.spHeader();
            System.Security.Cryptography.SHA1 Sh1 = new SHA1CryptoServiceProvider();
            string strUser = BitConverter.ToString(Sh1.ComputeHash(ASCIIEncoding.Default.GetBytes(System.Configuration.ConfigurationManager.AppSettings["strUser"]))).Replace("-", "");
            //secureHeader.strValor = strUser;
            //objLogin.spHeaderValue = secureHeader;
            //Usuario = objLogin.validacion(TextBox1.Text.ToLower(), TextBox1.Text, 1);
            //txtUsuario.Text = Usuario;
            if (Usuario.Equals(""))
            {

                Session["idUser"] = TextBox1.Text;
                Session["user"] = lblResult.Text;
                Session["Rol"] = "Alumno"; ;
                //Session["idUser"] = TextBox1.Text;
                //string[] strValores = Usuario.Split('|');
                //Session["user"] = strValores[0];
                //Session["Rol"] = "Alumno";
                FormsAuthentication.Initialize();
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                        TextBox1.Text, DateTime.Now, DateTime.Now.AddMinutes(20), false, Session["Rol"].ToString(), FormsAuthentication.FormsCookiePath);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(fat)));
                Response.Redirect("Inicio.aspx?sesion=1");
            }
            else
            {

                lblError.Text = "El Usuario y/o contraseña no son válidos";
                lblError.Visible = true;
            }
        }
    }

}