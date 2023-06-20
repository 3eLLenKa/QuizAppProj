using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizAppProj.Utilities;

namespace QuizAppProj.ViewModel
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void User(object obj) => CurrentView = new UserVM();
        private void CreateQuiz(object obj) => CurrentView = new CreateQuizVM();
        private void Settings(object obj) => CurrentView = new SettingsVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(User);
            OrdersCommand = new RelayCommand(CreateQuiz);
            SettingsCommand = new RelayCommand(Settings);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}