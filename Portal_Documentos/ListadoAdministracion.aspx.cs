using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Text;
using ClosedXML;
using ClosedXML.Excel;
using System.Web.Configuration;
using MySql.Data.MySqlClient;

public partial class ListadoAdministracion : System.Web.UI.Page
{
    string id_export = null;
    string id_page = null;
    
    applyWeb.Data.Data objAdministracion = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            cargar_pagina();
        }
        catch { Response.Redirect("Inicio.aspx"); }

        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        if (Session["Rol"] == null || (Session["Rol"].ToString().Equals("Alumno")))
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

                permisos();

                if (chkSoloCompletos.Checked == true)
                {
                    string strStatus = "COMPLETO";
                    CargaListaAlumnos(Session["Rol"].ToString(), Session["User"].ToString(), strStatus);
                }
                else
                {
                    string strStatus = "PENDIENTE";
                    CargaListaAlumnos(Session["Rol"].ToString(), Session["User"].ToString(), strStatus);
                }

            }
            search.Attributes.Add("placeholder", "Buscar...");
            
        }
    }


    protected void CargaListaAlumnos(string pRol, string pIDusuario, string pStatus)
    {

        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", pRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@User", pIDusuario));
        arrParametros.Add(new applyWeb.Data.Parametro("@Status", pStatus));
        DataSet dsAlumnos = objAdministracion.ExecuteSP("Obtener_Listado_Alumnos", arrParametros);
        gvAlumnos.DataSource = dsAlumnos;
        gvAlumnos.DataBind();
        
    }

    protected void chkSoloCompletos_CheckedChanged(object sender, EventArgs e)
    {
        string strStatus = "PENDIENTE";

        if (chkSoloCompletos.Checked == true)
            strStatus = "COMPLETO";

        CargaListaAlumnos(Session["Rol"].ToString(), Session["User"].ToString(), strStatus);
    }

    protected void permisos()
    {
        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        ConexionMySql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=3 AND B.IDSubMenu=1 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        MySqlCommand cmd = new MySqlCommand(strQuery, ConexionMySql);
        MySqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            int IDprivilegio = dr.GetInt32(0);

            if (IDprivilegio == 17) { exportar.Visible = true; } //Permiso para Exportar
            else if (IDprivilegio == 18) { chkSoloCompletos.Enabled = true; chkSoloCompletos.Visible = true; } //Permiso para Expedientes completos
            else
            {
                exportar.Visible = false;
                chkSoloCompletos.Visible = false;
            }


        }
        ConexionMySql.Close();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void cmdExportar_Click(object sender, EventArgs e)
    {
        string strStatus = "PENDIENTE";

        if (chkSoloCompletos.Checked == true)
            strStatus = "COMPLETO";

        CargaListaAlumnos_export(Session["Rol"].ToString(), Session["User"].ToString(), strStatus);

        Export_Excel("Listado_Documentos");
    }
    private void Export_Excel(string nombre_archivo)
    {
        DataTable dt = new DataTable("GridView_Data");
        foreach (TableCell cell in GridView1.HeaderRow.Cells)
        {

            dt.Columns.Add(cell.Text.Replace("&nbsp;", ""));
        }
        foreach (GridViewRow row in GridView1.Rows)
        {
            dt.Rows.Add();
            for (int i = 0; i < row.Cells.Count; i++)
            {
                dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text.Replace("&nbsp;", "");
            }
        }
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + nombre_archivo + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }
    //protected void Exportar_Datos_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    id_export = Exportar_Datos.SelectedValue.ToString();
    //}

    protected void gvAlumnos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Expediente")
        {
            int pagina = gvAlumnos.PageIndex;
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAlumnos.Rows[index];
            string Matricula = row.Cells[0].Text;
            if(TextBox1.Text==null || TextBox1.Text == "")
            {
                Response.Redirect("ListadoExpediente.aspx?IDAlumno=" + Matricula + "&Page=" + TextBox2.Text + "&PageEx=0");
            }
            else
            {
                Response.Redirect("ListadoExpediente.aspx?IDAlumno=" + Matricula + "&Page=" + TextBox1.Text + "&PageEx=0");
            }
            
        }
    }

    protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strStatus = "PENDIENTE";

        if (chkSoloCompletos.Checked == true)
            strStatus = "COMPLETO";
        //GridView gv = (GridView)sender;
        gvAlumnos.PageIndex = e.NewPageIndex;
        CargaListaAlumnos(Session["Rol"].ToString(), Session["User"].ToString(), strStatus);
        id_page = gvAlumnos.PageIndex.ToString();
        TextBox1.Text = id_page;

    }

    protected void search_TextChanged(object sender, EventArgs e)
    {
        string strStatus = "PENDIENTE";

        if (chkSoloCompletos.Checked == true)
            strStatus = "COMPLETO";
        CargaListaAlumnos_search(Session["Rol"].ToString(), Session["User"].ToString(), strStatus, search.Text.Trim());
    }

    protected void CargaListaAlumnos_search(string pRol, string pIDusuario, string pStatus,string ptext)
    {

        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", pRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@User", pIDusuario));
        arrParametros.Add(new applyWeb.Data.Parametro("@Status", pStatus));
        arrParametros.Add(new applyWeb.Data.Parametro("@text", ptext));
        DataSet dsAlumnos = objAdministracion.ExecuteSP("Obtener_Listado_Alumnos_Search", arrParametros);
        gvAlumnos.DataSource = dsAlumnos;
        gvAlumnos.DataBind();
    }

    protected void cargar_pagina()
    {
        string page_index = Request.QueryString["Page"].ToString();

            gvAlumnos.PageIndex = Convert.ToInt32(page_index);
        TextBox2.Text = page_index;
        //TextBox1.Text = page_index;
    }

    protected void CargaListaAlumnos_export(string pRol, string pIDusuario, string pStatus)
    {

        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@Rol", pRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@User", pIDusuario));
        arrParametros.Add(new applyWeb.Data.Parametro("@Status", pStatus));
        DataSet dsAlumnos = objAdministracion.ExecuteSP("Obtener_Listado_Alumnos", arrParametros);
        GridView1.DataSource = dsAlumnos;
        GridView1.DataBind();

    }
}