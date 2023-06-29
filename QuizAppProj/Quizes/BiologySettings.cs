using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuizAppProj.Quizes
{
    internal class BiologySettings : Biology
    {
        public List<CheckBox> questions;

        public int maxTime;
        public readonly int maxCount;

        public BiologySettings()
        {
            for (int i = 0; i < settings.Count; i++)
            {
                switch (settings[i])
                {
                    case "easyQuestionsCheckBox":
                        fillEasyQuestions();
                        break;
                    case "normalQuestionsCheckBox":
                        fillNormalQuestions();
                        break;
                    case "hardQuestionsCheckBox":
                        fillHardQuestions();
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

        private void fillEasyQuestions() { }
        private void fillNormalQuestions() { }
        private void fillHardQuestions() { }

    }
}