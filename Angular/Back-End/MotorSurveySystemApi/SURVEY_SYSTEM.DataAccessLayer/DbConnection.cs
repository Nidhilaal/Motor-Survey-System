using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SURVEY_SYSTEM.DataAccessLayer
{
    public class DbConnection
    {
        private  readonly IConfiguration _configuration;

        public DbConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbConnection()
        {

        }

        public string GetConnectionString()
        {
              var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            var ConnectionString = _configuration.GetConnectionString("DatabaseConnection");
            return ConnectionString;
        }


        public OracleConnection OpenConnection()
        {
            try
            {
                string connectionString = GetConnectionString();
                OracleConnection con = new OracleConnection(connectionString);

                con.Open();
                return con;
            }
            catch (OracleException oracleEx)
            {
                // Log or handle Oracle specific exceptions
                throw new Exception("Error connecting to Oracle database", oracleEx);
            }
            catch (Exception ex)
            {
                // Log or handle other exceptions
                throw new Exception("Error connecting to the database", ex);
            }
            finally
            {
                // Close or dispose resources if necessary
                // For example: con?.Dispose();
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


                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();
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
                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();
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
                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();

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
          
        }
        public static int ExecuteQuery(Dictionary<string, object> paramValues, string query)
        {

            OracleCommand cmd = new OracleCommand();
            OracleConnection connection = null;
            int rval;
            try
            {
                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            OracleParameter parameter = new OracleParameter(pValue.Key.ToString(), pValue.Value ?? DBNull.Value);
                            cmd.Parameters.Add(parameter);


                            //cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            OracleParameter parameter = new OracleParameter(pValue.Key.ToString(), pValue.Value ?? DBNull.Value);
                            cmd.Parameters.Add(parameter);

                            // cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
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
                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();
                cmd.CommandType = CommandType.Text;

                foreach (KeyValuePair<string, object> pValue in paramValues)
                {

                    if (query.Contains(":" + pValue.Key.ToString()))
                    {
                        if (pValue.Value != null && !string.IsNullOrEmpty(pValue.Value.ToString()))
                        {
                            OracleParameter parameter = new OracleParameter(pValue.Key.ToString(), pValue.Value ?? DBNull.Value);
                            cmd.Parameters.Add(parameter);


                            //cmd.Parameters.AddWithValue(pValue.Key.ToString(), pValue.Value);
                        }
                        else
                        {
                            OracleParameter parameter = new OracleParameter(pValue.Key.ToString(), pValue.Value ?? DBNull.Value);
                            cmd.Parameters.Add(parameter);

                            // cmd.Parameters.AddWithValue(pValue.Key.ToString(), DBNull.Value);
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
            int rval = 0;
            try {
                DbConnection dbConnection = new DbConnection();
                connection = dbConnection.OpenConnection();
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
