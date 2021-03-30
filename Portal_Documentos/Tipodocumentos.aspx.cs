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
using MySql.Data.MySqlClient;

public partial class Tipodocumentos : System.Web.UI.Page
{
    applyWeb.Data.Data objDocumentos = new applyWeb.Data.Data(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
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
        DataSet dsDocumentos = (DataSet)objDocumentos.ExecuteSP("Obtener_ListaTipoDocumento", arrP);
        gvTipoDocumento.DataSource = dsDocumentos;
        gvTipoDocumento.DataBind();
    }
    protected void Sincronizar_doc_Click(object sender, EventArgs e)
    {
        insert_update();
        sincroniza_niveles();
        sincroniza_tipo_alumno();
        sincroniza_campus();
        sincroniza_relacion_doc_nivel();
        ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Sincronización Completada','La sincronización de datos se realizo con éxito', 'success')</script>");

        this.Response.AddHeader("REFRESH", "2;URL=Tipodocumentos.aspx");
    }



    protected void insert_update()
    {

        string strQuery = "";
        strQuery = "SELECT DISTINCT TDOCU_CLAVE,TDOCU_DESC ,'JPG,JPEG,PDF' FORMATO,'1' FORZOSO, ' ' DESCRIP, '1' MIN, '4000' MAX FROM TDOCU WHERE TDOCU_ESTATUS='A' ORDER BY 1";

        MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        MySqlDataReader data_saes;
        MySqlCommand consulta_saes = new MySqlCommand();
        ConexionMySql_saes.Open();
        consulta_saes.Connection = ConexionMySql_saes;
        consulta_saes.CommandType = CommandType.Text;
        consulta_saes.CommandText = strQuery;

        data_saes = consulta_saes.ExecuteReader();


        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        ConexionMySql.Open();

        while (data_saes.Read())
        {
            string id_saes = data_saes.GetString(0);
            string Nombre = data_saes.GetString(1);
            string Descripcion = data_saes.GetString(4);
            string Formato = data_saes.GetString(2);
            string forzoso = data_saes.GetString(3);
            string min = data_saes.GetString(5);
            string max = data_saes.GetString(6);

            MySqlCommand cmd = new MySqlCommand("SP_Documentos", ConexionMySql);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id_saes_in", id_saes);
            cmd.Parameters.AddWithValue("@Nombre_in", Nombre);
            cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
            cmd.Parameters.AddWithValue("@TamanoMinimo_in", Convert.ToInt32(min));
            cmd.Parameters.AddWithValue("@TamanoMaximo_in", Convert.ToInt32(max));
            cmd.Parameters.AddWithValue("@Formato_in", Formato);
            //cmd.Parameters.AddWithValue("@Nivel", Nivel);
            //cmd.Parameters.AddWithValue("@tipo_alumno", tipo_alumno);
            //cmd.Parameters.AddWithValue("@residencia", residencia);
            cmd.Parameters.AddWithValue("@forzoso_in", Convert.ToInt32(forzoso));
            cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
        ConexionMySql.Close();
        ConexionMySql_saes.Close();
    }

    protected void sincroniza_niveles()
    {
        string strQuery = "";
        strQuery = "SELECT DISTINCT TNIVE_CLAVE,TNIVE_DESC FROM TNIVE WHERE TNIVE_ESTATUS='A' ORDER BY 1 ";
        MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        MySqlDataReader data_saes;
        MySqlCommand consulta_saes = new MySqlCommand();
        ConexionMySql_saes.Open();
        consulta_saes.Connection = ConexionMySql_saes;
        consulta_saes.CommandType = CommandType.Text;
        consulta_saes.CommandText = strQuery;

        data_saes = consulta_saes.ExecuteReader();
        

        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        ConexionMySql.Open();

        while (data_saes.Read())
        {
            string Codigo = data_saes.GetString(0);
            string Descripcion = data_saes.GetString(1);

            MySqlCommand cmd = new MySqlCommand("SP_sinc_niveles",  ConexionMySql);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
            cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
            cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
        ConexionMySql.Close();
        ConexionMySql_saes.Close();
    }

    protected void sincroniza_tipo_alumno()
    {
        string strQuery = "";
        strQuery = "SELECT DISTINCT TTIIN_CLAVE,TTIIN_DESC FROM TTIIN WHERE TTIIN_ESTATUS='A' ORDER BY 1";
        MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        MySqlDataReader data_saes;
        MySqlCommand consulta_saes = new MySqlCommand();
        ConexionMySql_saes.Open();
        consulta_saes.Connection = ConexionMySql_saes;
        consulta_saes.CommandType = CommandType.Text;
        consulta_saes.CommandText = strQuery;

        data_saes = consulta_saes.ExecuteReader();


        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        ConexionMySql.Open();

        while (data_saes.Read())
        {
            string Codigo = data_saes.GetString(0);
            string Descripcion = data_saes.GetString(1);

            MySqlCommand cmd = new MySqlCommand("SP_sinc_tipo_alu", ConexionMySql);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
            cmd.Parameters.AddWithValue("@Descripcion_in", Descripcion);
            cmd.Parameters.AddWithValue("@fecha_in", DateTime.Now);

            cmd.ExecuteNonQuery();
        }
        ConexionMySql.Close();
        ConexionMySql_saes.Close();
    }

    protected void sincroniza_campus()
    {
        string strQuery = "";
        strQuery = "SELECT DISTINCT TCAMP_CLAVE,TCAMP_DESC,CONCAT('Calle: ',TCAMP_CALLE,' Colonia: ',TCAMP_COLONIA,' CP: ',TCAMP_TCOPO_CLAVE) DIRECCION FROM TCAMP WHERE TCAMP_ESTATUS='A' ORDER BY 1";
        MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        MySqlDataReader data_saes;
        MySqlCommand consulta_saes = new MySqlCommand();
        ConexionMySql_saes.Open();
        consulta_saes.Connection = ConexionMySql_saes;
        consulta_saes.CommandType = CommandType.Text;
        consulta_saes.CommandText = strQuery;

        data_saes = consulta_saes.ExecuteReader();


        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        ConexionMySql.Open();

        while (data_saes.Read())
        {
            string Codigo = data_saes.GetString(0);
            string Descripcion = data_saes.GetString(1);
            string Direccion = data_saes.GetString(2);

            MySqlCommand cmd = new MySqlCommand("Insertar_Campus", ConexionMySql);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Codigo_in", Codigo);
            cmd.Parameters.AddWithValue("@Nombre_in", Descripcion);
            cmd.Parameters.AddWithValue("@Direccion_in", Direccion);


            cmd.ExecuteNonQuery();
        }
        ConexionMySql.Close();
        ConexionMySql_saes.Close();
    }


    static void sincroniza_relacion_doc_nivel()
    {
        string strQuery = "";
        strQuery = "SELECT DISTINCT 'N' IDPROCEDENCIA,TCODO_TTIIN_CLAVE,TCODO_TMODA_CLAVE,TCODO_TDOCU_CLAVE,TCODO_TNIVE_CLAVE FROM TCODO WHERE TCODO_ESTATUS='A' ORDER BY TCODO_TDOCU_CLAVE";
        MySqlConnection ConexionMySql_saes = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionStringSAES"].ConnectionString);
        MySqlDataReader data_saes;
        MySqlCommand consulta_saes = new MySqlCommand();
        ConexionMySql_saes.Open();
        consulta_saes.Connection = ConexionMySql_saes;
        consulta_saes.CommandType = CommandType.Text;
        consulta_saes.CommandText = strQuery;

        data_saes = consulta_saes.ExecuteReader();


        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        ConexionMySql.Open();

        while (data_saes.Read())
        {

            string IDProcedencia = data_saes.GetString(0);
            string IDTipoIngreso = data_saes.GetString(1);
            string IDModalidad = data_saes.GetString(2);
            string Id_SAES = data_saes.GetString(3);
            string IDNivel = data_saes.GetString(4);

            string strQueryok = "SELECT IDTipoDocumento FROM TipoDocumento WHERE id_saes='" + Id_SAES + "'";
            MySqlDataAdapter mysqladapter = new MySqlDataAdapter();
            DataSet dssql = new DataSet();
            MySqlCommand commandmysql = new MySqlCommand(strQueryok, ConexionMySql);
            mysqladapter.SelectCommand = commandmysql;
            mysqladapter.Fill(dssql);
            mysqladapter.Dispose();
            commandmysql.Dispose();
            string IDTipoDocumento = dssql.Tables[0].Rows[0][0].ToString();

            string strQueryvalida = "SELECT COUNT(*) FROM TiposDocumento_nivel WHERE  IDTipoDocumento='" + IDTipoDocumento + "' AND IDProcedencia='" + IDProcedencia + "' AND IDTipoIngreso='" + IDTipoIngreso + "' AND IDModalidad='" + IDModalidad + "' AND IDNivel='" + IDNivel + "'";
            MySqlDataAdapter mysqladapter_1 = new MySqlDataAdapter();
            DataSet dssql_1 = new DataSet();
            MySqlCommand commandmysql_1 = new MySqlCommand(strQueryvalida, ConexionMySql);
            mysqladapter_1.SelectCommand = commandmysql_1;
            mysqladapter_1.Fill(dssql_1);
            mysqladapter_1.Dispose();
            commandmysql_1.Dispose();

            if (dssql_1.Tables[0].Rows[0][0].ToString() == "0")
            {
                string strquery_insert = "INSERT INTO TiposDocumento_nivel VALUES ('" + IDTipoDocumento + "','" + IDProcedencia + "','" + IDTipoIngreso + "','" + IDModalidad + "','" + IDNivel + "',CURRENT_TIMESTAMP())";
                MySqlCommand myCommandinserta = new MySqlCommand(strquery_insert, ConexionMySql);
                myCommandinserta.ExecuteNonQuery();
            }




        }
        ConexionMySql.Close();
        ConexionMySql_saes.Close();
    }


    protected void permisos()
    {
        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        ConexionMySql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=2 AND B.IDSubMenu=2 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        MySqlCommand cmd = new MySqlCommand(strQuery, ConexionMySql);
        MySqlDataReader dr = cmd.ExecuteReader();
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
        ConexionMySql.Close();
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
        DataSet dsDocumentos = (DataSet)objDocumentos.ExecuteSP("Obtener_ListaTipoDocumento_search", arrP);
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
        MySqlConnection ConexionMySql =new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        string query = "UPDATE TipoDocumento SET Nombre = @Documento, Descripcion = @Descripcion, TamanoMinimo = @TamanoMinimo, TamanoMaximo = @TamnoMaximo, Formato = @Formato, Forzoso = @Forzoso WHERE IDTipoDocumento = @IDTipoDocumento";

        
        MySqlCommand com = new MySqlCommand(query, ConexionMySql);
        com.Parameters.Add("@IDTIpoDocumento", MySqlDbType.Int32).Value = IDTipoDocumento;
        com.Parameters.Add("@Documento", MySqlDbType.VarChar).Value = Documento;
        com.Parameters.Add("@Descripcion", MySqlDbType.VarChar).Value = Descripcion;
        com.Parameters.Add("@TamanoMinimo", MySqlDbType.VarChar).Value = TamanoMinimo;
        com.Parameters.Add("@TamnoMaximo", MySqlDbType.VarChar).Value = TamanoMaximo;
        com.Parameters.Add("@Formato", MySqlDbType.VarChar).Value = Formato;
        com.Parameters.Add("@Forzoso", MySqlDbType.VarChar).Value = Forzoso;

        ConexionMySql.Open();
        com.ExecuteNonQuery();
        ConexionMySql.Close();

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
        MySqlConnection ConexionMySql = new MySqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        string query = "DELETE FROM TipoDocumento WHERE IDTipoDocumento = @IDTipoDocumento";
        string query_log = "INSERT INTO Logs_Permisos (Proceso,Descripcion) VALUES ('TipoDocumento','El usuario " + Session["user"].ToString() + "elimino el documento: " + Documento + "')";
        MySqlCommand com = new MySqlCommand(query, ConexionMySql);
        com.Parameters.Add("@IDTIpoDocumento", MySqlDbType.Int32).Value = IDTipoDocumento;
        MySqlCommand com_1 = new MySqlCommand(query_log, ConexionMySql);
        ConexionMySql.Open();
        com_1.ExecuteNonQuery();
        com.ExecuteNonQuery();
        ConexionMySql.Close();

        gvTipoDocumento.EditIndex = -1;
        gvTipoDocumento.DataBind();
    }
}