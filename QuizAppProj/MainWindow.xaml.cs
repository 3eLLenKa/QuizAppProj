using QuizAppProj.Autorization;
using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;

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
            //FileInfo info = new FileInfo(@"D:\session.txt");

            //if (!info.Exists) { File.WriteAllText(@"D:\session.txt", "0"); }

            try
            {
                window = this;

                InitializeComponent();

                //if (!CheckSession())
                //{
                    MainFrame.Content = new MainPage();
                //}
                //else MainFrame.Content = new MainPage();
            }
            catch (Exception)
            {
                DataBaseUtilities utilities = new DataBaseUtilities();
                string uid = utilities.ReadUID();

                MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

                connection.Open();

                string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

                MySqlCommand command = new MySqlCommand(query, connection);

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
            try
            {
                DataBaseUtilities utilities = new DataBaseUtilities();
                string uid = utilities.ReadUID();

                MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

                connection.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE id = @UID AND isAutorized = 1";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);

                int count = Convert.ToInt32(command.ExecuteScalar());

                if (count > 0)
                {
                    connection.Close();
                    return true;
                }
                else { connection.Close(); return false; }
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте подключение к интернету!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown();
                return false;
            }
        }
    }
}