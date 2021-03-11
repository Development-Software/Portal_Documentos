using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.Security;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using CheckBox = System.Web.UI.WebControls.CheckBox;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.Web.Configuration;

public partial class Permisos : System.Web.UI.Page
{
    applyWeb.Data.Data objAreas = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string valor = Convert.ToString(Request.QueryString["rol"]);
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
            Response.End();
        }
        if (Session["Rol"] == null || Session["Rol"].ToString().Equals("Alumno"))
        {
            Response.Redirect("Default.aspx");
        }
        else
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            Session["Reset"] = true;
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            int timeout = ((int)section.Timeout.TotalMinutes - 3) * 1000 * 60;
            ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

            CargaListaRoles();
            Permisos_1.Visible = false;
            permisos();
            Permisos_App.Visible = true;
            CargarPermisos_App();

            if (valor == "1")
            {
                ddlRol.SelectedValue = "1";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }
            else if (valor == "2")
            {
                ddlRol.SelectedValue = "2";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }
            else if (valor == "3")
            {
                ddlRol.SelectedValue = "3";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }
            else if (valor == "4")
            {
                ddlRol.SelectedValue = "4";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }
            else if (valor == "5")
            {
                ddlRol.SelectedValue = "5";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }
            else if (valor == "6")
            {
                ddlRol.SelectedValue = "6";
                Permisos_1.Visible = true;
                CargaListaCampus();
                CargaListaNiveles();
                CargaListaDocumentos();
                CargaListaDocumentos_estatus();
                ocultar_perm_desplegables();
                lstUsuariosCampus.Items.Clear();
                lstUsuarios.Items.Clear();
                ListUsuariosNivel.Items.Clear();
                ListUsuarios.Items.Clear();
                ListUsuariosDocumentos.Items.Clear();
                ListUsuarios_1.Items.Clear();

                consulta_permisos_user();
            }

        }
        ocultar_img();
    }
    protected void CargaListaRoles()
    {
        ArrayList arrParam = new ArrayList();
        ddlRol.DataSource = objAreas.ExecuteSP("Obtener_Lista_Roles", arrParam);
        ddlRol.DataTextField = "Nombre";
        ddlRol.DataValueField = "IDRol";
        ddlRol.DataBind();
        ddlRol.Items.Insert(0, new ListItem("--- Seleccionar ---", "0"));
    }

    protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRol.SelectedValue == "0")
        {
            Permisos_1.Visible = false;
        }
        else
        if (ddlRol.SelectedIndex > 0)
        {

            //this.Response.AddHeader("REFRESH", "2;URL=UsuarioArea.aspx?rol=" + ddlRol.SelectedValue.ToString());
            Response.Redirect("Permisos.aspx?rol=" + ddlRol.SelectedValue.ToString());

        }

    }


    protected void CargaListaCampus()
    {
        ArrayList arrParametros = new ArrayList();
        DataSet dsCampus = objAreas.ExecuteSP("ObtenerListaCampus", arrParametros);
        lstCampus.DataSource = dsCampus;
        lstCampus.DataTextField = "Nombre";
        lstCampus.DataValueField = "IDCampus";
        lstCampus.DataBind();
    }

    protected void lstCampus_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaListaCampus_Usuarios();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast();", true);
        CargaListaUsuarios_C();
        ListUsuariosNivel.Items.Clear();
        ListUsuarios.Items.Clear();
        ListNiveles.ClearSelection();
        ListUsuariosDocumentos.Items.Clear();
        ListUsuarios_1.Items.Clear();
        ListDocumentos.ClearSelection();
        lstCampus.Focus();

    }

    protected void CargaListaCampus_Usuarios()
    {

        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDCampus = lstCampus.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDCampus", IDCampus));
        DataSet dsUsuariosCampus = objAreas.ExecuteSP("ObtenerUsuariosCampus", arrParametros);
        lstUsuariosCampus.DataSource = dsUsuariosCampus;
        lstUsuariosCampus.DataTextField = "Nombre";
        lstUsuariosCampus.DataValueField = "IDUsuario";
        lstUsuariosCampus.DataBind();
    }
    protected void CargaListaUsuarios_C()
    {
        string IDRol = ddlRol.SelectedItem.Value.ToString();

            string IDCampus = lstCampus.SelectedItem.Value.ToString();
            ArrayList arrParametros = new ArrayList();
            arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
            arrParametros.Add(new applyWeb.Data.Parametro("@IDCampus", IDCampus));
            DataSet dsUsuario_C = objAreas.ExecuteSP("Obtener_ListaUsuario_C", arrParametros);
            lstUsuarios.DataSource = dsUsuario_C;
            lstUsuarios.DataTextField = "Nombre";
            lstUsuarios.DataValueField = "IDUsuario";
            lstUsuarios.DataBind();
    }


    protected void cmdAgregar_Click(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < lstCampus.GetSelectedIndices().Length; x++)
        {
            string IDCampus = lstCampus.Items[lstCampus.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < lstUsuarios.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = lstUsuarios.Items[lstUsuarios.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();

                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDCampus", IDCampus));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                objAreas.ExecuteInsertSP("Insertar_Permisos_Campus", arrParam);
            }
        }
            
        CargaListaCampus_Usuarios();
        CargaListaUsuarios_C();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast();", true);
    }
    protected void cmdBorrar_Click(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < lstCampus.GetSelectedIndices().Length; x++)
        {
            string IDCampus = lstCampus.Items[lstCampus.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < lstUsuariosCampus.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = lstUsuariosCampus.Items[lstUsuariosCampus.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDCampus", IDCampus));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));

                objAreas.ExecuteInsertSP("Eliminar_Permisos_Campus", arrParam);
            }
        }
        CargaListaCampus_Usuarios();
        CargaListaUsuarios_C();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast();", true);
    }

    protected void CargaListaNiveles()
    {
        ArrayList arrParametros = new ArrayList();
        DataSet dsNiveles = objAreas.ExecuteSP("ObtenerListaNiveles", arrParametros);
        ListNiveles.DataSource = dsNiveles;
        ListNiveles.DataTextField = "Descripcion";
        ListNiveles.DataValueField = "Codigo";
        ListNiveles.DataBind();
    }

    protected void ListNiveles_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaListaNiveles_Usuarios();
        CargaListaUsuarios_N();
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_n();", true);
        lstUsuariosCampus.Items.Clear();
        lstUsuarios.Items.Clear();
        lstCampus.ClearSelection();
        ListUsuariosDocumentos.Items.Clear();
        ListUsuarios_1.Items.Clear();
        ListDocumentos.ClearSelection();
        ListNiveles.Focus();
    }
    protected void CargaListaNiveles_Usuarios()
    {

        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDNivel = ListNiveles.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDNivel", IDNivel));
        DataSet dsUsuariosNivel = objAreas.ExecuteSP("ObtenerUsuariosNivel", arrParametros);
        ListUsuariosNivel.DataSource = dsUsuariosNivel;
        ListUsuariosNivel.DataTextField = "Nombre";
        ListUsuariosNivel.DataValueField = "IDUsuario";
        ListUsuariosNivel.DataBind();
    }

    protected void CargaListaUsuarios_N()
    {
        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDNivel = ListNiveles.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDNivel", IDNivel));
        DataSet dsUsuario_N = objAreas.ExecuteSP("Obtener_ListaUsuario_N", arrParametros);
        ListUsuarios.DataSource = dsUsuario_N;
        ListUsuarios.DataTextField = "Nombre";
        ListUsuarios.DataValueField = "IDUsuario";
        ListUsuarios.DataBind();
    }


    protected void cmdAgregar_Click_n(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListNiveles.GetSelectedIndices().Length; x++)
        {
            string IDNivel = ListNiveles.Items[ListNiveles.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListUsuarios.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListUsuarios.Items[ListUsuarios.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDNivel", IDNivel));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                objAreas.ExecuteInsertSP("Insertar_Permisos_Niveles", arrParam);
            }
        }
        CargaListaNiveles_Usuarios();
        CargaListaUsuarios_N();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_n();", true);
    }
    protected void cmdBorrar_Click_n(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListNiveles.GetSelectedIndices().Length; x++)
        {
            string IDNivel = ListNiveles.Items[ListNiveles.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListUsuariosNivel.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListUsuariosNivel.Items[ListUsuariosNivel.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDNivel", IDNivel));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                objAreas.ExecuteInsertSP("Eliminar_Permisos_Niveles", arrParam);
            }
        }
        CargaListaNiveles_Usuarios();
        CargaListaUsuarios_N();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_n();", true);
    }

    protected void CargaListaDocumentos()
    {
        ArrayList arrParametros = new ArrayList();
        DataSet dsDocumentos = objAreas.ExecuteSP("ObtenerListaDocumentos", arrParametros);
        ListDocumentos.DataSource = dsDocumentos;
        ListDocumentos.DataTextField = "Nombre";
        ListDocumentos.DataValueField = "IDTipoDocumento";
        ListDocumentos.DataBind();
    }

    protected void ListDocumentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaListaDoc_Usuarios();
        CargaListaUsuarios_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
        lstUsuariosCampus.Items.Clear();
        lstUsuarios.Items.Clear();
        lstCampus.ClearSelection();
        ListUsuariosNivel.Items.Clear();
        ListUsuarios.Items.Clear();
        ListNiveles.ClearSelection();
        ListDocumentos.Focus();
        
    }

    protected void CargaListaDoc_Usuarios()
    {

        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDTipoDocumento = ListDocumentos.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
        DataSet dsUsuariosDoc = objAreas.ExecuteSP("ObtenerUsuariosDocumentos", arrParametros);
        ListUsuariosDocumentos.DataSource = dsUsuariosDoc;
        ListUsuariosDocumentos.DataTextField = "Nombre";
        ListUsuariosDocumentos.DataValueField = "IDUsuario";
        ListUsuariosDocumentos.DataBind();
    }

    protected void CargaListaUsuarios_D()
    {
        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDTipoDocumento = ListDocumentos.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
        DataSet dsUsuario_D = objAreas.ExecuteSP("Obtener_ListaUsuario_D", arrParametros);
        ListUsuarios_1.DataSource = dsUsuario_D;
        ListUsuarios_1.DataTextField = "Nombre";
        ListUsuarios_1.DataValueField = "IDUsuario";
        ListUsuarios_1.DataBind();
    }

    protected void cmdAgregar_Click_d(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListDocumentos.GetSelectedIndices().Length; x++)
        {
            string IDTipoDocumento = ListDocumentos.Items[ListDocumentos.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListUsuarios_1.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListUsuarios_1.Items[ListUsuarios_1.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                objAreas.ExecuteInsertSP("Insertar_Permisos_Documentos", arrParam);
            }
        }
        CargaListaDoc_Usuarios();
        CargaListaUsuarios_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
    }
    protected void cmdBorrar_Click_d(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListDocumentos.GetSelectedIndices().Length; x++)
        {
            string IDTipoDocumento = ListDocumentos.Items[ListDocumentos.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListUsuariosDocumentos.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListUsuariosDocumentos.Items[ListUsuariosDocumentos.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
                arrParam.Add(new applyWeb.Data.Parametro("@IDUsuario", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));

                objAreas.ExecuteInsertSP("Eliminar_Permisos_Documentos", arrParam);
            }
        }
        CargaListaDoc_Usuarios();
        CargaListaUsuarios_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
    }

    protected void permisos()
    {
        
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=2 AND B.IDSubMenu=4 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            int IDprivilegio = dr.GetInt32(0);

            if (IDprivilegio == 12) { UpdatePanel.Visible = true; } //Permiso para Asignar campus
            else if (IDprivilegio == 13) { UpdatePanel1.Visible = true; } //Permiso para Asignar nivel
            else if (IDprivilegio == 14) { UpdatePanel2.Visible = true; }//Permiso para Asignar Documentos
            else if (IDprivilegio == 15) { Panel3.Visible = true; }//Permisos de aplicacion
            else
            {
                UpdatePanel.Visible = false;
                UpdatePanel1.Visible = false;
                UpdatePanel2.Visible = false;
                Panel3.Visible = false;
                
            }
        }
        ConexionSql.Close();
    }

    private void CargarPermisos_App()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

        string strQueryEsc = "SELECT * FROM ( " +
                             "SELECT DISTINCT IDPrivilegio,Menu Permiso,Descripcion FROM Permisos_App WHERE IDSubMenu = 1 AND IDPermiso = 1" +
                             "UNION " +
                             "SELECT DISTINCT IDPrivilegio,Sub_Menu Permiso,Descripcion FROM Permisos_App WHERE IDPrivilegio NOT IN(SELECT DISTINCT IDPrivilegio FROM Permisos_App WHERE IDSubMenu = 1 AND IDPermiso = 1) AND IDPermiso = 1 " +
                             "UNION " +
                             "SELECT DISTINCT IDPrivilegio,Permiso Permiso,Descripcion FROM Permisos_App WHERE IDPrivilegio NOT IN(SELECT DISTINCT IDPrivilegio FROM Permisos_App WHERE IDSubMenu = 1 AND IDPermiso = 1) AND IDPermiso<>1)A " +
                             "ORDER BY 1";

        ConexionSql.Open();
        SqlDataAdapter dataadapter = new SqlDataAdapter(strQueryEsc, ConexionSql);
        DataSet ds = new DataSet();
        dataadapter.Fill(ds, "Permisos_App");
        Permisos_App.DataSource = ds;
        Permisos_App.DataBind();
        Permisos_App.DataMember = "Permisos_App";

        ConexionSql.Close();
        style_grid();
        ocultar_perm_desplegables();
        

    }

    protected void style_grid()
    {
        Permisos_App.Rows[0].Cells[2].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        Permisos_App.Rows[1].Cells[2].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        Permisos_App.Rows[15].Cells[2].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        Permisos_App.Rows[20].Cells[2].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        Permisos_App.Rows[21].Cells[2].Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
        Permisos_App.Rows[2].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[6].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[10].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[16].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[17].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[18].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[19].Cells[2].Attributes["class"] += "ul_submenu";
        Permisos_App.Rows[3].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[4].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[5].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[7].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[8].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[9].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[11].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[12].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[13].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[14].Cells[2].Attributes["class"] += "ul_permiso";
        Permisos_App.Rows[0].Visible = false;
        Permisos_App.Rows[5].Visible = false;
    }
    protected void ocultar_img()
    {
        foreach (GridViewRow Rows in Permisos_App.Rows)
        {
            Rows.Cells[1].Attributes["class"] = "table-mod_2";
            if (Rows.RowIndex != 1 && Rows.RowIndex != 2 && Rows.RowIndex != 6 && Rows.RowIndex != 10 && Rows.RowIndex != 15)
            {
                LinkButton img = Rows.FindControl("MenuP") as LinkButton;
                Rows.Cells[1].Controls.Remove(img);
            }

        }
    }

    protected void Permisos_App_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Expand")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Permisos_App.Rows[index];
            int id = 0;
            int id_1 = 0;
            int id_2 = 0;
            int id_3 = 0;

            if (row.Cells[0].Text == "2" || row.Cells[0].Text == "3" || row.Cells[0].Text == "7" || row.Cells[0].Text == "11" || row.Cells[0].Text == "16")
            {

                if (row.Cells[0].Text == "2")
                {

                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    LinkButton img = row.FindControl("MenuP") as LinkButton;
                    img.Visible = false;
                    LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                    img_2.Visible = true;

                }
                else if (row.Cells[0].Text == "3")
                {

                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[3].Visible = true;
                    Permisos_App.Rows[4].Visible = true;
                    //Permisos_App.Rows[5].Visible = true;
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    LinkButton img = row.FindControl("MenuP") as LinkButton;
                    img.Visible = false;
                    LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                    img_2.Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;
                }
                else if (row.Cells[0].Text == "7")
                {

                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    Permisos_App.Rows[7].Visible = true;
                    Permisos_App.Rows[8].Visible = true;
                    Permisos_App.Rows[9].Visible = true;
                    LinkButton img = row.FindControl("MenuP") as LinkButton;
                    img.Visible = false;
                    LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                    img_2.Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;

                }

                else if (row.Cells[0].Text == "11")
                {
                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    Permisos_App.Rows[11].Visible = true;
                    Permisos_App.Rows[12].Visible = true;
                    Permisos_App.Rows[13].Visible = true;
                    Permisos_App.Rows[14].Visible = true;
                    LinkButton img = row.FindControl("MenuP") as LinkButton;
                    img.Visible = false;
                    LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                    img_2.Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;

                }
                else if (row.Cells[0].Text == "16")
                {
                    id = (index + 1);
                    id_1 = id + 1;
                    id_2 = id_1 + 1;
                    id_3 = id_2 + 1;
                    Permisos_App.Rows[id].Visible = true;
                    Permisos_App.Rows[id_1].Visible = true;
                    Permisos_App.Rows[id_2].Visible = true;
                    Permisos_App.Rows[id_3].Visible = true;
                    LinkButton img = row.FindControl("MenuP") as LinkButton;
                    img.Visible = false;
                    LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                    img_2.Visible = true;
                }

                //LinkButton img = row.FindControl("MenuP") as LinkButton;
                //img.Visible = false;
                //LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                //img_2.Visible = true;

            }
        }
        else if (e.CommandName == "Collapse")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Permisos_App.Rows[index];
            int id = 0;
            int id_1 = 0;
            int id_2 = 0;
            int id_3 = 0;

            if (row.Cells[0].Text == "2" || row.Cells[0].Text == "3" || row.Cells[0].Text == "7" || row.Cells[0].Text == "11" || row.Cells[0].Text == "16")
            {

                if (row.Cells[0].Text == "2")
                {
                    Permisos_App.Rows[2].Visible = false;
                    Permisos_App.Rows[3].Visible = false;
                    Permisos_App.Rows[4].Visible = false;
                    //Permisos_App.Rows[5].Visible = false;
                    Permisos_App.Rows[6].Visible = false;
                    Permisos_App.Rows[7].Visible = false;
                    Permisos_App.Rows[8].Visible = false;
                    Permisos_App.Rows[9].Visible = false;
                    Permisos_App.Rows[10].Visible = false;
                    Permisos_App.Rows[11].Visible = false;
                    Permisos_App.Rows[12].Visible = false;
                    Permisos_App.Rows[13].Visible = false;
                    Permisos_App.Rows[14].Visible = false;

                }
                else if (row.Cells[0].Text == "3")
                {
                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[3].Visible = false;
                    Permisos_App.Rows[4].Visible = false;
                    //Permisos_App.Rows[5].Visible = false;
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;

                }
                else if (row.Cells[0].Text == "7")
                {
                    Permisos_App.Rows[6].Visible = true;
                    Permisos_App.Rows[7].Visible = false;
                    Permisos_App.Rows[8].Visible = false;
                    Permisos_App.Rows[9].Visible = false;
                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[10].Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;
                }
                else if (row.Cells[0].Text == "11")
                {
                    Permisos_App.Rows[10].Visible = true;
                    Permisos_App.Rows[11].Visible = false;
                    Permisos_App.Rows[12].Visible = false;
                    Permisos_App.Rows[13].Visible = false;
                    Permisos_App.Rows[14].Visible = false;
                    Permisos_App.Rows[2].Visible = true;
                    Permisos_App.Rows[6].Visible = true;
                    LinkButton img_3 = Permisos_App.Rows[1].FindControl("MenuP2") as LinkButton;
                    img_3.Visible = true;
                    LinkButton img_4 = Permisos_App.Rows[1].FindControl("MenuP") as LinkButton;
                    img_4.Visible = false;

                }
                else if (row.Cells[0].Text == "16")
                {
                    id = (index + 1);
                    id_1 = id + 1;
                    id_2 = id_1 + 1;
                    id_3 = id_2 + 1;
                    Permisos_App.Rows[id].Visible = false;
                    Permisos_App.Rows[id_1].Visible = false;
                    Permisos_App.Rows[id_2].Visible = false;
                    Permisos_App.Rows[id_3].Visible = false;

                }
                LinkButton img = row.FindControl("MenuP") as LinkButton;
                img.Visible = true;
                LinkButton img_2 = row.FindControl("MenuP2") as LinkButton;
                img_2.Visible = false;

            }
        }
    }
    protected void ocultar_perm_desplegables()
    {
        Permisos_App.Rows[1].Visible = true;
        Permisos_App.Rows[2].Visible = false;
        Permisos_App.Rows[3].Visible = false;
        Permisos_App.Rows[4].Visible = false;
        Permisos_App.Rows[5].Visible = false;
        Permisos_App.Rows[6].Visible = false;
        Permisos_App.Rows[7].Visible = false;
        Permisos_App.Rows[8].Visible = false;
        Permisos_App.Rows[9].Visible = false;
        Permisos_App.Rows[10].Visible = false;
        Permisos_App.Rows[11].Visible = false;
        Permisos_App.Rows[12].Visible = false;
        Permisos_App.Rows[13].Visible = false;
        Permisos_App.Rows[14].Visible = false;
        Permisos_App.Rows[16].Visible = false;
        Permisos_App.Rows[17].Visible = false;
        Permisos_App.Rows[18].Visible = false;
        Permisos_App.Rows[19].Visible = false;

    }

    protected void consulta_permisos_user()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string IDRol = ddlRol.SelectedItem.Value.ToString();

        string strQuery = "SELECT DISTINCT IDRol,CAST (IDPrivilegio AS VARCHAR)IDPrivilegio,CASE WHEN IDRol IS NULL THEN 'N' ELSE 'Y' END ACTIVO " +
                          "FROM( " +
                          "SELECT DISTINCT A.IDRol, CAST(B.IDPrivilegio AS VARCHAR)IDPrivilegio " +
                          "FROM(SELECT IDPrivilegio FROM Permisos_App) B " +
                          "LEFT JOIN Permisos_App_Rol A ON A.IDPrivilegio = B.IDPrivilegio AND A.IDRol = '" + IDRol + "')C " +
                          "ORDER BY 2";
        SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
        SqlDataReader dr = cmd.ExecuteReader();


        while (dr.Read())
        {
            foreach (GridViewRow Rows in Permisos_App.Rows)

            {
                CheckBox Check_P = (CheckBox)Rows.FindControl("Checkbox_P");


                string row_1 = Rows.Cells[0].Text;
                string IDprivilegio = dr.GetString(1);
                string activo = dr.GetString(2);

                if (row_1 == IDprivilegio && activo == "Y")
                {
                    Check_P.Checked = true;
                }
                else if (row_1 == IDprivilegio && activo == "N")
                {
                    Check_P.Checked = false;
                }
                else if (row_1 == "1")
                {
                    Check_P.Checked = true;
                }else if(row_1 == "6")
                {
                    Check_P.Checked = false;
                }


            }


        }
        ConexionSql.Close();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {



        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string IDRol = ddlRol.SelectedItem.Value.ToString();

        foreach (GridViewRow row in Permisos_App.Rows)
        {

            CheckBox chkRow = (row.FindControl("Checkbox_P") as CheckBox);
            string IDPrivilegio = row.Cells[0].Text.ToString();
            if (!chkRow.Checked)
            {
                string strQueryValida = "SELECT DISTINCT COUNT(*) FROM Permisos_App_Rol A WHERE A.IDRol='" + IDRol + "' AND IDPrivilegio='" + IDPrivilegio + "'";
                SqlDataAdapter sqladapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(strQueryValida, ConexionSql);
                DataSet dssql1 = new DataSet();
                sqladapter.SelectCommand = cmd;
                sqladapter.Fill(dssql1);
                sqladapter.Dispose();
                cmd.Dispose();

                if (dssql1.Tables[0].Rows[0][0].ToString() != "0")
                {
                    //Insertar log de eliminación de permisos
                    ArrayList arrParametros = new ArrayList();
                    arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                    arrParametros.Add(new applyWeb.Data.Parametro("@IDPrivilegio", IDPrivilegio));
                    arrParametros.Add(new applyWeb.Data.Parametro("@UserLog", Session["User"].ToString()));
                    arrParametros.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                    DataSet dsUsuario_D = objAreas.ExecuteSP("Eliminar_Permisos_Rol", arrParametros);

                }
            }
            else
            {
                //Insertar permisos nuevos con su log en bd
                ArrayList arrParametros = new ArrayList();
                arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParametros.Add(new applyWeb.Data.Parametro("@IDPrivilegio", IDPrivilegio));
                arrParametros.Add(new applyWeb.Data.Parametro("@UserLog", Session["User"].ToString()));
                arrParametros.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                DataSet dsUsuario_D = objAreas.ExecuteSP("Insertar_Permisos_Rol", arrParametros);
            }
        }
        ClientScript.RegisterStartupScript(this.GetType(), "", "guardar_permisos()", true);

    }

    protected void Checkbox_P_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.Parent.Parent;
        CheckBox Configuracion = (Permisos_App.Rows[1].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Tipo_Documentos = (Permisos_App.Rows[2].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Usuarios = (Permisos_App.Rows[6].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Permisos_Roles = (Permisos_App.Rows[10].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Doc_sincroniza = (Permisos_App.Rows[3].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Doc_Edita = (Permisos_App.Rows[4].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Doc_Elimina = (Permisos_App.Rows[5].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox User_Agrega = (Permisos_App.Rows[7].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox User_Edita = (Permisos_App.Rows[8].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox User_Elimina = (Permisos_App.Rows[9].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Permisos_Campus = (Permisos_App.Rows[11].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Permisos_Nivel = (Permisos_App.Rows[12].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Permisos_Doc = (Permisos_App.Rows[13].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Permisos_app = (Permisos_App.Rows[14].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Administracion = (Permisos_App.Rows[15].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Exporta = (Permisos_App.Rows[16].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Completos = (Permisos_App.Rows[17].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Subir = (Permisos_App.Rows[18].Cells[0].FindControl("Checkbox_P") as CheckBox);
        CheckBox Revisar = (Permisos_App.Rows[19].Cells[0].FindControl("Checkbox_P") as CheckBox);

        if (chk.Checked)
        {

            if (row.RowIndex.ToString() == "1")
            {
                Tipo_Documentos.Checked = true;
                Usuarios.Checked = true;
                Permisos_Roles.Checked = true;
            }
            else if (row.RowIndex.ToString() == "2")
            {
                Configuracion.Checked = true;
                Doc_sincroniza.Checked = true;
                Doc_Edita.Checked = true;
                //Doc_Elimina.Checked = true;
            }
            else if (row.RowIndex.ToString() == "6")
            {
                Configuracion.Checked = true;
                User_Agrega.Checked = true;
                User_Edita.Checked = true;
                User_Elimina.Checked = true;
            }
            else if (row.RowIndex.ToString() == "10")
            {
                Configuracion.Checked = true;
                Permisos_app.Checked = true;
                Permisos_Campus.Checked = true;
                Permisos_Doc.Checked = true;
                Permisos_Nivel.Checked = true;
            }
            else if (row.RowIndex.ToString() == "15")
            {
                Exporta.Checked = true;
                Completos.Checked = true;
                Subir.Checked = true;
                Revisar.Checked = true;
            }
            else if (row.RowIndex.ToString() == "3" || row.RowIndex.ToString() == "4" || row.RowIndex.ToString() == "5")
            {
                Configuracion.Checked = true;
                Tipo_Documentos.Checked = true;

            }
            else if (row.RowIndex.ToString() == "7" || row.RowIndex.ToString() == "8" || row.RowIndex.ToString() == "9")
            {
                Configuracion.Checked = true;
                Usuarios.Checked = true;

            }
            else if (row.RowIndex.ToString() == "11" || row.RowIndex.ToString() == "12" || row.RowIndex.ToString() == "13" || row.RowIndex.ToString() == "14")
            {
                Configuracion.Checked = true;
                Permisos_Roles.Checked = true;

            }
            else if (row.RowIndex.ToString() == "16" || row.RowIndex.ToString() == "17" || row.RowIndex.ToString() == "18"|| row.RowIndex.ToString() == "19")
            {
                Administracion.Checked = true;


            }
        }
        else if (!chk.Checked)
        {

            if (row.RowIndex.ToString() == "1")
            {
                Tipo_Documentos.Checked = false;
                Usuarios.Checked = false;
                Permisos_Roles.Checked = false;
                Doc_sincroniza.Checked = false;
                Doc_Edita.Checked = false;
                //Doc_Elimina.Checked = false;
                User_Agrega.Checked = false;
                User_Edita.Checked = false;
                User_Elimina.Checked = false;
                Permisos_app.Checked = false;
                Permisos_Campus.Checked = false;
                Permisos_Doc.Checked = false;
                Permisos_Nivel.Checked = false;
            }
            else if (row.RowIndex.ToString() == "2" && !Usuarios.Checked && !Permisos_Roles.Checked)
            {
                Configuracion.Checked = false;
                Doc_sincroniza.Checked = false;
                Doc_Edita.Checked = false;
                //Doc_Elimina.Checked = false;
            }
            else if (row.RowIndex.ToString() == "2")
            {

                Doc_sincroniza.Checked = false;
                Doc_Edita.Checked = false;
                //Doc_Elimina.Checked = false;
            }
            else if (row.RowIndex.ToString() == "6" && !Permisos_Roles.Checked && !Tipo_Documentos.Checked)
            {
                Configuracion.Checked = false;
                User_Agrega.Checked = false;
                User_Edita.Checked = false;
                User_Elimina.Checked = false;
            }
            else if (row.RowIndex.ToString() == "6")
            {
                User_Agrega.Checked = false;
                User_Edita.Checked = false;
                User_Elimina.Checked = false;
            }
            else if (row.RowIndex.ToString() == "10" && !Tipo_Documentos.Checked && !Usuarios.Checked)
            {
                Configuracion.Checked = false;
                Permisos_app.Checked = false;
                Permisos_Campus.Checked = false;
                Permisos_Doc.Checked = false;
                Permisos_Nivel.Checked = false;
            }
            else if (row.RowIndex.ToString() == "10")
            {

                Permisos_app.Checked = false;
                Permisos_Campus.Checked = false;
                Permisos_Doc.Checked = false;
                Permisos_Nivel.Checked = false;
            }

            else if (row.RowIndex.ToString() == "15")
            {
                Exporta.Checked = false;
                Completos.Checked = false;
                Subir.Checked = false;
                Revisar.Checked = false;
            }

        }
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_campus.Checked)
        {
            foreach(ListItem item in lstCampus.Items)
            {
                ListItem selecteditem = lstCampus.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = true;
                    
                    lstCampus.Enabled = false;
                }
                CargaListaUsuarios_C();
                CargaListaCampus_Usuarios();
            }
            
        }
        else
        {
            foreach (ListItem item in lstCampus.Items)
            {
                ListItem selecteditem = lstCampus.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = false;
                    lstCampus.Enabled = true;
                    
                }
            }
            lstUsuariosCampus.Items.Clear();
            lstUsuarios.Items.Clear();
        }
    }

   
    protected void CheckBox_Niveles_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Niveles.Checked)
        {
            foreach(ListItem item in ListNiveles.Items)
            {
                ListItem selecteditem = ListNiveles.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = true;
                    ListNiveles.Enabled = false;
                }
                CargaListaNiveles_Usuarios();
                CargaListaUsuarios_N();
            }
        }
        else
        {
            foreach (ListItem item in ListNiveles.Items)
            {
                ListItem selecteditem = ListNiveles.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = false;
                    ListNiveles.Enabled = true;
                    
                }
            }
            ListUsuariosNivel.Items.Clear();
            ListUsuarios.Items.Clear();
        }
    }
    protected void CheckBox_Doc_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Doc.Checked)
        {
            foreach(ListItem item in ListDocumentos.Items)
            {
                ListItem selecteditem = ListDocumentos.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = true;
                    ListDocumentos.Enabled = false;
                }
                CargaListaDoc_Usuarios();
                CargaListaUsuarios_D();
            }
        }
        else
        {
            foreach (ListItem item in ListDocumentos.Items)
            {
                ListItem selecteditem = ListDocumentos.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = false;
                    ListDocumentos.Enabled = true;
                }
            }
            ListUsuariosDocumentos.Items.Clear();
            ListUsuarios_1.Items.Clear();
        }

    }

    protected void CargaListaDocumentos_estatus()
    {
        ArrayList arrParametros = new ArrayList();
        DataSet dsDocumentos = objAreas.ExecuteSP("ObtenerListaDocumentos", arrParametros);
        ListDocs.DataSource = dsDocumentos;
        ListDocs.DataTextField = "Nombre";
        ListDocs.DataValueField = "IDTipoDocumento";
        ListDocs.DataBind();
    }
    protected void ListDocs_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargaListaDoc_Estatus();
        CargaListaEstatus_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
        lstUsuariosCampus.Items.Clear();
        lstUsuarios.Items.Clear();
        lstCampus.ClearSelection();
        ListUsuariosNivel.Items.Clear();
        ListUsuarios.Items.Clear();
        ListNiveles.ClearSelection();
        ListUsuariosDocumentos.Items.Clear();
        ListUsuarios_1.Items.Clear();
        ListDocumentos.ClearSelection();
        ListDocs.Focus();

    }

    protected void CheckBox1_CheckedChanged1(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            foreach (ListItem item in ListDocs.Items)
            {
                ListItem selecteditem = ListDocs.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = true;
                    ListDocs.Enabled = false;
                }
                CargaListaDoc_Estatus();
                CargaListaEstatus_D();
            }
        }
        else
        {
            foreach (ListItem item in ListDocs.Items)
            {
                ListItem selecteditem = ListDocs.Items.FindByValue(item.Value);
                if (selecteditem != null)
                {
                    selecteditem.Selected = false;
                    ListDocs.Enabled = true;
                }
            }
            ListDocE.Items.Clear();
            ListEstatus.Items.Clear();
        }
    }

    protected void CargaListaDoc_Estatus()
    {

        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDTipoDocumento = ListDocs.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
        DataSet dsUsuariosDoc = objAreas.ExecuteSP("ObtenerDocumentosEstatus", arrParametros);
        ListDocE.DataSource = dsUsuariosDoc;
        ListDocE.DataTextField = "Nombre";
        ListDocE.DataValueField = "IDEstatus";
        ListDocE.DataBind();
    }
    protected void CargaListaEstatus_D()
    {
        string IDRol = ddlRol.SelectedItem.Value.ToString();
        string IDTipoDocumento = ListDocs.SelectedItem.Value.ToString();
        ArrayList arrParametros = new ArrayList();
        arrParametros.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
        arrParametros.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
        DataSet dsUsuario_D = objAreas.ExecuteSP("Obtener_ListaEstatus_D", arrParametros);
        ListEstatus.DataSource = dsUsuario_D;
        ListEstatus.DataTextField = "Nombre";
        ListEstatus.DataValueField = "IDEstatus";
        ListEstatus.DataBind();
    }
    protected void cmAgregarEstatus_Click(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListDocs.GetSelectedIndices().Length; x++)
        {
            string IDTipoDocumento = ListDocs.Items[ListDocs.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListEstatus.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListEstatus.Items[ListEstatus.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
                arrParam.Add(new applyWeb.Data.Parametro("@IDEstatus", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));
                objAreas.ExecuteInsertSP("Insertar_Permisos_Documentos_Estatus", arrParam);
            }
        }
        CargaListaDoc_Estatus();
        CargaListaEstatus_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
    }

    protected void cmBorrarEstatus_Click(object sender, ImageClickEventArgs e)
    {
        for (int x = 0; x < ListDocs.GetSelectedIndices().Length; x++)
        {
            string IDTipoDocumento = ListDocs.Items[ListDocs.GetSelectedIndices()[x]].Value;

            for (int i = 0; i < ListDocE.GetSelectedIndices().Length; i++)
            {
                ArrayList arrParam = new ArrayList();
                string IDUsuario = ListDocE.Items[ListDocE.GetSelectedIndices()[i]].Value;
                string IDRol = ddlRol.SelectedItem.Value.ToString();
                string user_log = Session["user"].ToString();

                arrParam.Add(new applyWeb.Data.Parametro("@IDTipoDocumento", IDTipoDocumento));
                arrParam.Add(new applyWeb.Data.Parametro("@IDEstatus", IDUsuario));
                arrParam.Add(new applyWeb.Data.Parametro("@IDRol", IDRol));
                arrParam.Add(new applyWeb.Data.Parametro("@UserLog", user_log));
                arrParam.Add(new applyWeb.Data.Parametro("@fecha_mod", DateTime.Now.ToString()));

                objAreas.ExecuteInsertSP("Eliminar_Permisos_Documentos_Estatus", arrParam);
            }
        }
        CargaListaDoc_Estatus();
        CargaListaEstatus_D();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "slide_down_fast_d();", true);
    }
}