using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows.Controls;

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

            Dictionary<string, int> mas = new Dictionary<string, int>();

            using (MySqlConnection connection = new MySqlConnection(utilities.ConnectionString))
            {
                connection.Open();

                string query = "SELECT login, sum_result FROM Users ORDER BY sum_result DESC LIMIT 3;";

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

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
