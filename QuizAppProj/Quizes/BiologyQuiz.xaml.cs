using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public BiologyQuiz()
        {
            InitializeComponent();

            radioButtons = new List<RadioButton>() {{ firstAnswer }, {secondAnswer}, { thirdAnswer }, { fourthAnswer }};

            InitializeQuiz();
        }

        private void InitializeQuiz()
        {
            questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
            questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

            for (int i = 0; i < radioButtons.Count; i++)
            {
                radioButtons[i].Content = settings.gameQuestions.ElementAt(i).Key;
            }
        }

        private void AnswerButtonClick(object sender, RoutedEventArgs e)
        {
            if (maxCount != 0)
            {
                for (int i = 0; i < radioButtons.Count; i++)
                {
                    if (radioButtons[i].IsChecked == true & radioButtons[i].Content.ToString() == settings.gameQuestions.ElementAt(textNumberQuestion).Key)
                    {
                        maxCount--;
                        radioButtons[i].Background = Brushes.LimeGreen;

                        points += settings.MaxPoints;
                        temp += 4;
                        textNumberQuestion++;
                        numberQuestion++;

                        Thread.Sleep(500);

                        break;
                    }
                }

                questionNumberTextBox.Text = $"Вопрос #{numberQuestion}";
                questionTextBox.Text = settings.gameQuestions.ElementAt(textNumberQuestion).Value;

                for (int i = 0; i < radioButtons.Count; i++)
                {
                    radioButtons[i].Content = settings.gameAnswers.ElementAt(i + temp);
                }
            }
            else 
            {
                MessageBox.Show("Вы прошли викторину!", "Ура!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new MainPage());
            }
        }
    }
}