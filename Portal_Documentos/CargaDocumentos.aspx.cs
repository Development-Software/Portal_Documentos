using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Web.Configuration;

public partial class CargaDocumentos : System.Web.UI.Page
{
    applyWeb.Data.Data objExpediente = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
    public double porcentaje;

    protected void Page_Load(object sender, EventArgs e)
    {
        string valor = Convert.ToString(Request.QueryString["contrato"]);
        
        Page.Header.DataBind();
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        if (Session["Rol"] == null || !Session["Rol"].ToString().Equals("Alumno"))
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            
                if (valor == "1")
                {
                    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
                    string strQuery = "";
                    strQuery = "UPDATE Alumno SET ContratoAceptado='" + valor + "' WHERE IDAlumno='" + Session["CASNetworkID"].ToString() + "'";

                    ConexionSql.Open();
                    SqlCommand commandsql = new SqlCommand(strQuery, ConexionSql);
                    commandsql.ExecuteNonQuery();
                    Response.Redirect("CargaDocumentos.aspx");
                }
                else
                {
                    if (!Acepta_Privacidad1())
                        Response.Redirect("Privacidad.aspx");
                }
            
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                Session["Reset"] = true;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = ((int)section.Timeout.TotalMinutes - 3) * 1000 * 60;
                ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

                progress_bar();
                CargaListaExpediente(Session["CASNetworkID"].ToString());
                
                List_Docs.DataBind();
            }
        }
        search.Attributes.Add("placeholder", "Buscar...");
        string id_archivo = Convert.ToString(Request.QueryString["id_archivo"]);
        if (id_archivo!=null)
        {
            elimina_preview(id_archivo);
        }
        
    }


    protected void CargaListaExpediente(string pIDAlumno)
    {
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDAlumno", pIDAlumno));
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", "0"));
        DataSet dsExpedientes = objExpediente.ExecuteSP("OBTENER_LISTADO_DOCUMENTOS_ALUMNO", arrParametros);
        List_Docs.DataSource = dsExpedientes;
        List_Docs.DataBind();
    }





    //Valida que el las politicas de privacidad fueron aceptadas
    protected bool Acepta_Privacidad1()
    {
        ArrayList arrParam = new ArrayList();
        arrParam.Add(new applyWeb.Data.Parametro("@IDAlumno", Session["idUser"].ToString()));
        DataSet objDS = objExpediente.ExecuteSP("Revisar_ContratoAceptado", arrParam);
        if (objDS.Tables[0].Rows.Count > 0)
        {
            if (objDS.Tables[0].Rows[0]["ContratoAceptado"].ToString().ToLower().Equals("true"))
                return true;
            else
                return false;
        }
        return false;
    }




    protected void List_Docs_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Subir")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = List_Docs.Rows[index];
            string IDTipoDocumento = row.Cells[0].Text;
            string IDDocumento = row.Cells[1].Text;
            string IDAlumno = row.Cells[2].Text;
            if (IDDocumento != "0")
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "", "cargar_doc()", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "cargar_doc('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento="+IDDocumento+"&IDAlumno="+IDAlumno+"','CargaDocumentos.aspx')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento="+IDDocumento+ "&IDAlumno="+ IDAlumno+"','CargaDocumentos.aspx');", true);
            }


        }
        else if (e.CommandName == "Preview")
        {
            DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = List_Docs.Rows[index];
            string IDTipoDocumento = row.Cells[0].Text;
            string IDDocumento = row.Cells[1].Text;
            string IDAlumno = row.Cells[2].Text;
            cargar_vista_previa(IDTipoDocumento, IDDocumento, IDAlumno);
            //string url= vPath.ToString() + IDTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_preview('Preview_Page.aspx?id_tipod=" + IDTipoDocumento + "&id_doc="+IDDocumento+"');", true);
        }
        else if (e.CommandName == "Comentarios")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = List_Docs.Rows[index];
            string IDDocumento = row.Cells[1].Text;
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_Comentarios('Comentarios.aspx?IDDocumento=" + IDDocumento + "');", true);
            
        }
    }

    protected void search_TextChanged(object sender, EventArgs e)
    {
        CargaListaExpediente_search(Session["idUser"].ToString(), search.Text.Trim());
    }

    protected void CargaListaExpediente_search(string pIDAlumno, string ptext)
    {
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDAlumno", pIDAlumno));
        arrParametros.Add(new applyWeb.Data.Parametro("@text", ptext));
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", "1"));
        DataSet dsExpedientes = objExpediente.ExecuteSP("OBTENER_LISTADO_DOCUMENTOS_ALUMNO_SEARCH", arrParametros);
        List_Docs.DataSource = dsExpedientes;
        List_Docs.DataBind();
    }



    protected void List_Docs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string IDDocumento = e.Row.Cells[1].Text;
            string Estatus = e.Row.Cells[4].Text;
            ImageButton preview = e.Row.FindControl("imgPreview") as ImageButton;
            ImageButton subir = e.Row.FindControl("imgExpediente") as ImageButton;
            ImageButton comentarios = e.Row.FindControl("imgComentarios") as ImageButton;
            if (IDDocumento != "0" && Estatus== "ACEPTADO")
            {
                preview.Visible = true;
                subir.Visible = false;
                comentarios.Visible = true;
            }
            else if(IDDocumento != "0" && Estatus != "ACEPTADO")
            {
                preview.Visible = true;
                comentarios.Visible = true;
            }
            else
            {
                preview.Visible = false;
                comentarios.Visible = false;
            }
        }


   }

    private void cargar_vista_previa( string IdTipoDocumento,string IDDocumento, string IDAlumno)
    {
        DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
        //string IdTipoDocumento = Convert.ToString(Request.QueryString["id_tipod"]);
        //string IDDocumento = Convert.ToString(Request.QueryString["id_doc"]);
        //string IDAlumno = Session["CASNetworkID"].ToString();
        string strQuery = "SELECT DISTINCT Documento,Formato FROM Documentos_Alumno WHERE IDDocumento=@IDDocumento AND IDTipoDocumento=@IDTipoDocumento AND IDAlumno=@IDAlumno";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@IDDocumento", SqlDbType.Int).Value = Convert.ToInt32(IDDocumento);
        cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = Convert.ToInt32(IdTipoDocumento);
        cmd.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
        DataTable dt = GetData(cmd);
        if (dt != null)
        {
            Byte[] bytes = (Byte[])dt.Rows[0]["Documento"];
            string extension = "." + dt.Rows[0]["Formato"];
            string path = vPath.ToString()+ IdTipoDocumento+"_" + IDDocumento + "_" + IDAlumno + extension;
            File.WriteAllBytes(path, bytes);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_preview('Preview_Page.aspx?id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "&formato=" + extension + "&IDDocumento=" + IDDocumento + "&IDTipoDocumento=" + IdTipoDocumento + "','CargaDocumentos.aspx?id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension+"');", true);
            
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

    private void elimina_preview(string id_archivo)
    {
        
        DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
        File.Delete(vPath.ToString() + id_archivo);
        Response.Redirect("CargaDocumentos.aspx");
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
    protected void progress_bar()
    {


        string strQuery = "SELECT DISTINCT COUNT(TDA.IDTipoDocumento)Documentos, " +
                          "(SELECT COUNT(*) FROM Documentos_Alumno WHERE IDEstatusDocumento in (17,3) AND IDAlumno='" + Session["CASNetworkID"].ToString() + "')Entregados " +
                          "FROM TiposDocumento_Area TDA " +
                          "JOIN TipoDocumento TD ON TD.IDTipoDocumento = TDA.IDTipoDocumento " +
                          "JOIN Alumno AL ON AL.IDArea = TDA.IDArea AND AL.CodigoProcedencia = TDA.IDProcedencia AND AL.CodigoTipoIngreso = TDA.IDTipoIngreso AND AL.IDNivel=TDA.IDNivel  AND AL.IDModalidad=TDA.IDModalidad AND AL.IDAlumno = '" + Session["CASNetworkID"].ToString() + "' " +
                          "WHERE TD.id_banner NOT IN ('RVAL','SVAL')";
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = GetData(cmd);
        porcentaje = (Convert.ToDouble(dt.Rows[0]["Entregados"]) * 100) / Convert.ToDouble(dt.Rows[0]["Documentos"]);

        lbl_bar.Text = "Documentos entregados " + dt.Rows[0]["Entregados"].ToString() + " de " + dt.Rows[0]["Documentos"].ToString() + " (" + Math.Round(porcentaje).ToString() + "%)";

        if (Math.Round(porcentaje) < 40)
        {
            html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%;color:#000;");
            html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated");
        }
        else if (Math.Round(porcentaje) > 90)
        {
            html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%");
            html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated bg-success");
        }
        else
        {
            html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%");
            html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated");
        }
    }

}