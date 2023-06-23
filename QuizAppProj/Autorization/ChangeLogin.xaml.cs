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

namespace QuizAppProj.Autorization
{
    /// <summary>
    /// Логика взаимодействия для ChangeLogin.xaml
    /// </summary>
    public partial class ChangeLogin : Page
    {
        public ChangeLogin()
        {
            InitializeComponent();
        }

        private void ChangeEventArg(object sender, EventArgs e)
        {
            SessionCheckUtilities utilities = new SessionCheckUtilities();

            var newLogin = loginTextBox.Text.Trim();
            var uid = utilities.ReadUID();

            if (utilities.CheckUser(newLogin))
            {
                return;
            }

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");
            connection.Open();

            string query = "UPDATE Users SET login = @NewLogin WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@NewLogin", newLogin);

            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show($"Ваш логин успешно изменен на {newLogin}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new LoginForm());
        }
    }
}
