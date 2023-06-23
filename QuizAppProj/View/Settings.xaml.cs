using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace QuizAppProj.View
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void ChangeLogin(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите изменить логин?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                SessionCheckUtilities utilities = new SessionCheckUtilities();

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
                NavigationService.GetNavigationService(this).Navigate(new ChangeLogin());
            }
            else return;
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите изменить пароль?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                SessionCheckUtilities utilities = new SessionCheckUtilities();

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
                NavigationService.GetNavigationService(this).Navigate(new ChangePassword());
            }
            else return;
        }

        private void LeaveAccount(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите выйти?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (result == MessageBoxResult.Yes)
            {
                SessionCheckUtilities utilities = new SessionCheckUtilities();

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

                NavigationService.GetNavigationService(this).Navigate(new LoginForm());
            }
            else return;
        }

        private void DeleteAccount(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы точно хотите удалить аккаунт?\nОтменить это действие будет невозможно", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                SessionCheckUtilities utilities = new SessionCheckUtilities();

                string uid = utilities.ReadUID();

                Task task = new Task(() => { utilities.WriteUID(-1); });

                task.Wait(100);
                task.Start();

                SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-HCK9T1F\SQLEXPRESS;Initial Catalog=QuizDB;Integrated Security=True");

                connection.Open();

                string query = "DELETE FROM Users WHERE id = @UID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.ExecuteNonQuery();

                connection.Close();

                NavigationService.GetNavigationService(this).Navigate(new LoginForm());
            }
            else return;
        }
    }
}
