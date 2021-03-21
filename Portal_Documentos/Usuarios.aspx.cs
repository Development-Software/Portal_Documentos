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
using System.Web.Configuration;

public partial class Usuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        if (!IsPostBack)
        {
            Session["Reset"] = true;
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
            SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
            int timeout = ((int)section.Timeout.TotalMinutes - 3) * 1000 * 60;
            ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

            if (Session["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            

            LlenaPagina();
            tabla_add.Visible = false;
            permisos();
        }
    }

    private void LlenaPagina()
    {
        System.Threading.Thread.Sleep(50);

        SqlConnection ConexionSql =
                 new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        //Obtiene si el estudiante ya tiene registro

        string strQueryRol = "";
        strQueryRol = " SELECT IDRol,Nombre Descripcion FROM Rol" +
                       " UNION " +
                       " SELECT 0 IDRol, '<Selecciona Rol>' Descripcion " +
                       " ORDER BY IDRol ";

        string strQueryEsc = "";
        strQueryEsc = " SELECT a.Login, a.Nombre, c.Nombre Rol,a.Email,a.FechaUltModif FROM Usuario a INNER JOIN Roles_Usuarios b ON a.IDUsuario=b.IDUsuario INNER JOIN Rol c ON b.IDRol=c.IDRol where A.IDUsuario<>'22'";


        //Label1.Text = strQueryEsc;
        ConexionSql.Open();

        SqlDataAdapter sqladapter = new SqlDataAdapter();
        DataSet dssql = new DataSet();

        DataTable TablaRoles = new DataTable();
        SqlCommand ConsultaSql = new SqlCommand();
        SqlDataReader DatosSql;

        ConsultaSql.Connection = ConexionSql;
        ConsultaSql.CommandType = CommandType.Text;
        ConsultaSql.CommandText = strQueryRol;
        DatosSql = ConsultaSql.ExecuteReader();
        TablaRoles.Load(DatosSql, LoadOption.OverwriteChanges);

        CboRoles.DataSource = TablaRoles;
        CboRoles.DataValueField = "IDRol";
        CboRoles.DataTextField = "Descripcion";
        CboRoles.DataBind();

        SqlDataAdapter dataadapter = new SqlDataAdapter(strQueryEsc, ConexionSql);
        DataSet ds = new DataSet();
        dataadapter.Fill(ds, "Usuarios");
        Users.DataSource = ds;
        Users.DataBind();
        Users.DataMember = "Usuarios";

        ConexionSql.Close();

    }

    protected void Agregar_Click(object sender, EventArgs e)
    {
        //resultado.Text = "";
        if (Txtuser.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>swal('Falta capturar clave de usuario','', 'warning')</script>");
            Txtuser.Focus();
        }
        else if (TxtNombre.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta capturar nombre de usuario','', 'warning')</script>");
            TxtNombre.Focus();
        }
        else if (Txtcorreo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta capturar el correo electrónico','', 'warning')</script>");
            Txtcorreo.Focus();
        }
        else if (CboRoles.SelectedValue.ToString() == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta seleccionar rol de usuario','', 'warning')</script>");
            CboRoles.Focus();
        }

        else
        {
            SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
            ConexionSql.Open();

            string strvalida = "SELECT COUNT(*) FROM Usuario WHERE Login='" + Txtuser.Text + "'";

            SqlDataAdapter sqladapter = new SqlDataAdapter();

            DataSet dssql1 = new DataSet();

            SqlCommand commandsql1 = new SqlCommand(strvalida, ConexionSql);
            sqladapter.SelectCommand = commandsql1;
            sqladapter.Fill(dssql1);
            sqladapter.Dispose();
            commandsql1.Dispose();

            if (dssql1.Tables[0].Rows[0][0].ToString() == "0")
            {

                SqlCommand cmd = new SqlCommand("SP_insert_user", ConexionSql);
                cmd.CommandType = CommandType.StoredProcedure;

                //Ejecucion del comando en el servidor de BD
                cmd.Parameters.AddWithValue("@login", Txtuser.Text);
                cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text);
                cmd.Parameters.AddWithValue("@email", Txtcorreo.Text);
                cmd.Parameters.AddWithValue("@idrol", Convert.ToInt32(CboRoles.SelectedValue.ToString()));
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", Session["user"].ToString());
                cmd.ExecuteNonQuery();

                ConexionSql.Close();
                ClientScript.RegisterStartupScript(this.GetType(), "", "Agrega_usuario()", true);

                TxtNombre.Text = "";
                Txtuser.Text = "";
                Txtcorreo.Text = "";
                LlenaPagina();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('El nombre de usuario ya existe','', 'warning')</script>");
            }
        }

    }

    protected void Borrar_Click(object sender, EventArgs e)
    {
        if (Txtuser.Text == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta capturar clave de usuario','', 'warning')</script>");
        }
        else
        {
            string strBorra = "DELETE FROM Roles_Usuarios WHERE IDUsuario = (SELECT IDUsuario FROM Usuario WHERE Login='" + Txtuser.Text + "')";
            string strBorra_1 = "DELETE FROM Usuario WHERE Login='" + Txtuser.Text + "'";
            string strlog = "INSERT INTO Logs (Proceso,Descripcion,Usuario,Fecha) VALUES ('Eliminar Usuario','Se elimino al usuario "+ Txtuser.Text+"','"+Session["user"].ToString()+"','"+DateTime.Now.ToString()+"')";
            SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

            ConexionSql.Open();

            SqlCommand myCommandlog = new SqlCommand(strlog, ConexionSql);
            myCommandlog.ExecuteNonQuery();
            SqlCommand myCommandborra = new SqlCommand(strBorra, ConexionSql);
            myCommandborra.ExecuteNonQuery();
            SqlCommand myCommandborra_2 = new SqlCommand(strBorra_1, ConexionSql);
            myCommandborra_2.ExecuteNonQuery();
            ConexionSql.Close();

            ClientScript.RegisterStartupScript(this.GetType(), "", "Elimina_usuario()", true);

            LlenaPagina();
            TxtNombre.Text = "";
            Txtuser.Text = "";
        }

    }

    protected void GridUsuarios_Click(object sender, EventArgs e)
    {
        GridViewRow row = Users.SelectedRow;

        SqlConnection ConexionSql =
                       new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        string strQuery = "";
        strQuery = "SELECT Nombre,Email FROM Usuario WHERE Login='" + row.Cells[1].Text.ToString() + "'";

        ConexionSql.Open();

        SqlDataAdapter sqladapter = new SqlDataAdapter();

        DataSet dssql1 = new DataSet();

        SqlCommand commandsql1 = new SqlCommand(strQuery, ConexionSql);
        sqladapter.SelectCommand = commandsql1;
        sqladapter.Fill(dssql1);
        sqladapter.Dispose();
        commandsql1.Dispose();

        Txtuser.Text = row.Cells[1].Text;
        TxtNombre.Text = dssql1.Tables[0].Rows[0][0].ToString();
        Txtcorreo.Text = dssql1.Tables[0].Rows[0][1].ToString();
        CboRoles.Focus();
        Agregar_cmd.Visible = false;
        //update.Visible = true;
        Txtuser.ReadOnly = true;
        //delete.Visible = true;
        cancel.Visible = true;
        tabla_add.Visible = true;
        add_user.Visible = false;
        //CboCampus.Items.FindByText(row.Cells[1].Text.Trim()).Selected = true;
    }


    protected void Update_Click(object sender, ImageClickEventArgs e)
    {
        if (Txtuser.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "<script>swal('Falta capturar clave de usuario','', 'warning')</script>");
            Txtuser.Focus();
        }
        else if (TxtNombre.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta capturar nombre de usuario','', 'warning')</script>");
            TxtNombre.Focus();
        }
        else if (Txtcorreo.Text.Trim() == "")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta capturar el correo electrónico','', 'warning')</script>");
            Txtcorreo.Focus();
        }
        else if (CboRoles.SelectedValue.ToString() == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alerta", "<script>swal('Falta seleccionar rol de usuario','', 'warning')</script>");
            CboRoles.Focus();
        }
        else
        {
            SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
            ConexionSql.Open();

            SqlCommand cmd = new SqlCommand("SP_update_user", ConexionSql);
            cmd.CommandType = CommandType.StoredProcedure;

            //Ejecucion del comando en el servidor de BD
            cmd.Parameters.AddWithValue("@login", Txtuser.Text);
            cmd.Parameters.AddWithValue("@Nombre", TxtNombre.Text);
            cmd.Parameters.AddWithValue("@email", Txtcorreo.Text);
            cmd.Parameters.AddWithValue("@idrol", Convert.ToInt32(CboRoles.SelectedValue.ToString()));
            cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
            cmd.Parameters.AddWithValue("@username", Session["user"].ToString());
            cmd.ExecuteNonQuery();

            ConexionSql.Close();
            ClientScript.RegisterStartupScript(this.GetType(), "", "Actualiza_usuario()", true);

            TxtNombre.Text = "";
            Txtuser.Text = "";
            Txtcorreo.Text = "";
            LlenaPagina();


        }
    }

    protected void cancel_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("Usuarios.aspx");
    }

    protected void add_user_Click(object sender, EventArgs e)
    {
        tabla_add.Visible = true;
        add_user.Visible = false;
        cancel.Visible = true;
        update.Visible = false;
        Users.Columns[0].HeaderStyle.CssClass = "oculto";
        Users.Columns[0].ItemStyle.CssClass = "oculto";
    }
    protected void permisos()
    {
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        ConexionSql.Open();
        string strQuery = "SELECT DISTINCT A.IDPrivilegio,b.Permiso FROM Permisos_App_Rol A INNER JOIN Permisos_App B ON A.IDPrivilegio=B.IDPrivilegio INNER JOIN Rol C ON A.IDRol=C.IDRol WHERE B.IDMenu=2 AND B.IDSubMenu=3 AND C.Nombre='" + Session["Rol"].ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQuery, ConexionSql);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            int IDprivilegio = dr.GetInt32(0);

            if (IDprivilegio == 8) { add_user.Visible = true; Agregar_cmd.Visible = true; } //Permiso para Agregar usuarios
            else if (IDprivilegio == 9) { Users.Columns[0].Visible = true; update.Visible = true; } //Permiso para Editar Usuarios
            else if (IDprivilegio == 10) { Users.Columns[0].Visible = true; delete.Visible = true; }//Permiso para Eliminar Usuarios
            else
            {
                add_user.Visible = false;
                Agregar_cmd.Visible = false;
                Users.Columns[0].Visible = false;
                update.Visible = false;
                Users.Columns[0].Visible = false;
                delete.Visible = false;
            }


        }
        ConexionSql.Close();
    }
}