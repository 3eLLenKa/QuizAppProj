using QuizAppProj.Quizes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
