using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuizAppProj.Autorization
{
    internal class ConnectToDatabase
    {
        private const string connectionString = @"Data Source=DESCTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True";
        private readonly SqlConnection connection = new SqlConnection(connectionString);

        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnectionString()
        {
            return connection;
        }
    }
}
