using QuizAppProj.Autorization;
using MySql.Data.MySqlClient;
using System.IO;

namespace QuizAppProj.Create
{
    internal class CreateInfo : CreationPage
    {
        public void WriteUID(string uid)
        {
            FileInfo fileInfo = new FileInfo(@"D:\quizuid.txt");

            if (!fileInfo.Exists) 
            { 
                fileInfo.Create();
            }
            else
            {
                using (StreamWriter writer = fileInfo.AppendText())
                {
                    writer.WriteLine(uid);
                    writer.Close();
                }
            }           
        }

        public string GetUID()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            MySqlConnection connection = new MySqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "select QuizID from UserQuizes where UserID = @UID, QuizName = @Name";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@Name", savedName.Text);

            string id = command.ExecuteScalar().ToString();

            return id;

        }
    }
}