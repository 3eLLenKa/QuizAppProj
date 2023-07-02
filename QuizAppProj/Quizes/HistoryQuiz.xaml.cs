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
using System.Windows.Threading;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для HistoryQuiz.xaml
    /// </summary>
    public partial class HistoryQuiz : Page
    {
        private HistorySettings settings = new HistorySettings();

        private List<RadioButton> radioButtons;

        private DispatcherTimer timer;

        private int secondsElapsed;
        private int points;
        private int answersOffset;
        private int maxCount;

        private int numberQuestion = 1;
        private int textNumberQuestion = 0;

        public HistoryQuiz()
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

                    points += settings.MaxPoints;
                    pointsTextBox.Text = $"Кол-во баллов: {points}";

                    radioButtons[i].Background = Brushes.LimeGreen;
                    radioButtons[i].Foreground = Brushes.Lime;

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
                        if (settings.gameQuestions.ContainsKey(item.Content.ToString()))
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
                radioButtons[i].Foreground = Brushes.White;
                radioButtons[i].Content = settings.gameAnswers.ElementAt(i + answersOffset);
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

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "UPDATE Users SET history_result = history_result + @Result WHERE id = @UID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Result", points);
            command.Parameters.AddWithValue("@UID", uid);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
