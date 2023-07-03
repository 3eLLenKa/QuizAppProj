using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace QuizAppProj.Autorization
{
    /// <summary>
    /// Логика взаимодействия для RegForm.xaml
    /// </summary>
    public partial class RegForm : Page
    {
        public RegForm()
        {
            InitializeComponent();
        }
        private void RegisterEventArg(object sender, EventArgs e)
        {
            DataBaseUtilities utilities = new DataBaseUtilities();

            var loginUser = loginTextBox.Text.Trim();
            var passwordUser = passwordBox.Password.Trim();
            string regDate = DateTime.Now.ToString();

            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBox.Show("Введите логин и пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (loginUser.Length > 10)
            {
                MessageBox.Show("Логин не должен быть больше 10 символов!", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (utilities.CheckUser(loginUser))
            {
                return;
            }

            try
            {
                MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

                connection.Open();

                string query = "INSERT INTO Users(login, password, is_admin, isAutorized, reg_date) values(@Login, @Password, 0, 1, @RegDate)";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@Login", loginUser);
                command.Parameters.AddWithValue("@Password", passwordUser);
                command.Parameters.AddWithValue("@RegDate", regDate);

                if (command.ExecuteNonQuery() == 1)
                {
                    int uid = utilities.GetUID(loginUser, passwordUser);
                    utilities.WriteUID(uid);

                    connection.Close();

                    MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new MainPage());
                }
                else { connection.Close(); MessageBox.Show("Аккаунт не создан!", "Что-то не так...", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            catch (Exception)
            {
                string uid = utilities.ReadUID();

                utilities.WriteUID(-1); 

                MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

                connection.Open();

                string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Проверьте подключение к интернету!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}