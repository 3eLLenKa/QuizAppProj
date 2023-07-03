using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            DataBaseUtilities utilities = new DataBaseUtilities();

            var newLogin = loginTextBox.Text.Trim();
            var uid = utilities.ReadUID();

            utilities.WriteUID(-1);

            if (utilities.CheckUser(newLogin))
            {
                return;
            }

            MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "UPDATE Users SET login = @NewLogin where id = @UID;";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@NewLogin", newLogin);

            command.ExecuteNonQuery();
            connection.Close();

            MessageBox.Show($"Ваш логин успешно изменен на {newLogin}", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new LoginForm());
        }
    }
}
