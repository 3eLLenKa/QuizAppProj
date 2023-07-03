﻿using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            var loginUser = loginTextBox.Text.Trim();
            var passwordUser = passwordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBox.Show("Введите логин и пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DataBaseUtilities utilities = new DataBaseUtilities();
                SqlConnection connection = new SqlConnection(utilities.ConnectionString);

                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE login = @Login AND password = @Password AND isAutorized = 0";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Login", loginUser);
                command.Parameters.AddWithValue("@Password", passwordUser);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    int uid = utilities.GetUID(loginUser, passwordUser);
                    utilities.WriteUID(uid);

                    utilities.UpdateIsAutorized(loginUser);

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
                MessageBox.Show($"Проверьте подключение к интернету", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}