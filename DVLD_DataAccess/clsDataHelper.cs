using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    internal class clsDataHelper
    {
        public static bool GetSingleRow(string StoredName, SqlParameter[] Parameters, Action<SqlDataReader> mapAction)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if(Parameters != null)command.Parameters.AddRange(Parameters);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            mapAction(reader);
                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }
                    }
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }


        public static async Task<bool> GetSingleRowAsync(string StoredName, SqlParameter[] Parameters, Action<SqlDataReader> mapAction)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null) command.Parameters.AddRange(Parameters);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;

                            mapAction(reader);
                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }
                    }
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }


        public static DataTable GetDataTable(string StoredName, SqlParameter[] Parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null) command.Parameters.AddRange(Parameters);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // The record was found
                            dt.Load(reader);
                        }
                        else
                        {
                            // The record was not found
                        }
                    }
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }

            }

            return dt;
        }


        public static async Task<DataTable> GetDataTableAsync(string StoredName, SqlParameter[] Parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null) command.Parameters.AddRange(Parameters);

                try
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (reader.HasRows)
                        {
                            // The record was found
                            dt.Load(reader);
                        }
                        else
                        {
                            // The record was not found
                        }
                    }
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                }

            }

            return dt;
        }



        public static int ExecuteNonQuery(string StoredName, SqlParameter[] Parameters)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null) command.Parameters.AddRange(Parameters);

                try
                {
                    connection.Open();

                    RowsAffected = command.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    RowsAffected = -1;
                }
            }

            return RowsAffected;
        }


        public static async Task<int> ExecuteNonQueryAsync(string StoredName, SqlParameter[] Parameters)
        {
            int RowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(Setteing.ConnectionString))
            using (SqlCommand command = new SqlCommand(StoredName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                if (Parameters != null) command.Parameters.AddRange(Parameters);

                try
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    RowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                }

                catch (Exception ex)
                {
                    //Console.WriteLine("Error: " + ex.Message);
                    RowsAffected = -1;
                }
            }

            return RowsAffected;
        }


        public static object ExecuteScalar(string procedureName, SqlParameter[] parameters)
        {
            object result = null;

            using (var connection = new SqlConnection(Setteing.ConnectionString))
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null) command.Parameters.AddRange(parameters);

                try
                {
                    connection.Open();
                    result = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    // Log Error
                    result = null;
                }
            }

            return (result == DBNull.Value) ? null : result;
        }


        public static async Task<object> ExecuteScalarAsync(string procedureName, SqlParameter[] parameters)
        {
            object result = null;

            using (var connection = new SqlConnection(Setteing.ConnectionString))
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null) command.Parameters.AddRange(parameters);

                try
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // Log Error
                    result = null;
                }
            }

            return (result == DBNull.Value) ? null : result;
        }


    }
}
