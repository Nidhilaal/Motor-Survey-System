using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Dbconnection
    {
        private static string ConnString = GetConnectionString("ConnectionString", string.Empty);
        public static string GetConnectionString(string key, string defaultValue)
        {
            string value = string.Empty;
            if (string.IsNullOrEmpty(defaultValue))
            {
                value = defaultValue;
            }
            try
            {
                value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write("Error reading webconfig : " + ex.ToString());
            }
            return value;
        }
        public static OracleConnection OpenConnection()
        {
            try
            {
                OracleConnection con = new OracleConnection();
                con.ConnectionString = ConnString;
                con.Open();
                return con;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {

            }
        }

        public static void CloseConnection(OracleConnection connection)
        {
            try
            {
                if (connection != null)
                    connection.Close();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
            }
        }

        public static int ExecuteQuery(string sql)
        {
            OracleConnection connection = null;
            try
            {
                OracleCommand cmd = new OracleCommand();
                connection = OpenConnection();
                cmd.CommandText = sql;
                cmd.Connection = connection;
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {

                throw err;
            }
            finally
            {
                CloseConnection(connection);
            }
        }

        public static DataTable ExecuteDataset(string query)
        {
            OracleCommand cmd;
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                cmd = new OracleCommand(query, connection);
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static object ExecuteScalar(string query)
        {
            DataSet Dset = new DataSet();
            OracleConnection connection = null;
            try
            {
                connection = OpenConnection();
                OracleCommand objcmd = new OracleCommand(query, connection);
                object outcount = objcmd.ExecuteScalar();
                return outcount;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            if (Dset != null && Dset.Tables[0] != null)
            {
                return Dset.Tables[0];
            }
            else
            {
                return null;
            }
        }
        public static int ExecuteQuery(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            int rval;
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                rval = cmd.ExecuteNonQuery();
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return rval;

        }


        public static DataSet ExecuteQuerySelect(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            DataSet Dset = new DataSet();
            try
            {

                connection = OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
                        }
                    }
                }
                cmd.CommandText = query;
                cmd.Connection = connection;
                OracleDataAdapter Da = new OracleDataAdapter(cmd);
                Da.Fill(Dset);
            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;

            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return Dset;

        }
        public static int ExecuteStoredProcedure(Dictionary<string, object> paramValues, string storedProcedureName)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            int rval =0;
            try
            {
                connection = OpenConnection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                cmd.Connection = connection;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {
                    OracleParameter param = new OracleParameter(pValue.Key, pValue.Value);
                    if (pValue.Value == null)
                    {
                        param.Direction = ParameterDirection.Output;
                        param.Size = 4000;
                    }
                    cmd.Parameters.Add(param);
                }

                rval = cmd.ExecuteNonQuery();

                foreach (OracleParameter parameter in cmd.Parameters)
                 {
                    if (parameter.Direction == ParameterDirection.Output && paramValues.ContainsKey(parameter.ParameterName))
                    {
                        paramValues[parameter.ParameterName] = parameter.Value;
                    }
                }

            }
            catch (OracleException sqlerr)
            {
                throw sqlerr;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open)
                    CloseConnection(connection);
            }
            return rval;
        }
    }
}
