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
            InitializeComponent();

            SessionCheckUtilities utilities = new SessionCheckUtilities();

            this.infoUID.Text += utilities.ReadUID();
            this.infoDate.Text += GetDateReg();
            this.infoLogin.Text += GetLogin();
        }

        private string GetDateReg()
        {
            SessionCheckUtilities utilities = new SessionCheckUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

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
            SessionCheckUtilities utilities = new SessionCheckUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT login FROM Users WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            string login = command.ExecuteScalar().ToString();

            connection.Close();

            return login;
        }
    }
}