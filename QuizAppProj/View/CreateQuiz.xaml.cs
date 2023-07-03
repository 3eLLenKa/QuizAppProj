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
using QuizAppProj.Autorization;
using QuizAppProj.Create;

namespace QuizAppProj.View
{
    /// <summary>
    /// Логика взаимодействия для CreateQuiz.xaml
    /// </summary>
    public partial class CreateQuiz : UserControl
    {
        //List<TextBlock> buttonList;
        public CreateQuiz()
        {
            InitializeComponent();
            //buttonList = new List<TextBlock>() { firstSlot.Content as TextBlock, secondSlot.Content as TextBlock, thirdSlot.Content as TextBlock, fourthSlot.Content as TextBlock };

            //foreach (var item in buttonList)
            //{
            //    if (item.Text == "Untitled") item.Text = GetName();
            //    break;
            //}
        }

        private void FirstSlot(object sender, RoutedEventArgs e)
        {
            CreateInfo info = new CreateInfo();

            NavigationService.GetNavigationService(this).Navigate(new CreationPage());
        }

        private void SecondSlot(object sender, RoutedEventArgs e)
        {
            CreateInfo info = new CreateInfo();

            NavigationService.GetNavigationService(this).Navigate(new CreationPage());
        }

        private void ThirdSlot(object sender, RoutedEventArgs e)
        {
            CreateInfo info = new CreateInfo();

            NavigationService.GetNavigationService(this).Navigate(new CreationPage());
        }

        private void FourthSlot(object sender, RoutedEventArgs e)
        {
            CreateInfo info = new CreateInfo();

            NavigationService.GetNavigationService(this).Navigate(new CreationPage());
        }

        //private string GetName()
        //{
        //    CreateInfo createInfo = new CreateInfo();
        //    DataBaseUtilities utilities = new DataBaseUtilities();

        //    string uid = utilities.ReadUID();
        //    string quid = createInfo.ReadQuizID();

        //    SqlConnection connection = new SqlConnection(utilities.ConnectionString);
        //    connection.Open();

        //    string query = "select QuizName from UserQuizes where UserID = @UID, QuizID = @QUID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@UID", uid);
        //    command.Parameters.AddWithValue("@QUID", quid);

        //    string name = command.ExecuteScalar().ToString();

        //    connection.Close();
        //    return name;
        //}
    }
}