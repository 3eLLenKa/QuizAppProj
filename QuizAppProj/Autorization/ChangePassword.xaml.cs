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
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Page
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangeEventArg(object sender, RoutedEventArgs e)
        {
            SessionCheckUtilities utilities = new SessionCheckUtilities();

            var newPassword = passwordTextBox.Text.Trim();
            var uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");
            connection.Open();

            string query = "UPDATE Users SET password = @NewPass WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@NewPass", newPassword);

            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show($"Ваш логин успешно изменен на {newPassword}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new LoginForm());
        }
    }
}
