using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;

public partial class Tipodocumentos : System.Web.UI.Page
{
    applyWeb.Data.Data objDocumentos = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
    public bool permiso_editar = false;
    public bool permiso_eliminar = false;
    protected void Page_Load(object sender, EventArgs e)
    {
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
        {

            permisos();
        }
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            Session["Reset"] = true;
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            int timeout = ((int)section.Timeout.TotalMinutes-3) * 1000 * 60;
            ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);
            ObtenerListaTipoDocumento();
            permisos();

        }
        
        search.Attributes.Add("placeholder", "Buscar...");
    }

    protected void ObtenerListaTipoDocumento()
    {
        ArrayList arrP = new ArrayList();
        DataSet dsDocumentos = (DataSet)objDocumentos.ExecuteSP("Obter_ListaTipoDocumento", arrP);
        gvTipoDocumento.DataSource = dsDocumentos;
        gvTipoDocumento.DataBind();
    }
    protected void Sincronizar_doc_Click(object sender, EventArgs e)
    {
        //insert_update();
        //sincroniza_niveles();
        //sincroniza_tipo_alumno();
        //sincroniza_campus();
        //sincroniza_relacion_doc_nivel();
        ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Sincronización Completada','La sincronización de datos se realizo con éxito', 'success')</script>");

        this.Response.AddHeader("REFRESH", "2;URL=Tipodocumentos.aspx");
    }



    //protected void insert_update()
    //{

    //    string strQuery = "";
    //    strQuery = "SELECT DISTINCT SARCHKB_ADMR_CODE,STVADMR_DESC,'JPG,JPEG,PDF' FORMATO,'1' FORZOSO, ' ' DESCRIP, '1' MIN, '4000' MAX " +
    //               "FROM SARCHKB " +
    //               "LEFT JOIN STVADMR ON STVADMR_CODE = SARCHKB_ADMR_CODE " +
    //               "WHERE SARCHKB_LEVL_CODE<>'0' AND SARCHKB_STYP_CODE IS NOT NULL AND SARCHKB_RESD_CODE IS NOT NULL ORDER BY 1 ";

    //    var ConexionBanner = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionNOAH"].ConnectionString);
    //    OracleDataReader DatosBanner;
    //    var ConsultaOracle = new OracleCommand();
    //    ConexionBanner.Open();
    //    ConsultaOracle.Connection = ConexionBanner;
    //    ConsultaOracle.CommandType = CommandType.Text;
    //    ConsultaOracle.CommandText = strQuery;
    //    DatosBanner = ConsultaOracle.ExecuteReader();
    //    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

    //    ConexionSql.Open();

    //    while (DatosBanner.Read())
    //    {
    //        string id_banner = DatosBanner.GetString(0);
    //        string Nombre = DatosBanner.GetString(1);
    //        string Descripcion = DatosBanner.GetString(4);
    //        string Formato = DatosBanner.GetString(2);
    //        string forzoso = DatosBanner.GetString(3);
    //        string min = DatosBanner.GetString(5);
    //        string max = DatosBanner.GetString(6);

    //        SqlCommand cmd = new SqlCommand("SP_Documentos", ConexionSql);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@id_banner", id_banner);
    //        cmd.Parameters.AddWithValue("@Nombre", Nombre);
    //        cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
    //        cmd.Parameters.AddWithValue("@TamanoMinimo", Convert.ToInt32(min));
    //        cmd.Parameters.AddWithValue("@TamanoMaximo", Convert.ToInt32(max));
    //        cmd.Parameters.AddWithValue("@Formato", Formato);
    //        //cmd.Parameters.AddWithValue("@Nivel", Nivel);
    //        //cmd.Parameters.AddWithValue("@tipo_alumno", tipo_alumno);
    //        //cmd.Parameters.AddWithValue("@residencia", residencia);
    //        cmd.Parameters.AddWithValue("@forzoso", Convert.ToInt32(forzoso));
    //        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

    //        cmd.ExecuteNonQuery();
    //    }
    //    ConexionSql.Close();
    //}

    //protected void sincroniza_niveles()
    //{
    //    string strQuery = "";
    //    strQuery = "SELECT DISTINCT STVLEVL_CODE,STVLEVL_DESC,AREA " +
    //                "FROM STVLEVL " +
    //                "INNER JOIN(SELECT DISTINCT SMBPGEN_PROGRAM, SMRPRLE_LEVL_CODE, SMRPRLE_DEGC_CODE, CASE SMRPRLE_DEGC_CODE  WHEN  'BACHIL' THEN '1' WHEN 'LICENC' THEN '2' WHEN 'MAESTR' THEN '3'  ELSE '4' END AREA " +
    //                "FROM SMRPRLE " +
    //                "INNER JOIN SMBPGEN ON SMBPGEN_PROGRAM = SMRPRLE_PROGRAM AND SMBPGEN_ACTIVE_IND = 'Y' " +
    //                "WHERE SMRPRLE_PROGRAM NOT IN('DHU022003E','ORT042017E','MAD292016M')) ON SMRPRLE_LEVL_CODE = STVLEVL_CODE " +
    //                "ORDER BY 1 ";

    //    var ConexionBanner = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionNOAH"].ConnectionString);
    //    OracleDataReader DatosBanner;
    //    var ConsultaOracle = new OracleCommand();
    //    ConexionBanner.Open();
    //    ConsultaOracle.Connection = ConexionBanner;
    //    ConsultaOracle.CommandType = CommandType.Text;
    //    ConsultaOracle.CommandText = strQuery;
    //    DatosBanner = ConsultaOracle.ExecuteReader();
    //    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

    //    ConexionSql.Open();

    //    while (DatosBanner.Read())
    //    {
    //        string Codigo = DatosBanner.GetString(0);
    //        string Descripcion = DatosBanner.GetString(1);
    //        string Area = DatosBanner.GetString(2);

    //        SqlCommand cmd = new SqlCommand("SP_sinc_niveles", ConexionSql);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@Codigo", Codigo);
    //        cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
    //        cmd.Parameters.AddWithValue("@IDArea", Convert.ToInt32(Area));
    //        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

    //        cmd.ExecuteNonQuery();
    //    }
    //    ConexionSql.Close();
    //}

    //protected void sincroniza_tipo_alumno()
    //{
    //    string strQuery = "";
    //    strQuery = "SELECT STVSTYP_CODE,STVSTYP_DESC FROM STVSTYP ORDER BY 1";

    //    var ConexionBanner = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionNOAH"].ConnectionString);
    //    OracleDataReader DatosBanner;
    //    var ConsultaOracle = new OracleCommand();
    //    ConexionBanner.Open();
    //    ConsultaOracle.Connection = ConexionBanner;
    //    ConsultaOracle.CommandType = CommandType.Text;
    //    ConsultaOracle.CommandText = strQuery;
    //    DatosBanner = ConsultaOracle.ExecuteReader();
    //    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

    //    ConexionSql.Open();

    //    while (DatosBanner.Read())
    //    {
    //        string Codigo = DatosBanner.GetString(0);
    //        string Descripcion = DatosBanner.GetString(1);

    //        SqlCommand cmd = new SqlCommand("SP_sinc_tipo_alu", ConexionSql);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@Codigo", Codigo);
    //        cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
    //        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

    //        cmd.ExecuteNonQuery();
    //    }
    //    ConexionSql.Close();
    //}

    //protected void sincroniza_campus()
    //{
    //    string strQuery = "";
    //    strQuery = "SELECT STVCAMP_CODE,STVCAMP_DESC FROM STVCAMP WHERE STVCAMP_CODE NOT LIKE 'C%' AND STVCAMP_CODE<>'XX' ORDER BY 1";

    //    var ConexionBanner = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionNOAH"].ConnectionString);
    //    OracleDataReader DatosBanner;
    //    var ConsultaOracle = new OracleCommand();
    //    ConexionBanner.Open();
    //    ConsultaOracle.Connection = ConexionBanner;
    //    ConsultaOracle.CommandType = CommandType.Text;
    //    ConsultaOracle.CommandText = strQuery;
    //    DatosBanner = ConsultaOracle.ExecuteReader();
    //    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

    //    ConexionSql.Open();

    //    while (DatosBanner.Read())
    //    {
    //        string Codigo = DatosBanner.GetString(0);
    //        string Descripcion = DatosBanner.GetString(1);

    //        SqlCommand cmd = new SqlCommand("Insertar_Campus", ConexionSql);
    //        cmd.CommandType = CommandType.StoredProcedure;

    //        cmd.Parameters.AddWithValue("@Codigo", Codigo);
    //        cmd.Parameters.AddWithValue("@Nombre", Descripcion);


    //        cmd.ExecuteNonQuery();
    //    }
    //    ConexionSql.Close();
    //}


    //static void sincroniza_relacion_doc_nivel()
    //{
    //    string strQuery = "";
    //    strQuery = "SELECT DISTINCT CASE SMRPRLE_DEGC_CODE  WHEN  'BACHIL' THEN '1' WHEN 'LICENC' THEN '2' WHEN 'MAESTR' THEN '3'  ELSE '4' END IDArea,SARCHKB_RESD_CODE IDProcedencia,SARCHKB_STYP_CODE IDTipoIngreso,CASE WHEN SMRPRLE_CAMP_CODE IN ('09','10')THEN '3' ELSE '2' END IDModalidad,SARCHKB_ADMR_CODE,SARCHKB_LEVL_CODE " +
    //                "FROM SARCHKB " +
    //                "INNER JOIN SMRPRLE ON SMRPRLE_LEVL_CODE=SARCHKB_LEVL_CODE " +
    //                "INNER JOIN SMBPGEN ON SMBPGEN_PROGRAM=SMRPRLE_PROGRAM " +
    //                "WHERE SARCHKB_LEVL_CODE<>'0' AND SARCHKB_STYP_CODE IS NOT NULL AND SARCHKB_RESD_CODE IS NOT NULL AND SMBPGEN_ACTIVE_IND='Y' " +
    //                "AND SMRPRLE_PROGRAM NOT IN ('MAD292016M','ORT042017E') ";

    //    var ConexionBanner = new OracleConnection(ConfigurationManager.ConnectionStrings["ConexionNOAH"].ConnectionString);
    //    OracleDataReader DatosBanner;
    //    var ConsultaOracle = new OracleCommand();
    //    ConexionBanner.Open();
    //    ConsultaOracle.Connection = ConexionBanner;
    //    ConsultaOracle.CommandType = CommandType.Text;
    //    ConsultaOracle.CommandText = strQuery;
    //    DatosBanner = ConsultaOracle.ExecuteReader();

    //    SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);

    //    ConexionSql.Open();

    //    while (DatosBanner.Read())
    //    {
    //        string IDArea = DatosBanner.GetString(0);
    //        string IDProcedencia = DatosBanner.GetString(1);
    //        string IDTipoIngreso = DatosBanner.GetString(2);
    //        string IDModalidad = DatosBanner.GetString(3);
    //        string Id_Banner = DatosBanner.GetString(4);
    //        string IDNivel = DatosBanner.GetString(5);

    //        string strQueryok = "SELECT IDTipoDocumento FROM TipoDocumento WHERE id_banner='" + Id_Banner + "'";
    //        SqlDataAdapter sqladapter = new SqlDataAdapter();
    //        DataSet dssql = new DataSet();
    //        SqlCommand commandsql = new SqlCommand(strQueryok, ConexionSql);
    //        sqladapter.SelectCommand = commandsql;
    //        sqladapter.Fill(dssql);
    //        sqladapter.Dispose();
    //        commandsql.Dispose();
    //        string IDTipoDocumento = dssql.Tables[0].Rows[0][0].ToString();

    //        string strQueryvalida = "SELECT COUNT (*) FROM TiposDocumento_Area WHERE IDArea='" + IDArea + "' AND IDTipoDocumento='" + IDTipoDocumento + "' AND IDProcedencia='" + IDProcedencia + "' AND IDTipoIngreso='" + IDTipoIngreso + "' AND IDModalidad='" + IDModalidad + "' AND IDNivel='" + IDNivel + "'";
    //        SqlDataAdapter sqladapter_1 = new SqlDataAdapter();
    //        DataSet dssql_1 = new DataSet();
    //        SqlCommand commandsql_1 = new SqlCommand(strQueryvalida, ConexionSql);
    //        sqladapter_1.SelectCommand = commandsql_1;
    //        sqladapter_1.Fill(dssql_1);
    //        sqladapter_1.Dispose();
    //        commandsql_1.Dispose();

    //        if (dssql_1.Tables[0].Rows[0][0].ToString() == "0")
    //        {
    //            string strquery_insert = "INSERT INTO TiposDocumento_Area VALUES ('" + IDArea + "','" + IDTipoDocumento + "','" + IDProcedencia + "','" + IDTipoIngreso + "','" + IDModalidad + "','" + IDNivel + "','" + DateTime.Now.ToString() + "')";
    //            SqlCommand myCommandinserta = new SqlCommand(strquery_insert, ConexionSql);
    //            myCommandinserta.ExecuteNonQuery();
    //        }




    //    }
    //    ConexionSql.Close();
    //}


    protected void permisos()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=2 AND B.IDSubMenu=2 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            int IDprivilegio = dr.GetInt32(0);

            if (IDprivilegio == 4) { Sincronizar_doc.Visible = true; } //Permiso para Sincronizar Documentos
            else if (IDprivilegio == 5) { permiso_editar = true; } //Permiso para Editar Documentos
            else if (IDprivilegio == 6) { permiso_eliminar = true; }//Permiso para Eliminar Documentos
            else
            {
                Sincronizar_doc.Visible = false;
                permiso_editar = false;
                permiso_eliminar = false;
            }


        }
        ConexionSql.Close();
    }


    protected void gvTipoDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.PageIndex = e.NewPageIndex;
        ObtenerListaTipoDocumento();
    }

    protected void search_TextChanged(object sender, EventArgs e)
    {
        ArrayList arrP = new ArrayList();
        arrP.Add(new applyWeb.Data.Parametro("@text",search.Text.Trim()));
        DataSet dsDocumentos = (DataSet)objDocumentos.ExecuteSP("Obter_ListaTipoDocumento_search", arrP);
        gvTipoDocumento.DataSource = dsDocumentos;
        gvTipoDocumento.DataBind();
        
    }


    protected void gvTipoDocumento_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTipoDocumento.EditIndex = e.NewEditIndex;
        ObtenerListaTipoDocumento();
    }

    protected void gvTipoDocumento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        e.Cancel = true;
        gvTipoDocumento.EditIndex = -1;
        ObtenerListaTipoDocumento();
    }

    protected void gvTipoDocumento_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvTipoDocumento.Rows[e.RowIndex];
        //TextBox txtIDTipoDocumento = (TextBox)row.FindControl("txtIDTipoDocumento");
        TextBox txtDocumento = (TextBox)row.FindControl("txtDocumento");
        TextBox txtDescripcion = (TextBox)row.FindControl("txtDescripcion");
        TextBox txtTamanoMinimo = (TextBox)row.FindControl("txtTamanoMinimo");
        TextBox txtTamanoMaximo = (TextBox)row.FindControl("txtTamanoMaximo");
        TextBox txtFormato = (TextBox)row.FindControl("txtFormato");
        CheckBox chkForzoso = (CheckBox)row.FindControl("chk1");
        int IDTipoDocumento = Int32.Parse(gvTipoDocumento.DataKeys[e.RowIndex].Value.ToString());
        string Documento = txtDocumento.Text;
        string Descripcion = txtDescripcion.Text;
        string Formato = txtFormato.Text;
        string Forzoso = null;
        string TamanoMinimo = null;
        string TamanoMaximo = null;

        if (chkForzoso.Checked)
        {
            Forzoso = "1";
        }
        else { Forzoso = "0"; }
        if (!IsNumeric(txtTamanoMinimo.Text) && !IsNumeric(txtTamanoMaximo.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Datos Invalidos','Los datos del tamaño del archivo deben ser solo numeros.', 'error')</script>");
            
        }
        else if (IsNumeric(txtTamanoMinimo.Text) && !IsNumeric(txtTamanoMaximo.Text))
        {
             TamanoMinimo = txtTamanoMinimo.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Datos Invalidos','Los datos del tamaño máximo del archivo deben ser solo numeros.', 'error')</script>");
        }
        else if (!IsNumeric(txtTamanoMinimo.Text) && IsNumeric(txtTamanoMaximo.Text))
        {
             TamanoMaximo = txtTamanoMaximo.Text;
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Datos Invalidos','Los datos del tamaño mínimo del archivo deben ser solo numeros.', 'error')</script>");
        }
        else if (IsNumeric(txtTamanoMinimo.Text) && IsNumeric(txtTamanoMaximo.Text))
        {
             TamanoMinimo = txtTamanoMinimo.Text;
             TamanoMaximo = txtTamanoMaximo.Text;
            UpdateTipoDocumento(IDTipoDocumento,Documento,Descripcion,TamanoMinimo,TamanoMaximo,Formato,Forzoso);
            ObtenerListaTipoDocumento();
        }
        
    }

    private void UpdateTipoDocumento(int IDTipoDocumento, string Documento, string Descripcion, string TamanoMinimo, string TamanoMaximo,string Formato,string Forzoso )
    {
        SqlConnection ConexionSql =new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        string query = "UPDATE TipoDocumento SET Nombre = @Documento, Descripcion = @Descripcion, TamanoMinimo = @TamanoMinimo, TamanoMaximo = @TamnoMaximo, Formato = @Formato, Forzoso = @Forzoso WHERE IDTipoDocumento = @IDTipoDocumento";

        
        SqlCommand com = new SqlCommand(query, ConexionSql);
        com.Parameters.Add("@IDTIpoDocumento", SqlDbType.Int).Value = IDTipoDocumento;
        com.Parameters.Add("@Documento", SqlDbType.VarChar).Value = Documento;
        com.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion;
        com.Parameters.Add("@TamanoMinimo", SqlDbType.VarChar).Value = TamanoMinimo;
        com.Parameters.Add("@TamnoMaximo", SqlDbType.VarChar).Value = TamanoMaximo;
        com.Parameters.Add("@Formato", SqlDbType.VarChar).Value = Formato;
        com.Parameters.Add("@Forzoso", SqlDbType.VarChar).Value = Forzoso;

        ConexionSql.Open();
        com.ExecuteNonQuery();
        ConexionSql.Close();

        gvTipoDocumento.EditIndex = -1;
        gvTipoDocumento.DataBind();
    }
    public bool IsNumeric(string value)
    {
        return value.All(char.IsNumber);
    }

    protected void gvTipoDocumento_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        ObtenerListaTipoDocumento();
    }

    protected void gvTipoDocumento_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = gvTipoDocumento.Rows[e.RowIndex];
        
        int IDTipoDocumento = Int32.Parse(gvTipoDocumento.DataKeys[e.RowIndex].Value.ToString());
        string Documento = row.Cells[1].Text;
        try
        {
            DeleteTipoDocumento(IDTipoDocumento, Documento);
        }
        catch(Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Error al Eliminar','El documento no se puede eliminar por la relación con otros datos en el sistema', 'error')</script>");
        }
        ObtenerListaTipoDocumento();
        

    }
    private void DeleteTipoDocumento(int IDTipoDocumento,string Documento)
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlConnectionString"].ConnectionString);
        string query = "DELETE FROM TipoDocumento WHERE IDTipoDocumento = @IDTipoDocumento";
        string query_log = "INSERT INTO Logs_Permisos (Proceso,Descripcion) VALUES ('TipoDocumento','El usuario " + Session["user"].ToString() + "elimino el documento: " + Documento + "')";
        SqlCommand com = new SqlCommand(query, ConexionSql);
        com.Parameters.Add("@IDTIpoDocumento", SqlDbType.Int).Value = IDTipoDocumento;
        SqlCommand com_1 = new SqlCommand(query_log, ConexionSql);
        ConexionSql.Open();
        com_1.ExecuteNonQuery();
        com.ExecuteNonQuery();
        ConexionSql.Close();

        gvTipoDocumento.EditIndex = -1;
        gvTipoDocumento.DataBind();
    }
}