using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QuizAppProj.Quizes
{
    internal class BiologySettings : Biology
    {
        public int maxPoints;
        public int maxTime;
        public int maxCount;

        public readonly Dictionary<string, string> gameQuestions = new Dictionary<string, string>(); //Ключ - ответ, значение - вопрос
        public readonly List<string> gameAnswers = new List<string>();

        private Dictionary<string, string> easyQuestions = new Dictionary<string, string>(15)
        {
            
        };
        private Dictionary<string, string> normalQuestions = new Dictionary<string, string>(15)
        {

        };
        private Dictionary<string, string> hardQuestions = new Dictionary<string, string>(15)
        {

        };

        private List<string> easyAnswers = new List<string>()
        {

        };
        private List<string> normalAnswers = new List<string>()
        {

        };
        private List<string> hardAnswers = new List<string>()
        {

        };
        public BiologySettings()
        {
            for (int i = 0; i < settings.Count; i++)
            {
                switch (settings[i])
                {
                    case "easyQuestionsCheckBox":
                        gameQuestions = easyQuestions;
                        gameAnswers = easyAnswers;
                        maxPoints = 1;
                        break;
                    case "normalQuestionsCheckBox":
                        gameQuestions = normalQuestions;
                        maxPoints = 3;
                        break;
                    case "hardQuestionsCheckBox":
                        gameQuestions = hardQuestions;
                        maxPoints = 5;
                        break;
                    case "easyTimeCheckBox":
                        maxTime = 30;
                        break;
                    case "normalTimeCheckBox":
                        maxTime = 20;
                        break;
                    case "hardTimeCheckBox":
                        maxTime = 15;
                        break;
                    case "easyCountCheckBox":
                        maxCount = 5; 
                        break;
                    case "normalCountCheckBox":
                        maxCount = 10;
                        break;
                    case "hardCountCheckBox":
                        maxCount = 15;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}