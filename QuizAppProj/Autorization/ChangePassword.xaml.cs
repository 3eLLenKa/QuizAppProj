using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            DataBaseUtilities utilities = new DataBaseUtilities();

            var newPassword = passwordTextBox.Text.Trim();
            var uid = utilities.ReadUID();

            utilities.WriteUID(-1);

            MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "UPDATE Users SET password = @NewPass WHERE id = @UID";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@NewPass", newPassword);

            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show($"Ваш пароль успешно изменен на {newPassword}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new LoginForm());
        }
    }
}
