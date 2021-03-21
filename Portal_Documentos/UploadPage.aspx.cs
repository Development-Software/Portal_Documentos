using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadPage : System.Web.UI.Page
{
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

        string IDTipoDocumento = Convert.ToString(Request.QueryString["IDTipoDocumento"]);
        string IDDocumento = Convert.ToString(Request.QueryString["IDDocumento"]);
        string IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);

        if (IDTipoDocumento == null || IDDocumento == null || IDAlumno == null)
        {
            Response.Redirect("Inicio.aspx");
        }

        if (Request.Files.Count > 0)
        {
            DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/UploadedFiles/"));

            string path = virtualDirPath.ToString();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                string extension = System.IO.Path.GetExtension(file.FileName);
                string formato= extension.Replace(".", "");
                path += IDTipoDocumento + extension;

                string strQuery = "SELECT DISTINCT COUNT(*)Contador FROM Documentos_Alumno WHERE IDTipoDocumento=@IDTipoDocumento AND IDAlumno=@IDAlumno";
                SqlCommand cmd = new SqlCommand(strQuery);
                cmd.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = IDTipoDocumento;
                cmd.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
                DataTable dt = GetData(cmd);

                if (dt.Rows[0]["Contador"].ToString() == "0")
                {
                    try
                    {
                        Stream fs = file.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] ImagenOriginal = br.ReadBytes((Int32)fs.Length);

                        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "INSERT INTO Documentos_Alumno (IDAlumno,IDTipoDocumento,Documento,IDEstatusDocumento,Formato,FechaUltModif) VALUES(@IDAlumno,@IDTipoDocumento,@Documento,@IDEstatusDocumento,@Formato,@FechaUltModif)";
                        cmd1.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
                        cmd1.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = IDTipoDocumento;
                        cmd1.Parameters.Add("@Documento", SqlDbType.Binary).Value = ImagenOriginal;
                        cmd1.Parameters.Add("@IDEstatusDocumento", SqlDbType.Int).Value = 17;
                        cmd1.Parameters.Add("@Formato", SqlDbType.VarChar).Value = formato.ToLower();
                        cmd1.Parameters.Add("@FechaUltModif", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = ConexionSql;
                        ConexionSql.Open();
                        cmd1.ExecuteNonQuery();
                        ConexionSql.Close();
                        
                        if (Session["Rol"].ToString() != "Alumno")
                        {
                            log(IDAlumno, file.FileName);
                        }

                    }
                    catch (HttpException ex)
                    {
                        DirectoryInfo vpath = new DirectoryInfo(Server.MapPath("~/Logs/"));
                        StreamWriter sw = new StreamWriter(vpath + IDAlumno+ "_UploadPage.txt", true);

                        sw.WriteLine(ex.ToString());
                        sw.Close();
                    }
                }
                else
                {
                    try
                    {
                        Stream fs = file.InputStream;
                        BinaryReader br = new BinaryReader(fs);
                        Byte[] ImagenOriginal = br.ReadBytes((Int32)fs.Length);

                        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.CommandText = "UPDATE Documentos_Alumno SET Documento=@Documento,IDEstatusDocumento=@IDEstatusDocumento, Formato=@formato, FechaUltModif=@FechaUltModif WHERE IDTipoDocumento=@IDTipoDocumento AND IDAlumno=@IDAlumno AND IDDocumento=@IDDocumento";
                        cmd2.Parameters.Add("@Documento", SqlDbType.Binary).Value = ImagenOriginal;
                        cmd2.Parameters.Add("@IDEstatusDocumento", SqlDbType.Int).Value = 17;
                        cmd2.Parameters.Add("@Formato", SqlDbType.VarChar).Value = formato.ToLower();
                        cmd2.Parameters.Add("@FechaUltModif", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd2.Parameters.Add("@IDTipoDocumento", SqlDbType.Int).Value = IDTipoDocumento;
                        cmd2.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
                        cmd2.Parameters.Add("@IDDocumento", SqlDbType.Int).Value = IDDocumento;
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Connection = ConexionSql;
                        ConexionSql.Open();
                        cmd2.ExecuteNonQuery();
                        ConexionSql.Close();
                        
                        if (Session["Rol"].ToString() != "Alumno")
                        {
                            log(IDAlumno, file.FileName);
                        }
                    }
                    catch (HttpException ex)
                    {
                        DirectoryInfo vpath = new DirectoryInfo(Server.MapPath("~/Logs/"));
                        StreamWriter sw = new StreamWriter(vpath + IDAlumno + "_UploadPage.txt", true);

                        sw.WriteLine(ex.ToString());
                        sw.Close();
                    }

                }
                
            }
            //Se comenta el control para evitar el envio de correos cuando se entrega un documento. 
            //envia_correo_estatus("3");
            //Se validan documentos entregados para envio de notificación de documentos completos
            valida_entregados();
            string json = "{}";
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(json);
            Response.End();


        }
        
    }

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
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


    protected void envia_correo_estatus(string Estatus)
    {
        //string IDEstatus = DropDownEstatus.SelectedValue.ToString();
        string IDDocumento = Convert.ToString(Request.QueryString["IDDocumento"]);
        string IDTipoDocumento = Convert.ToString(Request.QueryString["IDTipoDocumento"]);
        string IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);


        string strQuery = "SELECT DISTINCT IDNotificacion,IDEstatus,Descripcion,Tipo_Notificacion, Asunto_correo,Cuerpo_correo FROM Configuracion_Notificaciones WHERE Dias=0 AND IDEstatus=@IDEstatus";
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(strQuery);
        try
        {
            ConexionSql.Open();

            cmd.Parameters.AddWithValue("@IDEstatus", Estatus);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;
            cmd.Connection = ConexionSql;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                obtener_plantilla_correo_admin(dr.GetInt32(0), Estatus, IDAlumno, IDTipoDocumento, "notificaciones");

            }

        }
        catch (Exception ex)
        {
            DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
            StreamWriter sw = new StreamWriter(virtualDirPath + "Error(envia_correo_estatus)_" + IDAlumno + ".txt", true);

            sw.WriteLine(ex.Message.ToString());
            sw.WriteLine(cmd.CommandText);
            sw.Close();
        }
        finally { ConexionSql.Close(); }



    }

    protected void obtener_plantilla_correo(int IDNotificacion, string IDEstatus, string IDTipoDocumento, string IDAlumno)
    {

        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        SqlCommand cmd = new SqlCommand("Creacion_correo_upload", ConexionSql);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IDNotificacion", IDNotificacion);
        cmd.Parameters.AddWithValue("@IDEstatus", IDEstatus);
        cmd.Parameters.AddWithValue("@IDTipoDocumento", IDTipoDocumento);
        cmd.Parameters.AddWithValue("@IDAlumno", IDAlumno);
        cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
        cmd.Parameters["@Correo"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 1000);
        cmd.Parameters["@Subject"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Body", SqlDbType.VarChar, 8000);
        cmd.Parameters["@Body"].Direction = ParameterDirection.Output;
        try
        {
            ConexionSql.Open();
            //Executing the SP

            int i = cmd.ExecuteNonQuery();
            //Storing the output parameters value in 3 different variables.
            string Correo = Convert.ToString(cmd.Parameters["@Correo"].Value);
            string Subject = Convert.ToString(cmd.Parameters["@Subject"].Value);
            string Body = Convert.ToString(cmd.Parameters["@Body"].Value);
            //TextBox2.Text = Correo + "--" + Subject + "--" + Body;
            servicio_correo(Correo, Subject, Body, IDAlumno);
        }

        catch (Exception ex)
        {
            DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
            StreamWriter sw = new StreamWriter(virtualDirPath + "Error(obtener_plantilla_correo)_" + IDAlumno + ".txt", true);

            sw.WriteLine(ex.Message.ToString());
            sw.Close();
        }
        finally
        {
            ConexionSql.Close();
        }



    }

    protected void obtener_plantilla_correo_admin(int IDNotificacion, string IDEstatus, string IDAlumno, string IDTipoDocumento, string User)
    {

        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);

        SqlCommand cmd = new SqlCommand("Creacion_correo_admin_upload", ConexionSql);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@IDNotificacion", IDNotificacion);
        cmd.Parameters.AddWithValue("@IDEstatus", IDEstatus);
        cmd.Parameters.AddWithValue("@IDTipoDocumento", IDTipoDocumento);
        cmd.Parameters.AddWithValue("@IDAlumno", IDAlumno);
        cmd.Parameters.AddWithValue("@user", User);
        cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 100);
        cmd.Parameters["@Correo"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Subject", SqlDbType.VarChar, 1000);
        cmd.Parameters["@Subject"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@Body", SqlDbType.VarChar, 8000);
        cmd.Parameters["@Body"].Direction = ParameterDirection.Output;
        try
        {
            ConexionSql.Open();
            //Executing the SP

            int i = cmd.ExecuteNonQuery();
            //Storing the output parameters value in 3 different variables.
            string Correo = Convert.ToString(cmd.Parameters["@Correo"].Value);
            string Subject = Convert.ToString(cmd.Parameters["@Subject"].Value);
            string Body = Convert.ToString(cmd.Parameters["@Body"].Value);
            //TextBox2.Text = Correo + "--" + Subject + "--" + Body;
            servicio_correo(Correo, Subject, Body, IDAlumno);
        }

        catch (Exception ex)
        {
            DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
            StreamWriter sw = new StreamWriter(virtualDirPath + "Error(obtener_plantilla_correo_admin)_" + IDAlumno + ".txt", true);

            sw.WriteLine(ex.Message.ToString());
            sw.WriteLine(Convert.ToString(cmd.Parameters["@Correo"].Value));
            sw.Close();
        }
        finally
        {
            ConexionSql.Close();
        }



    }
    protected void servicio_correo(string Correo, string subject, string body, string IDAlumno)
    {
        //MailMessage mailMessage = new MailMessage();
        //var fromAddress = new MailAddress(ConfigurationManager.AppSettings["AdminMail"].ToString(), ConfigurationManager.AppSettings["DisplayName"].ToString());
        //MailMessage message = new MailMessage(fromAddress, Correo,subject,body);
        //message.IsBodyHtml = true;
        //SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["MailHost"].ToString(), int.Parse(ConfigurationManager.AppSettings["Port"].ToString()));
        //smtpClient.UseDefaultCredentials = true;

        MailAddress from = new MailAddress(ConfigurationManager.AppSettings["AdminMail"].ToString(), ConfigurationManager.AppSettings["DisplayName"].ToString());
        MailAddress to = new MailAddress(Correo);
        MailMessage message = new MailMessage(from, to);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["MailHost"].ToString(), int.Parse(ConfigurationManager.AppSettings["Port"].ToString()));
        client.Credentials = CredentialCache.DefaultNetworkCredentials;
        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            DirectoryInfo virtualDirPath = new DirectoryInfo(Server.MapPath("~/Logs/"));
            StreamWriter sw = new StreamWriter(virtualDirPath + "Error(servicio_correo)_" + IDAlumno + ".txt", true);

            sw.WriteLine(ex.Message.ToString());
            sw.Close();
        }

    }

    protected void log(string IDAlumno, string Documento)
    {

        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandText = "INSERT INTO Logs (Proceso,Descripcion,Usuario,Fecha) VALUES ('Subir Archivo','Se a cargado correctamente el documento '+@Documento+' al alumno '+@IDAlumno,@UserLog,@fecha_mod)";
        cmd1.Parameters.Add("@IDAlumno", SqlDbType.VarChar).Value = IDAlumno;
        cmd1.Parameters.Add("@Documento", SqlDbType.VarChar).Value = Documento;
        cmd1.Parameters.Add("@UserLog", SqlDbType.VarChar).Value = Session["user"].ToString(); ;
        cmd1.Parameters.Add("@fecha_mod", SqlDbType.DateTime).Value = DateTime.Now;
        cmd1.CommandType = CommandType.Text;
        cmd1.Connection = ConexionSql;
        ConexionSql.Open();
        cmd1.ExecuteNonQuery();
        ConexionSql.Close();

    }

    //Valida si el alumno ya tiene todos sus documentos entregados//

    protected void valida_entregados()
    {
        string IDAlumno = Convert.ToString(Request.QueryString["IDAlumno"]);
        

        string strQuery = "SELECT DISTINCT COUNT(TDA.IDTipoDocumento)Documentos, " +
                          "(SELECT COUNT(*) FROM Documentos_Alumno WHERE IDEstatusDocumento in (17,3) AND IDAlumno='"+IDAlumno+"')Entregados " +
                          "FROM TiposDocumento_Area TDA " +
                          "JOIN TipoDocumento TD ON TD.IDTipoDocumento = TDA.IDTipoDocumento " +
                          "JOIN Alumno AL ON AL.IDArea = TDA.IDArea AND AL.CodigoProcedencia = TDA.IDProcedencia AND AL.CodigoTipoIngreso = TDA.IDTipoIngreso AND AL.IDNivel=TDA.IDNivel  AND AL.IDModalidad=TDA.IDModalidad AND AL.IDAlumno = '" + IDAlumno + "' " +
                          "WHERE TD.id_banner NOT IN ('RVAL','SVAL')";
        SqlConnection ConexionSql = new SqlConnection(ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(strQuery);
        DataTable dt = GetData(cmd);
        if(dt.Rows[0]["Documentos"].ToString()== dt.Rows[0]["Entregados"].ToString())
        {
            cambia_estatus(IDAlumno);
            envia_correo_estatus("11");
        }

    }

    protected void cambia_estatus(string IDAlumno)
    {
        
        string strQuery = "SELECT TDA.IDDocumento,TDA.IDEstatusDocumento,TDA.Documento,TDA.FechaUltModif " +
                            "FROM Documentos_Alumno TDA " +
                            "JOIN TipoDocumento TD ON TD.IDTipoDocumento = TDA.IDTipoDocumento  " +
                            "WHERE TD.id_banner NOT IN ('RVAL','SVAL')  " +
                            "AND TDA.IDAlumno='"+IDAlumno+"'";
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand command = new SqlCommand(strQuery, con);
        con.Open();
        SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {

                update_status(reader["IDDocumento"].ToString());
            }

        }
        reader.Close();

        con.Close();
    }

    protected void update_status( string IDDocumento)
    {
        string Query_Insert;
        Query_Insert = "UPDATE Documentos_Alumno SET IDEstatusDocumento='3', FechaUltModif='" + DateTime.Now + "' WHERE IDDocumento='" + IDDocumento + "'";
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlCommand commandsql = new SqlCommand(Query_Insert, con);
        con.Open();
        commandsql.ExecuteNonQuery();
        con.Close();
    }
}