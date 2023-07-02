using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizAppProj.View
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            try
            {
                InitializeComponent();

                DataBaseUtilities utilities = new DataBaseUtilities();

                this.infoUID.Text += utilities.ReadUID();
                this.infoDate.Text += GetDateReg();
                this.infoLogin.Text += GetLogin();

                this.historyResult.Text += GetHistoryPoints();
                this.geographyResult.Text += GetGeographyPoints();
                this.biologyResult.Text += GetBiologyPoints();
                this.mixedResult.Text += GetMixedPoints();
            }
            catch (Exception)
            {
                DataBaseUtilities utilities = new DataBaseUtilities();
                string uid = utilities.ReadUID();

                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private string GetDateReg()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT reg_date FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string date = command.ExecuteScalar().ToString();

            connection.Close();

            return date;
        }
        private string GetLogin()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT login FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string login = command.ExecuteScalar().ToString();

            connection.Close();

            return login;
        }

        private string GetBiologyPoints()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT biology_result FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string result = command.ExecuteScalar().ToString();

            connection.Close();

            return result;
        }
        private string GetHistoryPoints()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT history_result FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string result = command.ExecuteScalar().ToString();

            connection.Close();

            return result;
        }
        private string GetGeographyPoints()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT geography_result FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string result = command.ExecuteScalar().ToString();

            connection.Close();

            return result;
        }
        private string GetMixedPoints()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = "SELECT mixed_result FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string result = command.ExecuteScalar().ToString();

            connection.Close();

            return result;
        }
    }
}