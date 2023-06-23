using System;
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
            SessionCheckUtilities utilities = new SessionCheckUtilities();

            var loginUser = loginTextBox.Text;
            var passwordUser = passwordBox.Password;
            string regDate = DateTime.Now.ToString();

            if (string.IsNullOrWhiteSpace(loginUser) || string.IsNullOrWhiteSpace(passwordUser))
            {
                MessageBox.Show("Введите логин и пароль.", "Что - то не так...", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (utilities.CheckUser(loginUser))
            {
                return;
            }

            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "INSERT INTO Users(login, password, is_admin, isAutorized, reg_date) values(@Login, @Password, 0, 1, @RegDate)";

                SqlCommand command = new SqlCommand(query, connection);

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

                Task task = new Task(() => { utilities.WriteUID(-1); });

                task.Wait(100);
                task.Start();

                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "UPDATE Users SET isAutorized = 0 WHERE id = @UID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}