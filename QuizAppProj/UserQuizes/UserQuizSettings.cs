using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuizAppProj.Autorization;
using QuizAppProj.Create;

namespace QuizAppProj.UserQuizes
{
    internal class UserQuizSettings
    {
        public int MaxPoints { get; } = 1;
        public int MaxTime { get; } = 30;
        public int MaxCount { get; }
        public string QuizName { get; }

        public readonly Dictionary<string, string> gameQuestions = new Dictionary<string, string>(); //Ключ - ответ, значение - вопрос
        public readonly List<string> gameAnswers = new List<string>();

        //private void GetGameQuestions()
        //{

        //}

        //private string[] GetGameAnswers()
        //{
        //    DataBaseUtilities utilities = new DataBaseUtilities();

        //    CreateInfo info = new CreateInfo();
        //    string uid = info.GetUID();

        //    SqlConnection connection = new SqlConnection(utilities.ConnectionString);
        //    connection.Open();

        //    string query = "select Answers from UserQuizes where UserID = @UID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@UID", uid);

        //    string result = command.ExecuteScalar().ToString();
        //    return result;
        //}

        //private string[] GetCorrect()
        //{

        //}
    }
}
