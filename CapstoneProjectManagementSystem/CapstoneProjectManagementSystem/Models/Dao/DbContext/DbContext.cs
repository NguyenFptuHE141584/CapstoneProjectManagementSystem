using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Models.Dao.DBContext
{
    public class DbContext
    {
        public static SqlConnection GetConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectionStr = config.GetConnectionString("connectstr");
            return new SqlConnection(connectionStr);
        }

        public static DataTable GetDataBySQL(string sql, SqlParameter[] parameters = null)
        {
            DataTable result = new DataTable();
            SqlConnection connection = GetConnection();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            //Start a local transaction
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            command.Connection = connection;
            try
            {
                if (parameters != null) command.Parameters.AddRange(parameters);
                command.CommandText = sql;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(result);

                //Attempt to commit the transaction
                transaction.Commit();
                Console.WriteLine("All sql querry run successfully !!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine(" Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // Handle any errors that may have occurred on the server
                    // that would cause the rollback to fail, such as a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }

            connection.Close();
            return result;
        }


        public static int ExecuteSQL(string sql, SqlParameter[] parameters = null)
        {
            int count = 0;
            SqlConnection connection = GetConnection();

            connection.Open();

            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            //Start a local transaction
            transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            command.Connection = connection;
            try
            {
                if (parameters != null) command.Parameters.AddRange(parameters);
                command.CommandText = sql;
                count = command.ExecuteNonQuery();

                //Attempt to commit the transaction
                transaction.Commit();
                Console.WriteLine("All sql querry run successfully !!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // Handle any errors that may have occurred on the server
                    // that would cause the rollback to fail, such as a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }


            connection.Close();

            return count;
        }
    }
}
