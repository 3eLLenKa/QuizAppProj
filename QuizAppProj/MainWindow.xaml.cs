﻿using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

namespace QuizAppProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow window;
        public MainWindow()
        {
            FileInfo info = new FileInfo(@"D:\session.txt");

            if (!info.Exists) { File.WriteAllText(@"D:\session.txt", "0"); }

            try
            {
                window = this;

                InitializeComponent();

                if (!CheckSession())
                {
                    MainFrame.Content = new LoginForm();
                }
                else MainFrame.Content = new MainPage();
            }
            catch (Exception)
            {
                DataBaseUtilities utilities = new DataBaseUtilities();
                string uid = utilities.ReadUID();

                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }

        private bool CheckSession()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

            connection.Open();

            string query = "SELECT COUNT(*) FROM Users WHERE id = @UID AND isAutorized = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);

            int count = (int)command.ExecuteScalar();

            if (count > 0)
            {
                connection.Close();
                return true;
            }
            else { connection.Close(); return false; }
        }
    }
}