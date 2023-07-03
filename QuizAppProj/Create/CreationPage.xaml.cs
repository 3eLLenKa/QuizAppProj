using System;
using QuizAppProj.Autorization;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QuizAppProj.Create
{
    /// <summary>
    /// Логика взаимодействия для CreationPage.xaml
    /// </summary>
    public partial class CreationPage : Page
    {
        private int count = 0;

        private List<string> answers;
        private List<string> correctAnswers = new List<string>();
        private List<string> questions = new List<string>();
        public CreationPage()
        {
            InitializeComponent();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(quizNameTextBox.Text) | string.IsNullOrWhiteSpace(quizNameTextBox.Text) | string.IsNullOrEmpty(countAnswers.Text) | string.IsNullOrWhiteSpace(countAnswers.Text))
            {
                MessageBox.Show("Вы заполнили не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            { 
                savedName.Text = quizNameTextBox.Text;
                savedCount.Text = countAnswers.Text;

                answers = new List<string>(int.Parse(savedCount.Text));

                saveButton.Visibility = Visibility.Hidden;

                quizNameTextBox.Visibility = Visibility.Hidden;
                countAnswers.Visibility = Visibility.Hidden;

                continueButton.Visibility = Visibility.Visible;
                countTextBlock.Visibility = Visibility.Visible;
                savedCount.Visibility = Visibility.Visible;
                savedName.Visibility = Visibility.Visible;

                questionAddTextBlock.Visibility = Visibility.Visible;
                questionAddTextBox.Visibility = Visibility.Visible;

                answerAddTextBlock.Visibility = Visibility.Visible;
                answerAddTextBox.Visibility = Visibility.Visible;
            }
        }

        private void ContinueClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(answerAddTextBox.Text) | string.IsNullOrWhiteSpace(answerAddTextBox.Text) | string.IsNullOrEmpty(questionAddTextBox.Text) | string.IsNullOrWhiteSpace(questionAddTextBox.Text))
            {
                MessageBox.Show("Вы не вписали вопрос и/или ответы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (count < int.Parse(savedCount.Text))
                {
                    string[] tempAnswers = answerAddTextBox.Text.Trim().Split(',');

                    questions.Add(questionAddTextBox.Text.Trim());

                    if (tempAnswers.Length != 4)
                    {
                        MessageBox.Show("Вы ввели не то количесвто ответов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    {
                        string[] temp = tempAnswers;

                        for (int i = 0; i < temp.Length; i++)
                        {
                            string temp1 = temp[i].Replace("+", string.Empty);
                            temp[i] = temp1;
                        }

                        if (temp.Length != temp.Distinct().Count())
                        {
                            MessageBox.Show("Ответы не могут повторяться!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    foreach (string item in tempAnswers)
                    {
                        if (item.Contains("+")) { correctAnswers.Add(item.Replace("+", string.Empty).Trim()); break; }
                    }

                    tempAnswers = answerAddTextBox.Text.Trim().Replace("+", string.Empty).Split(',');

                    foreach (var item in tempAnswers)
                    {
                        answers.Add(item.Trim());
                    }

                    count++;
                    countTextBlock.Text = $"Сохранено вопросов: {count}";

                    questionAddTextBox.Text = string.Empty;
                    answerAddTextBox.Text = string.Empty;
                }
                else
                {
                    questionAddTextBlock.Visibility = Visibility.Hidden;
                    questionAddTextBox.Visibility = Visibility.Hidden;

                    answerAddTextBlock.Visibility = Visibility.Hidden;
                    answerAddTextBox.Visibility = Visibility.Hidden;

                    publishButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void PublishClick(object sender, RoutedEventArgs e)
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "Insert into UserQuizes(QuizName, UserID, Questions, CorrectAnswers, Answers) values(@Name, @UID, @Questions, @Correct, @Answers)";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", quizNameTextBox.Text);
            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@Questions", questions.SelectMany(val => Encoding.UTF8.GetBytes(val)).ToArray());
            command.Parameters.AddWithValue("@Correct", correctAnswers.SelectMany(val => Encoding.UTF8.GetBytes(val)).ToArray());
            command.Parameters.AddWithValue("@Answers", answers.SelectMany(val => Encoding.UTF8.GetBytes(val)).ToArray());

            command.ExecuteNonQuery();

            connection.Close();

            MessageBox.Show("Викторина опубликована!\nПерейдите во вкладку \"Пользовательские квизы\"", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.Navigate(new MainPage());
        }
    }
}