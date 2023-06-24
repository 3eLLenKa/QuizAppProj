using System;
using System.Collections.Generic;
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

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для Biology.xaml
    /// </summary>
    public partial class Biology : Page
    {
        private List<CheckBox> customCheckedBoxes = new List<CheckBox>();

        private List<CheckBox> easyCheckedBoxes = new List<CheckBox>();
        private List<CheckBox> normalCheckedBoxes = new List<CheckBox>();
        private List<CheckBox> hardCheckedBoxes = new List<CheckBox>();

        private bool isPreset = false;
        public Biology()
        {
            InitializeComponent();

            if (easyCheckedBoxes.Count < 3 | normalCheckedBoxes.Count < 3 | hardCheckedBoxes.Count < 3) 
            {
                customPresrt.IsChecked = true;
            }
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Настройки сложности вопросов

        private void easyQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                easyCheckedBoxes.Add(easyQuestionsCheckBox);

                normalCheckedBoxes.Remove(normalQuestionsCheckBox);
                hardCheckedBoxes.Remove(hardQuestionsCheckBox);
            }

            normalQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void normalQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                normalCheckedBoxes.Add(normalQuestionsCheckBox);

                easyCheckedBoxes.Remove(easyQuestionsCheckBox);
                hardCheckedBoxes.Remove(hardQuestionsCheckBox);
            }

            easyQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void hardQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                hardCheckedBoxes.Add(hardQuestionsCheckBox);

                easyCheckedBoxes.Remove(easyQuestionsCheckBox);
                normalCheckedBoxes.Remove(normalQuestionsCheckBox);
            }


            easyQuestionsCheckBox.IsChecked = false;
            normalQuestionsCheckBox.IsChecked = false;
        }

        //Настройки кол-ва времени на ответ

        private void easyTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                easyCheckedBoxes.Add(easyTimeCheckBox);

                normalCheckedBoxes.Remove(normalTimeCheckBox);
                hardCheckedBoxes.Remove(hardTimeCheckBox);
            }


            normalTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void normalTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                normalCheckedBoxes.Add(normalTimeCheckBox);

                easyCheckedBoxes.Remove(easyTimeCheckBox);
                hardCheckedBoxes.Remove(hardTimeCheckBox);
            }


            easyTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void hardTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                hardCheckedBoxes.Add(hardTimeCheckBox);

                easyCheckedBoxes.Remove(easyTimeCheckBox);
                normalCheckedBoxes.Remove(normalTimeCheckBox);
            }


            easyTimeCheckBox.IsChecked = false;
            normalTimeCheckBox.IsChecked = false;
        }

        //Настройки кол-ва вопросов

        private void easyCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                easyCheckedBoxes.Add(easyCountCheckBox);

                normalCheckedBoxes.Remove(normalCountCheckBox);
                hardCheckedBoxes.Remove(hardCountCheckBox);
            }


            normalCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void normalCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                normalCheckedBoxes.Add(normalCountCheckBox);

                hardCheckedBoxes.Remove(hardCountCheckBox);
                easyCheckedBoxes.Add(hardCountCheckBox);
            }


            easyCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void hardCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!isPreset)
            {
                hardCheckedBoxes.Add(hardCountCheckBox);

                easyCheckedBoxes.Remove(easyCountCheckBox);
                hardCheckedBoxes.Remove(hardCountCheckBox);
            }


            easyCountCheckBox.IsChecked = false;
            normalCountCheckBox.IsChecked = false;
        }

        //Пресеты сложности

        private void easyPresrt_Checked(object sender, RoutedEventArgs e)
        {
            isPreset = true;

            easyQuestionsCheckBox.IsChecked = true;
            easyTimeCheckBox.IsChecked = true;
            easyCountCheckBox.IsChecked = true;
        }

        private void normalPresrt_Checked(object sender, RoutedEventArgs e)
        {
            isPreset = true;

            normalQuestionsCheckBox.IsChecked = true;
            normalTimeCheckBox.IsChecked = true;
            normalCountCheckBox.IsChecked = true;
        }

        private void hardPresrt_Checked(object sender, RoutedEventArgs e)
        {
            isPreset = true;

            hardQuestionsCheckBox.IsChecked = true;
            hardTimeCheckBox.IsChecked = true;
            hardCountCheckBox.IsChecked = true;
        }

        private void customPresrt_Checked(object sender, RoutedEventArgs e)
        {
            //customCheckedBoxes.AddRange(easyCheckedBoxes);
            //customCheckedBoxes.AddRange(normalCheckedBoxes);
            //easyCheckedBoxes.AddRange(hardCheckedBoxes);

            //if (!isPreset & (easyCheckedBoxes.Count < 3 | normalCheckedBoxes.Count < 3 | hardCheckedBoxes.Count < 3))
            //{
            //    for (int i = 0; i < customCheckedBoxes.Count; i++)
            //    {
            //        customCheckedBoxes[i].IsChecked = true;
            //    }
            //}
            //else
            //{
            //    customCheckedBoxes.ForEach(x => x.IsChecked = false);
            //}
        }
    }
}