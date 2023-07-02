using QuizAppProj.Autorization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAppProj.ViewModel
{
    internal class UserVM
    {
        public string UserID { get; set; }
        public string DateReg { get; set; }
        public string Login { get; set; }
    }
}