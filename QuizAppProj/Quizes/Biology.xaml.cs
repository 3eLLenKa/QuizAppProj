using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Threading;
using System.Windows.Shapes;
using System.Runtime.InteropServices.ComTypes;
using System.Data.SqlClient;
using QuizAppProj.Autorization;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для Biology.xaml
    /// </summary>
    public partial class Biology : Page
    {
        private List<CheckBox> customCheckedBoxes = new List<CheckBox>(3);
        private List<CheckBox> checkBoxes;
        public Biology()
        {
            InitializeComponent();
            checkBoxes = new List<CheckBox>()
            {
                { easyQuestionsCheckBox },
                { normalQuestionsCheckBox },
                { hardQuestionsCheckBox },
                { easyTimeCheckBox },
                { normalTimeCheckBox },
                { hardTimeCheckBox },
                { easyCountCheckBox },
                { normalCountCheckBox },
                { hardCountCheckBox }
            };
            easyPresrt.IsChecked = true;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Настройки сложности вопросов

        private void easyQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void normalQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void hardQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyQuestionsCheckBox.IsChecked = false;
            normalQuestionsCheckBox.IsChecked = false;
        }

        //Настройки кол-ва времени на ответ

        private void easyTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void normalTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void hardTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyTimeCheckBox.IsChecked = false;
            normalTimeCheckBox.IsChecked = false;
        }

        //Настройки кол-ва вопросов

        private void easyCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void normalCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void hardCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyCountCheckBox.IsChecked = false;
            normalCountCheckBox.IsChecked = false;
        }

        //Пресеты сложности

        private void easyPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            easyQuestionsCheckBox.IsChecked = true;
            easyTimeCheckBox.IsChecked = true;
            easyCountCheckBox.IsChecked = true;
        }

        private void normalPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            normalQuestionsCheckBox.IsChecked = true;
            normalTimeCheckBox.IsChecked = true;
            normalCountCheckBox.IsChecked = true;
        }

        private void hardPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            hardQuestionsCheckBox.IsChecked = true;
            hardTimeCheckBox.IsChecked = true;
            hardCountCheckBox.IsChecked = true;
        }

        private void customPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Visible;

            List<string> list = new List<string>(3);

            SessionCheckUtilities utilities = new SessionCheckUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);

            connection.Open();

            string query = $"SELECT biology_questions_setting, biology_time_setting, biology_count_setting FROM Users WHERE id = {uid}";

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            reader.Read();

            list.Add(reader[0].ToString());
            list.Add(reader[1].ToString());
            list.Add(reader[2].ToString());      
           
            reader.Close();
            connection.Close();

            foreach (CheckBox item in checkBoxes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (item.Name == list[i])
                    {
                        item.IsChecked = true;
                    }
                }
            }
        }

        private void saveSettingsClick(object sender, RoutedEventArgs e)
        {
            int count = 0;

            for (int i = 0; i < checkBoxes.Count; i++)
            {
                if (checkBoxes[i].IsChecked == true)
                {
                    customCheckedBoxes.Add(checkBoxes[i]);
                    count++;
                }
            }

            if (count == 3)
            {
                SessionCheckUtilities utilities = new SessionCheckUtilities();
                string uid = utilities.ReadUID();

                SqlConnection connection = new SqlConnection(utilities.ConnectionString);
                connection.Open();

                string query = "UPDATE Users SET biology_questions_setting = @Questions, biology_time_setting = @Time, biology_count_setting = @Count WHERE id = @UID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@UID", uid);
                command.Parameters.AddWithValue("@Questions", customCheckedBoxes[0].Name);
                command.Parameters.AddWithValue("@Time", customCheckedBoxes[1].Name);
                command.Parameters.AddWithValue("@Count", customCheckedBoxes[2].Name);

                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Ваши настройки сохранены!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                
                customCheckedBoxes.Clear();
            }
            else { MessageBox.Show("Вы выбрали не все настройки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); customCheckedBoxes.Clear(); }
        }
    }
}