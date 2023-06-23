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
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ChangeLogin(object sender, EventArgs e)
        {

        }

        private void ChangePassword(object sender, EventArgs e)
        {

        }

        private void LeaveAccount(object sender, EventArgs e)
        {
            SessionCheckUtilities utilities = new SessionCheckUtilities();

            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.ExecuteNonQuery();

            connection.Close();

            NavigationService.GetNavigationService(this).Navigate(new LoginForm());
        }

        private void DeleteAccount(object sender, EventArgs e)
        {

        }
    }
}
