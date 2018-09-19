using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditExams.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int AnswerNum { get; set; }

        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public DateTime Date { get; set; }
        public bool IsCorrect { get; set; }
        public string UserName { get; set; }

        //Данные из таблицы Questions
        public int RightAnswer { get; set; } 
        public string QuestionText { get; set; }
        public Question Question { get; set; }
    }

}
