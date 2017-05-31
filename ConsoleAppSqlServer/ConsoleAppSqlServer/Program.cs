using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleAppSqlServer
{
    class Program
    {
        static Boolean checkTableExists(SqlConnection con, String tableName)
        {
            string sql = 
                "select 1 \n" +
                "from   sys.tables \n" +
                "join sys.schemas on schemas.schema_id = tables.schema_id \n" +
                "where schemas.name + '.' + tables.name = @tableName";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@tableName", tableName);
            Boolean exists = false;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    exists = true;
                }
            }
            return exists;
        }

        static void Main(string[] args)
        {
            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;

            connectionString = "Data Source=LAPTOP-SHP0CKIB\\RUPEN;Initial Catalog=rupen;User ID=sa;Password=n3wf0x";
            connection = new SqlConnection(connectionString);
            sql = "create table test01 (id int identity(1,1), val1 varchar(50), creation_dtm datetime default GETDATE())";
            try
            {
                connection.Open();
                Console.WriteLine(checkTableExists(connection, "dbo.test01"));
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
