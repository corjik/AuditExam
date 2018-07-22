using AuditExams.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditExams
{
    public class SampleData
    {
        public static void Initialize(ExamContext context)
        {
            if (!context.Questions.Any())
            {
                context.Questions.AddRange(
                    new Question
                    {
                        QuestionText = "Вопрос 1",
                        Variant1 = "Q1 V1",
                        Variant2 = "Q1 V2",
                        Variant3 = "Q1 V3",
                        RightAnswer = 1,
                        Course = "C1"
                    },
                    new Question
                    {
                        QuestionText = "Вопрос 2",
                        Variant1 = "Q2 V1",
                        Variant2 = "Q2 V2",
                        Variant3 = "Q2 V3",
                        RightAnswer = 1,
                        Course = "C3"
                    },
                    new Question
                    {
                        QuestionText = "Вопрос 3",
                        Variant1 = "Q3 V1",
                        Variant2 = "Q3 V2",
                        Variant3 = "Q3 V3",
                        RightAnswer = 1,
                        Course = "C2"
                    },
                    new Question
                    {
                        QuestionText = "Вопрос 4",
                        Variant1 = "Q4 V1",
                        Variant2 = "Q4 V2",
                        Variant3 = "Q4 V3",
                        RightAnswer = 1,
                        Course = "C1"
                    },
                    new Question
                    {
                        QuestionText = "Вопрос 5",
                        Variant1 = "Q5 V1",
                        Variant2 = "Q5 V2",
                        Variant3 = "Q5 V3",
                        RightAnswer = 1,
                        Course = "C3"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
