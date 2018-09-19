using AuditExams.Models;
using System;
using System.Linq;

namespace AuditExams
{
    public class SampleData
    {
        public static void Initialize(ExamContext context)
        {
            if (!context.Questions.Any())
            {
                Random rnd = new Random();
                for (int i = 1; i < 100; i++)
                {
                    Question temp = new Question();
                    temp.Text = "Вопрос" + i;
                    temp.Variant1 = "Q" + i + " V1";
                    temp.Variant2 = "Q" + i + " V2";
                    temp.Variant3 = "Q" + i + " V3";
                    temp.RightAnswer = rnd.Next(1, 4);
                    temp.Course = "C" + rnd.Next(1, 4);

                    context.Questions.Add(temp);

                };

                context.SaveChanges();
            }
        }


        //public static void Initialize(UserContext context)
        //{
        //    if (!context.Users.Any())
        //    {
        //        context.Users.AddRange(
        //            new User
        //            {
        //                Email = "admin@test.ru",
        //                LastName = "Admin",
        //                FirstName = "Admin",
        //                Password = "123"
        //            });
        //    }

        //    context.SaveChanges();
        //}

    }
}
