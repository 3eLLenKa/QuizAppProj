using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Threading;
using System.Windows.Shapes;
using System.Runtime.InteropServices.ComTypes;

namespace QuizAppProj.Quizes
{
    /// <summary>
    /// Логика взаимодействия для Biology.xaml
    /// </summary>
    public partial class Biology : Page
    {
        private List<CheckBox> customCheckedBoxes = new List<CheckBox>(3);
        private List<CheckBox> checkBoxes;
        public Biology()
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
            easyPresrt.IsChecked = true;
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        //Настройки сложности вопросов

        private void easyQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void normalQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyQuestionsCheckBox.IsChecked = false;
            hardQuestionsCheckBox.IsChecked = false;
        }

        private void hardQuestionsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyQuestionsCheckBox.IsChecked = false;
            normalQuestionsCheckBox.IsChecked = false;
        }

        //Настройки кол-ва времени на ответ

        private void easyTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void normalTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyTimeCheckBox.IsChecked = false;
            hardTimeCheckBox.IsChecked = false;
        }

        private void hardTimeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyTimeCheckBox.IsChecked = false;
            normalTimeCheckBox.IsChecked = false;
        }

        //Настройки кол-ва вопросов

        private void easyCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            normalCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void normalCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyCountCheckBox.IsChecked = false;
            hardCountCheckBox.IsChecked = false;
        }

        private void hardCountCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            easyCountCheckBox.IsChecked = false;
            normalCountCheckBox.IsChecked = false;
        }

        //Пресеты сложности

        private void easyPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            easyQuestionsCheckBox.IsChecked = true;
            easyTimeCheckBox.IsChecked = true;
            easyCountCheckBox.IsChecked = true;
        }

        private void normalPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            normalQuestionsCheckBox.IsChecked = true;
            normalTimeCheckBox.IsChecked = true;
            normalCountCheckBox.IsChecked = true;
        }

        private void hardPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Hidden;

            hardQuestionsCheckBox.IsChecked = true;
            hardTimeCheckBox.IsChecked = true;
            hardCountCheckBox.IsChecked = true;
        }

        private void customPresrt_Checked(object sender, RoutedEventArgs e)
        {
            saveSettings.Visibility = Visibility.Visible;

            List<string> list = new List<string>(3);

            StreamReader reader = new StreamReader(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Quizes\BiologySettings.txt");
            
            for (int i = 0; i < 3; i++)
            {
                list.Add(reader.ReadLine());
            }

            reader.Close();

            foreach (CheckBox item in checkBoxes)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (item.Name == list[i])
                    {
                        item.IsChecked = true;
                    }
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
                StreamWriter writer = new StreamWriter(@"C:\Users\alexk\source\repos\QuizAppProj\QuizAppProj\Quizes\BiologySettings.txt");
                for (int i = 0; i < customCheckedBoxes.Count; i++)
                {
                    writer.WriteLine(customCheckedBoxes[i].Name);
                }
                writer.Close();

                MessageBox.Show("Ваши настройки сохранены!\nВы можете их перезаписать, заново нажав на кнопку.\n\nПриложение перезапустится.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);

                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            else { MessageBox.Show("Вы выбрали не все настройки!"); customCheckedBoxes.Clear(); }
        }
    }
}