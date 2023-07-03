using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ReadQuizID()
        {
            DataBaseUtilities utilities = new DataBaseUtilities();
            string uid = utilities.ReadUID();

            SqlConnection connection = new SqlConnection(utilities.ConnectionString);
            connection.Open();

            string query = "select QuizID from UserQuizes where UserID = @UID, QuizName = @Name";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UID", uid);
            command.Parameters.AddWithValue("@Name", savedName.Text);

            string id = command.ExecuteScalar().ToString();

            return id;

        }
    }
}