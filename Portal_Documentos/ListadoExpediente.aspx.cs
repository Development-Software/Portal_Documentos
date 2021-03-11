using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

public partial class ListadoExpediente : System.Web.UI.Page
{
    applyWeb.Data.Data objExpediente = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
    string id_page_ex;
    string id_page;
    string id_page_reload;
    public double porcentaje;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            cargar_pagina();
            permisos();
            if (TextBox1.Text == null || TextBox1.Text == "")
            {
                id_page_reload = TextBox2.Text;
            }
            else
            {
                id_page_reload = TextBox1.Text;
            }
        }
        catch { Response.Redirect("Inicio.aspx"); } 

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        if (Session["Rol"] == null || (Session["Rol"].ToString().Equals("Alumno") ))
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                Session["Reset"] = true;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = ((int)section.Timeout.TotalMinutes - 3) * 1000 * 60;
                ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
                
                try
                {
                    progress_bar();
                    carga_nombre_alumno();
                    CargaListaExpediente(Request.QueryString["IDAlumno"].ToString());
                    
                }
                catch (Exception ex)
                {
                    Response.Redirect("Inicio.aspx");
                }
                id_page_reload = TextBox1.Text;
            }
            try
            {
                if (Request.QueryString["Page"].ToString() == null || Request.QueryString["Page"].ToString() == "")
                {
                    id_page = "0";
                }
                else
                {
                    id_page = Request.QueryString["Page"].ToString();
                }
            }
            catch { Response.Redirect("Inicio.aspx"); }

        }
        search.Attributes.Add("placeholder", "Buscar...");
        
        string id_archivo = Convert.ToString(Request.QueryString["id_archivo"]);
        if (id_archivo != null)
        {
            elimina_preview(id_archivo, Request.QueryString["IDAlumno"].ToString());
        }
        
    }

    protected void CargaListaExpediente(string pIDAlumno)
    {
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDAlumno", pIDAlumno));
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", Session["Rol"].ToString()));
        //Session["AlumnoInperson"] = pIDAlumno;

        DataSet dsExpedientes = objExpediente.ExecuteSP("OBTENER_LISTADO_DOCUMENTOS_ALUMNO", arrParametros);
        gvExpediente.DataSource = dsExpedientes;
        gvExpediente.DataBind();
    }

    protected void gvExpediente_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GridView gv = (GridView)sender;
        gvExpediente.PageIndex = e.NewPageIndex;
        CargaListaExpediente(Request.QueryString["IDAlumno"].ToString());
        // this.gvExpediente.PageIndex = e.NewPageIndex;
        //this.CargaListaExpediente(Request.QueryString["IDAlumno"].ToString());
        id_page_ex = gvExpediente.PageIndex.ToString();
        TextBox1.Text = id_page_ex;
    }

    protected void search_TextChanged(object sender, EventArgs e)
    {
        CargaListaExpediente_search(Request.QueryString["IDAlumno"].ToString(), search.Text.Trim());
    }

    protected void CargaListaExpediente_search(string pIDAlumno,string ptext)
    {
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDAlumno", pIDAlumno));
        arrParametros.Add(new applyWeb.Data.Parametro("@text", ptext));
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", Session["Rol"].ToString()));
        Session["AlumnoInperson"] = pIDAlumno;

        DataSet dsExpedientes = objExpediente.ExecuteSP("OBTENER_LISTADO_DOCUMENTOS_ALUMNO_SEARCH", arrParametros);
        gvExpediente.DataSource = dsExpedientes;
        gvExpediente.DataBind();
    }

    protected void gvExpediente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Subir")
        {
            
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvExpediente.Rows[index];
            string IDTipoDocumento = row.Cells[0].Text;
            string IDDocumento = row.Cells[1].Text;
            string IDAlumno = row.Cells[2].Text;
            if (IDDocumento != "0")
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "", "cargar_doc()", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "cargar_doc('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento=" + IDDocumento + "&IDAlumno=" + IDAlumno + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno+"&Page="+id_page+ "&PageEx=" + id_page_reload + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog('InputFile.aspx?IDTipoDocumento=" + IDTipoDocumento + "&IDDocumento=" + IDDocumento + "&IDAlumno=" + IDAlumno + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno + "&Page="+id_page+ "&PageEx=" + id_page_reload + "');", true);
            }


        }
        else if (e.CommandName == "Preview")
        {
            DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvExpediente.Rows[index];
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
            GridViewRow row = gvExpediente.Rows[index];
            string IDDocumento = row.Cells[1].Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_Comentarios('Comentarios.aspx?IDDocumento=" + IDDocumento + "');", true);

        }
    }

    private void cargar_vista_previa(string IdTipoDocumento, string IDDocumento, string IDAlumno)
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
            string path = vPath.ToString() + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension;
            File.WriteAllBytes(path, bytes);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JQDialog_preview('Preview_Page.aspx?id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "&formato=" + extension + "&IDDocumento="+IDDocumento+ "&IDTipoDocumento=" + IdTipoDocumento + "','ListadoExpediente.aspx?IDAlumno=" + IDAlumno+"&id_archivo=" + IdTipoDocumento + "_" + IDDocumento + "_" + IDAlumno + extension + "&Page=" + id_page + "&PageEx=" + id_page_reload + "');", true);

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
    protected void gvExpediente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string IDDocumento = e.Row.Cells[1].Text;
            string Estatus = e.Row.Cells[4].Text;
            ImageButton preview = e.Row.FindControl("imgPreview") as ImageButton;
            ImageButton subir = e.Row.FindControl("imgExpediente") as ImageButton;
            ImageButton comentarios = e.Row.FindControl("imgComentarios") as ImageButton;
            if (IDDocumento != "0" && Estatus == "ACEPTADO")
            {
                preview.Visible = true;
                subir.Visible = false;
                comentarios.Visible = true;
            }
            else if (IDDocumento != "0" && Estatus != "ACEPTADO")
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

    private void elimina_preview(string id_archivo, string IDAlumno)
    {
        string id_page_reload_1 = Convert.ToString(Request.QueryString["PageEx"]);
        DirectoryInfo vPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));
        File.Delete(vPath.ToString() + id_archivo);
        Response.Redirect("ListadoExpediente.aspx?IDAlumno="+IDAlumno + "&Page=" + id_page + "&PageEx=" + id_page_reload_1);
    }

    protected void carga_nombre_alumno()
    {
        string IDAlumno = Request.QueryString["IDAlumno"].ToString();
        string strQuery = "SELECT DISTINCT Nombre FROM Alumno WHERE IDAlumno=@IDAlumno";
        SqlCommand cmd = new SqlCommand(strQuery);
        cmd.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
        DataTable dt = GetData(cmd);
        Nombre_Alumno.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dt.Rows[0]["Nombre"].ToString());
        
    }

    protected void back_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ListadoAdministracion.aspx?Page=" + id_page);


    }

    protected void LinkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListadoAdministracion.aspx?Page=" + id_page);
    }
    protected void cargar_pagina()
    {
        
        string page_index = Request.QueryString["PageEx"].ToString();
        if (page_index != null)
        {
            gvExpediente.PageIndex = Convert.ToInt32(page_index);
            TextBox2.Text = page_index;
        }
        else
        {
            gvExpediente.PageIndex = 0;
            TextBox2.Text = "0";
        }
        
        //TextBox1.Text = page_index;
    }

    protected void permisos()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=3 AND B.IDSubMenu=1 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
        SqlDataReader dr = cmd.ExecuteReader();
        gvExpediente.Columns[6].Visible = false;
        while (dr.Read())
        {   
            int IDprivilegio = dr.GetInt32(0);

            if (IDprivilegio == 19) { gvExpediente.Columns[6].Visible = true; } //Permiso para subir documentos
           

        }
        ConexionSql.Close();
    }

    protected void progress_bar()
    {
        string IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);


        string strQuery = "SELECT DISTINCT COUNT(TDA.IDTipoDocumento)Documentos, " +
                          "(SELECT COUNT(*) FROM Documentos_Alumno WHERE IDEstatusDocumento in (17,3) AND IDAlumno='" + IDAlumno + "')Entregados " +
                          "FROM TiposDocumento_Area TDA " +
                          "JOIN TipoDocumento TD ON TD.IDTipoDocumento = TDA.IDTipoDocumento " +
                          "JOIN Alumno AL ON AL.IDArea = TDA.IDArea AND AL.CodigoProcedencia = TDA.IDProcedencia AND AL.CodigoTipoIngreso = TDA.IDTipoIngreso AND AL.IDNivel=TDA.IDNivel  AND AL.IDModalidad=TDA.IDModalidad AND AL.IDAlumno = '" + IDAlumno + "' " +
                          "WHERE TD.id_banner NOT IN ('RVAL','SVAL')";
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = GetData(cmd);
        porcentaje = (Convert.ToDouble(dt.Rows[0]["Entregados"]) * 100) / Convert.ToDouble(dt.Rows[0]["Documentos"]);
        
        lbl_bar.Text = "Documentos entregados " + dt.Rows[0]["Entregados"].ToString() + " de "+ dt.Rows[0]["Documentos"] .ToString()+ " ("+ Math.Round(porcentaje).ToString()+"%)";
        
        if (Math.Round(porcentaje) < 40)
        {
            html_progress_bar.Attributes.Add("style", "width:" + Math.Round(porcentaje).ToString() + "%;color:#000;");
            html_progress_bar.Attributes.Add("class", "progress-bar progress-bar-striped progress-bar-animated");
        }
        else if(Math.Round(porcentaje) > 90)
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