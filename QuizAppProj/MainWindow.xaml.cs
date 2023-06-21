using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
            window = this;

            InitializeComponent();
            if (!CheckSession())
            {
                MainFrame.Content = new LoginForm();
            }
            else MainFrame.Content = new MainPage();
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
            StreamReader reader = new StreamReader(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Autorization\QuizAppUID.txt");

            string uid = reader.ReadLine();

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
