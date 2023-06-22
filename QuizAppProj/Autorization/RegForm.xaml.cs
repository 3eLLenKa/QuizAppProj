﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
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
using System.Data.SqlClient;

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
            var loginUser = loginTextBox.Text;
            var passwordUser = passwordBox.Password;
            string regDate = DateTime.Now.ToString();

            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBox.Show("Введите логин и пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CheckUser())
            {
                return;
            }

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "INSERT INTO Users(login, password, is_admin, isAutorized, reg_date) values(@Login, @Password, 0, 1, @RegDate)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", loginUser);
            command.Parameters.AddWithValue("@Password", passwordUser);
            command.Parameters.AddWithValue("@RegDate", regDate);

            if (command.ExecuteNonQuery() == 1)
            {
                int uid = GetUID();

                File.WriteAllText(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Autorization\QuizAppUID.txt", uid.ToString());

                MessageBox.Show("Регистрация прошла успешно.", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainPage());
            }
            else MessageBox.Show("Аккаунт не создан!", "Что-то не так...", MessageBoxButton.OK, MessageBoxImage.Error);

            connection.Close();
        }

        private bool CheckUser()
        {
            var loginUser = loginTextBox.Text;
            var passwordUser = passwordBox.Password;

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE login = @Login AND password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", loginUser);
            command.Parameters.AddWithValue("@Password", passwordUser);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                connection.Close();
                MessageBox.Show($"{DateTime.Now}", "Что-то не так...", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            else { connection.Close(); return false; }
        }

        private int GetUID()
        {
            var loginUser = loginTextBox.Text;
            var passwordUser = passwordBox.Password;

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT id FROM Users WHERE login = @Login AND password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Login", loginUser);
            command.Parameters.AddWithValue("@Password", passwordUser);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                int userId = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return userId;
            }
            else
            {
                connection.Close();
                return -1;
            }
        }
    }
}