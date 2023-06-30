﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для BiologyQuiz.xaml
    /// </summary>
    public partial class BiologyQuiz : Page
    {
        private static BiologySettings settings = new BiologySettings();

        private List<RadioButton> radioButtons;

        private int temp;
        private int maxCount = settings.MaxCount;
        private int points = 0;
        private int numberQuestion = 1;
        private int textNumberQuestion = 0;

        private DispatcherTimer timer;
        private int secondsElapsed;
        public BiologyQuiz()
        {
            InitializeComponent();

            radioButtons = new List<RadioButton>() {{ firstAnswer }, {secondAnswer}, { thirdAnswer }, { fourthAnswer }};

            InitializeQuiz();
        }

        private void InitializeQuiz()
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = settings.MaxCount;

            questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
            questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].Content = settings.gameAnswers.ElementAt(i);
            }

            StartTimer();
        }

        private void AnswerButtonClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < radioButtons.Count; i++)
            {
                if (radioButtons[i].IsChecked == true & radioButtons[i].Content.ToString() == settings.gameQuestions.ElementAt(textNumberQuestion).Key)
                {
                    timer.Stop();

                    radioButtons[i].Background = Brushes.LimeGreen;
                    radioButtons[i].Foreground = Brushes.Lime;

                    points += settings.MaxPoints;

                    answerButton.Visibility = Visibility.Hidden;
                    continueButton.Visibility = Visibility.Visible;

                    isFinish();

                    break;
                }

                if (radioButtons[i].IsChecked == true & radioButtons[i].Content.ToString() != settings.gameQuestions.ElementAt(textNumberQuestion).Key)
                {
                    timer.Stop();

                    foreach (var item in radioButtons)
                    {
                        if (settings.gameAnswers.Contains(item.Content))
                        {
                            item.Foreground = Brushes.LimeGreen;
                            break;
                        }
                    }

                    radioButtons[i].Background = Brushes.Red;
                    radioButtons[i].Foreground = Brushes.Red;

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

                MessageBox.Show("Вы прошли викторину!", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainPage());
            }
        }

        private void ContinueMethod()
        {
            textNumberQuestion++;
            numberQuestion++;
            temp += 4;
            maxCount--;

            answerButton.Visibility = Visibility.Visible;
            continueButton.Visibility = Visibility.Hidden;

            questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
            questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].IsChecked = false;
                radioButtons[i].Background = Brushes.White;
                radioButtons[i].Foreground = Brushes.White;
                radioButtons[i].Content = settings.gameAnswers.ElementAt(i + temp);
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
    }
}