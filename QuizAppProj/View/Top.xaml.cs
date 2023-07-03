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
    /// Логика взаимодействия для Top.xaml
    /// </summary>
    public partial class Top : UserControl
    {
        public Top()
        {
            InitializeComponent();

            first.Text = $"#1 {GetTopUsers().ElementAt(0).Key} | {GetTopUsers().ElementAt(0).Value}";
            second.Text = $"#2 {GetTopUsers().ElementAt(1).Key} | {GetTopUsers().ElementAt(1).Value}";
            third.Text = $"#3 {GetTopUsers().ElementAt(2).Key} | {GetTopUsers().ElementAt(2).Value}";
        }

        private Dictionary<string, int> GetTopUsers()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            Dictionary<string, int> mas = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(utilities.ConnectionString))
            {
                connection.Open();

                string query = "SELECT TOP 3 login, sum_result FROM Users ORDER BY sum_result DESC;";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string login = reader["login"].ToString();
                    int points = int.Parse(reader["sum_result"].ToString());

                    mas.Add(login, points);
                }

                reader.Close();
            }

            return mas;
        }
    }
}
