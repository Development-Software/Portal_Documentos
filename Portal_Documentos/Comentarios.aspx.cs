using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Comentarios : System.Web.UI.Page
{
    applyWeb.Data.Data objExpediente = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
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
        if (Convert.ToString(Request.QueryString["IDDocumento"]) == null)
        {
            Response.Redirect("Inicio.aspx");
        }
        Cargacomentarios(Convert.ToString(Request.QueryString["IDDocumento"]));
    }

    protected void Cargacomentarios(string IDDocumento)
    {
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDDocumento", IDDocumento));
        DataSet dsExpedientes = objExpediente.ExecuteSP("Obtener_Comentarios_Documento", arrParametros);
        GridViewComentarios.DataSource = dsExpedientes;
        GridViewComentarios.DataBind();
    }
}