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
        private BiologySettings settings = new BiologySettings();

        private List<RadioButton> radioButtons;

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
            for (int i = 0; i < radioButtons.Count; i++)
            {
                if (radioButtons[i].IsChecked == true & (string)radioButtons[i].Content == settings.easyAnswers.ElementAt(textNumberQuestion))
                {
                    radioButtons[i].Background = Brushes.LimeGreen;

                    points += settings.maxPoints;
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
                radioButtons[i].Content = settings.easyAnswers.ElementAt(i + 4);
            }
        }
    }
}