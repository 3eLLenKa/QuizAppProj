using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QuizAppProj.Autorization
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Page
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void RegFormNavigation(object sender, EventArgs e)
        {
            NavigationService.Navigate(new RegForm());
        }
        private void AutorizeEventArg(object sender, EventArgs e)
        {
            var loginUser = loginTextBox.Text;
            var passwordUser = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBox.Show("Введите логин и пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE login = @Login AND password = @Password AND isAutorized = 0";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Login", loginUser);
                command.Parameters.AddWithValue("@Password", passwordUser);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    SessionCheckUtilities utilities = new SessionCheckUtilities();

                    Task task = new Task(() => {
                        int uid = utilities.GetUID(loginUser, passwordUser);
                        utilities.WriteUID(uid);
                    });

                    utilities.UpdateIsAutorized(loginUser);

                    task.Wait(100);
                    task.Start();

                    connection.Close();

                    MessageBox.Show("Авторизация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new MainPage());
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Неверный логин или пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Проверьте подключение к интернету");
            }
        }
    }
}