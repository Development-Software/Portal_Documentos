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
            using (SqlConnection objCnn = new SqlConnection(mstrConnectionString))
            {

                //SqlConnection objCnn = new SqlConnection(mstrConnectionString);
                SqlCommand objCmd = new SqlCommand();

                try
                {
                    objCnn.Open();
                    objCmd.Connection = objCnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = pstrName;

                    foreach (Parametro objParam in parrParameters)
                    {
                        SqlParameter objNewParam = new SqlParameter(objParam.Nombre, objParam.Valor);
                        objCmd.Parameters.Add(objNewParam);
                    }

                    return objCmd.ExecuteNonQuery();

                }
                catch
                {
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
            SqlConnection objCnn = new SqlConnection(mstrConnectionString);
            SqlCommand objCmd = new SqlCommand();
            SqlDataAdapter objDA = new SqlDataAdapter();


            try
            {
                objCnn.Open();
                objCmd.Connection = objCnn;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = pstrName;

                foreach (Parametro objParam in parrParameters)
                {
                    SqlParameter objNewParam = new SqlParameter(objParam.Nombre, objParam.Valor);
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
            using (SqlConnection objCnn = new SqlConnection(mstrConnectionString))
            {
                SqlCommand objCmd = new SqlCommand();
                SqlDataAdapter objDA = new SqlDataAdapter();
                DataSet dsReturn = new DataSet();

                try
                {
                    objCnn.Open();
                    objCmd.Connection = objCnn;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = pstrName;

                    foreach (Parametro objParam in parrParameters)
                    {
                        SqlParameter objNewParam = new SqlParameter(objParam.Nombre, objParam.Valor);
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
