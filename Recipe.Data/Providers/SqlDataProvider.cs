using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Recipe.Data.Interfaces;

namespace Recipe.Data.Providers
{
    public class SqlDataProvider : IDataProvider
    {
        //https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-code-examples
        private readonly string connString;

        public SqlDataProvider(string connectionString)
        {
            this.connString = connectionString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connString);
        }

        public void Get(string storedProc, Action<IDataReader, short> map)
        {
            if (map == null)
            {
                throw new NullReferenceException("Provide ObjectMapper");
            }

            SqlConnection connection = null;
            short result = 0;

            using (connection = GetConnection())
            {
                var command = new SqlCommand(storedProc, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"\t{reader[0]}\t{reader[1]}\t{reader[2]}");
                        if (map != null)
                        {
                            map(reader, result);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void ExecuteProc(
            string storedProc, 
            Action<SqlParameterCollection> paramMapper, 
            Action<IDataReader, short> map, 
            Action<SqlParameterCollection> returnParams = null, 
            Action<SqlCommand> cmdModifier = null)
        {
            if (map == null)
            {
                throw new NullReferenceException("Provide ObjectMapper");
            }

            SqlDataReader reader = null;
            SqlCommand cmd = null;
            SqlConnection conn = null;
            short result = 0;

            try
            {
                using (conn = GetConnection())
                {
                    if (conn != null)
                    {
                        if (conn.State != ConnectionState.Open)
                        {
                            conn.Open();
                        }

                        cmd = GetCommand(conn, storedProc, paramMapper);
                        if (cmd != null)
                        {
                            if (cmdModifier != null)
                            {
                                cmdModifier(cmd);
                            }

                            reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                            while (true)
                            {
                                while (reader.Read())
                                {
                                    if (map != null)
                                    {
                                        map(reader, result);
                                    }

                                    result += 1;
                                }

                                if (reader.IsClosed || !reader.NextResult())
                                    break;

                                if (result > 10)
                                {
                                    throw new Exception("Max result sets exceeded.");
                                }
                            }

                            reader.Close();

                            if (returnParams != null)
                            {
                                returnParams(cmd.Parameters);
                            }

                            if (conn.State != ConnectionState.Closed)
                            {
                                conn.Close();
                            }
                        }
                    }
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
        }

        public int Add(string storedProc, Action<SqlParameterCollection> paramMapper, Action<SqlParameterCollection> returnParams = null)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            
            try
            {
                using (connection = GetConnection())
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        command = GetCommand(connection, storedProc, paramMapper);
                        if (command != null)
                        {
                            int returnVal = command.ExecuteNonQuery();

                            if (connection.State != ConnectionState.Closed)
                                connection.Close();

                            if (returnParams != null)
                                returnParams(command.Parameters);

                            return returnVal;
                        }

                    }
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        public int Update(string storedProc, Action<SqlParameterCollection> paramMapper)
        {
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                using (connection = GetConnection())
                {
                    if (connection != null)
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();

                        command = GetCommand(connection, storedProc, paramMapper);
                        if (command != null)
                        {
                            int returnVal = command.ExecuteNonQuery();

                            if (connection.State != ConnectionState.Closed)
                                connection.Close();

                            return returnVal;
                        }

                    }
                }
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            return -1;
        }

        private SqlCommand GetCommand(SqlConnection conn, string cmdText = null, Action<SqlParameterCollection> mapper = null)
        {
            SqlCommand command = null;

            if (conn != null)
            {
                command = conn.CreateCommand();
            }

            if (command != null)
            {
                if (!String.IsNullOrEmpty(cmdText))
                {
                    command.CommandText = cmdText;
                    command.CommandType = CommandType.StoredProcedure;
                }

                if (mapper != null)
                {
                    mapper(command.Parameters);
                }
            }

            return command;
        }
    }
}
