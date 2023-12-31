﻿using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для MixedQuiz.xaml
    /// </summary>
    public partial class MixedQuiz : Page
    {
        private MixedSettings settings = new MixedSettings();

        private List<RadioButton> radioButtons;

        private DispatcherTimer timer;

        private int secondsElapsed;
        private int points;
        private int answersOffset;
        private int maxCount;

        private int numberQuestion = 1;
        private int textNumberQuestion = 0;

        public MixedQuiz()
        {
            InitializeComponent();

            radioButtons = new List<RadioButton>() { { firstAnswer }, { secondAnswer }, { thirdAnswer }, { fourthAnswer } };
            maxCount = settings.MaxCount;
            points = 0;

            InitializeQuiz();
        }

        private void InitializeQuiz()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = settings.MaxCount;

            pointsTextBox.Text = $"Кол-во баллов: {points}";

            questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
            questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

            for (int i = 0; i < radioButtons.Count; i++)
            {
                (radioButtons[i].Content as TextBlock).Text = settings.gameAnswers.ElementAt(i);
            }

            StartTimer();
        }

        private void AnswerButtonClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < radioButtons.Count; i++)
            {
                if (radioButtons[i].IsChecked == true & (radioButtons[i].Content as TextBlock).Text == settings.gameQuestions.ElementAt(textNumberQuestion).Key)
                {
                    timer.Stop();

                    points += settings.MaxPoints;
                    pointsTextBox.Text = $"Кол-во баллов: {points}";

                    radioButtons[i].Background = Brushes.LimeGreen;
                    (radioButtons[i].Content as TextBlock).Foreground = Brushes.Lime;

                    answerButton.Visibility = Visibility.Hidden;
                    continueButton.Visibility = Visibility.Visible;

                    isFinish();

                    break;
                }

                if (radioButtons[i].IsChecked == true & (radioButtons[i].Content as TextBlock).Text != settings.gameQuestions.ElementAt(textNumberQuestion).Key)
                {
                    timer.Stop();

                    foreach (var item in radioButtons)
                    {
                        if (settings.gameQuestions.ContainsKey((item.Content as TextBlock).Text))
                        {
                            (item.Content as TextBlock).Foreground = Brushes.LimeGreen;
                            break;
                        }
                    }

                    if (points != 0) { points -= (settings.MaxPoints); pointsTextBox.Text = $"Кол-во баллов: {points}"; }

                    radioButtons[i].Background = Brushes.Red;
                    (radioButtons[i].Content as TextBlock).Foreground = Brushes.Red;

                    answerButton.Visibility = Visibility.Hidden;
                    continueButton.Visibility = Visibility.Visible;

                    isFinish();

                    break;
                }
            }
        }

        private void isFinish()
        {
            if (maxCount == 0)
            {
                continueButton.Visibility = Visibility.Hidden;
                answerButton.Visibility = Visibility.Hidden;

                SaveResult();

                MessageBox.Show($"Вы прошли викторину!\n\nКол-во баллов: {points}\nРезультат сохранён.", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainPage());
            }
        }

        private void ContinueMethod()
        {
            textNumberQuestion++;
            numberQuestion++;
            answersOffset += 4;
            maxCount--;

            answerButton.Visibility = Visibility.Visible;
            continueButton.Visibility = Visibility.Hidden;

            questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
            questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].IsChecked = false;
                radioButtons[i].Background = Brushes.White;
                (radioButtons[i].Content as TextBlock).Foreground = Brushes.White;
                (radioButtons[i].Content as TextBlock).Text = settings.gameAnswers.ElementAt(i + answersOffset);
            }

            progressBar.Value++;
            StartTimer();
        }

        private void ContinueButtonClick(object sender, RoutedEventArgs e)
        {
            ContinueMethod();
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            secondsElapsed = settings.MaxTime;
            UpdateTimeText();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsElapsed--;

            if (secondsElapsed < 0)
            {
                timer.Stop();
                MessageBox.Show("Вы не успели!");
                ContinueMethod();
            }
            else
            {
                UpdateTimeText();
            }
        }

        private void UpdateTimeText()
        {
            timeTextBox.Text = $"Осталось времени: {secondsElapsed} сек.";
        }

        private void SaveResult()
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

            string query = "UPDATE Users SET mixed_result = mixed_result + @Result, sum_result = mixed_result + biology_result + geography_result + history_result WHERE id = @UID";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@Result", points);
            command.Parameters.AddWithValue("@UID", uid);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}