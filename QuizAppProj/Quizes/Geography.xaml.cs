﻿using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для Geography.xaml
    /// </summary>
    public partial class Geography : Page
    {
        private List<CheckBox> checkBoxes;
        private List<CheckBox> customCheckedBoxes = new List<CheckBox>(3);

        protected static HashSet<string> settings = new HashSet<string>();
        public Geography()
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
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BiologyQuizNavigation(object sender, RoutedEventArgs e)
        {
            if (settings.Count < 3) { MessageBox.Show("Вы выбрали не все настройки!"); return; }
            else NavigationService.Navigate(new GeographyQuiz());
        }

        //Настройки сложности вопросов

        private void easyQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("easyQuestionsCheckBox");

            normalQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void normalQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("normalQuestionsCheckBox");

            easyQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void hardQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("hardQuestionsCheckBox");

            easyQuestionsCheckBox.IsChecked = false;
            normalQuestionsCheckBox.IsChecked = false;
        }

        //Настройки кол-ва времени на ответ

        private void easyTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("easyTimeCheckBox");

            normalTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void normalTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("normalTimeCheckBox");

            easyTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void hardTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("hardTimeCheckBox");

            easyTimeCheckBox.IsChecked = false;
            normalTimeCheckBox.IsChecked = false;
        }

        //Настройки кол-ва вопросов

        private void easyCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("easyCountCheckBox");

            normalCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void normalCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("normalCountCheckBox");

            easyCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void hardCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            settings.Add("hardCountCheckBox");

            easyCountCheckBox.IsChecked = false;
            normalCountCheckBox.IsChecked = false;
        }

        //Пресеты сложности

        private void easyPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            settings.Clear();

            settings.Add("easyQuestionsCheckBox");
            settings.Add("easyTimeCheckBox");
            settings.Add("easyCountCheckBox");

            easyQuestionsCheckBox.IsChecked = true;
            easyTimeCheckBox.IsChecked = true;
            easyCountCheckBox.IsChecked = true;
        }

        private void normalPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            settings.Clear();

            settings.Add("normalQuestionsCheckBox");
            settings.Add("normalTimeCheckBox");
            settings.Add("normalCountCheckBox");

            normalQuestionsCheckBox.IsChecked = true;
            normalTimeCheckBox.IsChecked = true;
            normalCountCheckBox.IsChecked = true;
        }

        private void hardPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            settings.Clear();

            settings.Add("hardQuestionsCheckBox");
            settings.Add("hardTimeCheckBox");
            settings.Add("hardCountCheckBox");

            hardQuestionsCheckBox.IsChecked = true;
            hardTimeCheckBox.IsChecked = true;
            hardCountCheckBox.IsChecked = true;
        }

        private void customPresrt_Checked(object sender, RoutedEventArgs e)
        {

            saveSettings.Visibility = Visibility.Visible;

            settings.Clear();

            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте подключение к интернету!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            string query = $"SELECT geography_questions_setting, geography_time_setting, geography_count_setting FROM Users WHERE id = {uid}";

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            reader.Read();

            settings.Add(reader[0].ToString());
            settings.Add(reader[1].ToString());
            settings.Add(reader[2].ToString());

            reader.Close();
            connection.Close();

            foreach (CheckBox item in checkBoxes)
            {
                if (settings.Contains(item.Name))
                {
                    item.IsChecked = true;
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
                DataBaseUtilities utilities = new DataBaseUtilities();
                string uid = utilities.ReadUID();

                MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);

                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте подключение к интернету!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                }

                string query = "UPDATE Users SET geography_questions_setting = @Questions, geography_time_setting = @Time, geography_count_setting = @Count WHERE id = @UID";

                MySqlCommand command = new MySqlCommand(query, connection);

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
