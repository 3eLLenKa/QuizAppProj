using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;

namespace QuizAppProj.Autorization
{
    internal class SessionCheckUtilities
    {
        public int GetUID(object userLogin, object userPassword) 
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT id FROM Users WHERE login = @Login AND password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", userLogin);
            command.Parameters.AddWithValue("@Password", userPassword);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                int userId = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return userId;
            }
            else
            {
                connection.Close();
                return -1;
            }
        }

        public void WriteUID(int uid)
        {
            File.WriteAllText(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Autorization\QuizAppUID.txt", uid.ToString());
        }

        public string ReadUID()
        {
            StreamReader reader = new StreamReader(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Autorization\QuizAppUID.txt");

            string uid = reader.ReadLine();
            return uid;
        }

        public void UpdateIsAutorized(string userLogin)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "UPDATE Users SET isAutorized = 1 WHERE login = @Login";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", userLogin);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool CheckUser(string login)
        {
            login = login.Trim();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE login = @Login";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", login);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                connection.Close();
                MessageBox.Show($"Этот логин уже используется!", "Что-то не так...", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            else { connection.Close(); return false; }
        }
    }
}
