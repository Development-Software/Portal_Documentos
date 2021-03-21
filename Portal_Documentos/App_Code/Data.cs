using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace applyWeb.Data
{
    public class Data
    {
        private string mstrConnectionString;
        public Data(string pstrConnectionString)
        {
            mstrConnectionString = pstrConnectionString;
        }

        public int ExecuteInsertSP(string pstrName, ArrayList parrParameters)
        {
            using (MySqlConnection objCnn = new MySqlConnection(mstrConnectionString))
            {

                //SqlConnection objCnn = new SqlConnection(mstrConnectionString);
                MySqlCommand objCmd = new MySqlCommand();

                try
                {
                    objCnn.Open();
                    objCmd.Connection = objCnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = pstrName;

                    foreach (Parametro objParam in parrParameters)
                    {
                        MySqlParameter objNewParam = new MySqlParameter(objParam.Nombre, objParam.Valor);
                        objCmd.Parameters.Add(objNewParam);
                    }

                    return objCmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    string str = (ex.Message);
                    return 0;
                }
                finally
                {
                    objCnn.Close();
                    objCnn.Dispose();
                    objCmd.Dispose();
                }
            }
        }

        public IDataReader ExecuteReader(string pstrName, ArrayList parrParameters)
        {
            //using (SqlConnection objCnn = new SqlConnection(mstrConnectionString))
            //{
            MySqlConnection objCnn = new MySqlConnection(mstrConnectionString);
            MySqlCommand objCmd = new MySqlCommand();
            MySqlDataAdapter objDA = new MySqlDataAdapter();


            try
            {
                objCnn.Open();
                objCmd.Connection = objCnn;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = pstrName;

                foreach (Parametro objParam in parrParameters)
                {
                    MySqlParameter objNewParam = new MySqlParameter(objParam.Nombre, objParam.Valor);
                    objCmd.Parameters.Add(objNewParam);
                }

                return objCmd.ExecuteReader();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
            finally
            {
                //objCnn.Close();
                //objCnn = null;
                //objCmd = null;
                //objDA = null;
            }
            //}
        }

        public DataSet ExecuteSP(string pstrName, ArrayList parrParameters)
        {
            using (MySqlConnection objCnn = new MySqlConnection(mstrConnectionString))
            {
                MySqlCommand objCmd = new MySqlCommand();
                MySqlDataAdapter objDA = new MySqlDataAdapter();
                DataSet dsReturn = new DataSet();

                try
                {
                    objCnn.Open();
                    objCmd.Connection = objCnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = pstrName;

                    foreach (Parametro objParam in parrParameters)
                    {
                        MySqlParameter objNewParam = new MySqlParameter(objParam.Nombre, objParam.Valor);
                        objCmd.Parameters.Add(objNewParam);
                    }

                    objDA.SelectCommand = objCmd;

                    objDA.Fill(dsReturn);
                    return dsReturn;
                }
                catch (Exception es)
                {
                    throw new Exception(es.Message);
                }
                finally
                {
                    objCnn.Close();
                    objCnn.Dispose();
                    objCmd.Dispose();
                    objDA.Dispose();
                }
            }
        }
    }

    public class Parametro
    {
        private string mstrNombre;
        public string Nombre
        {
            get { return mstrNombre; }
            set { mstrNombre = value; }
        }

        private object mobjValor;
        public object Valor
        {
            get { return mobjValor; }
            set { mobjValor = value; }
        }

        public Parametro()
        {
            mstrNombre = "";
            mobjValor = "";
        }

        public Parametro(string pstrNombre, object pobjValor)
        {
            mstrNombre = pstrNombre;
            mobjValor = pobjValor;
        }

    }
}
