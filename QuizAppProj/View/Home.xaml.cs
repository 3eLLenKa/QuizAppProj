using QuizAppProj.Quizes;
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

namespace QuizAppProj.View
{
    /// <summary>
    /// Логика взаимодействия для Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
        }

        private void LoadHistoryQuiz(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new History());
        }

        private void LoadGeographyQuiz(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Geography());
        }

        private void LoadBiologyQuiz(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Biology());
        }

        private void LoadMixedQuiz(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Mixed());
        }
    }
}
