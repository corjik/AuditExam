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


        public DateTime Date { get; set; }


        public int RightAnswer { get; set; } //Верный ответ из Questions
        public int QuestionId { get; set; } //QuestionID из базы Questions
        public string QuestionText { get; set; }
        public Question Question { get; set; }

        public ICollection<Question> Questions { get; set; }

    }

}
