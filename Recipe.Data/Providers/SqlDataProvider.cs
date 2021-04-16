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

        public void Get(string storedProc)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                var command = new SqlCommand(storedProc, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"\t{reader[0]}\t{reader[1]}\t{reader[2]}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}
