using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuditExams.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace AuditExams.Controllers
{
    public class HomeController : Controller
    {
        ExamContext db;
        public HomeController(ExamContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Exam(string course)
        {
            ViewBag.Course = course;
            return View();
        }

        [HttpGet]
        public JsonResult GenerateQuestionList(string course)
        {
            IEnumerable<Question> questions;
            if (course == "Общий")
            {
                questions = db.Questions;
            }
            else
            {
                questions = db.Questions
                            .Where(p => p.Course == course);
            };

            Random rnd = new Random();
            List<Question> questionList = questions.ToList();

            //Случайный выбор вопросов
            for (int i = questionList.Count - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                var temp = questionList[j];
                questionList[j] = questionList[i];
                questionList[i] = temp;
            }

            var sortedUsers = from u in questionList
                              orderby u.Id
                              select u;

            return Json(sortedUsers.Take(10));
        }

        [HttpGet]
        public JsonResult ReloadData (string qList)
        {
            Dictionary<string, string> dicData = JsonConvert.DeserializeObject<Dictionary<string, string>>(qList);
            List<string> listData = dicData.Keys.ToList();

            List<Question> questionsList = new List<Question>();
            for (int i = 0; i< listData.Count; i++)
            {
                if (listData[i] != "course")
                {
                    Question temp = db.Questions
                        .Where(p => p.Id == Convert.ToInt32(listData[i]))
                        .FirstOrDefault();
                    questionsList.Add(temp);
                };
            };

            var sortedQ = from u in questionsList
                          orderby u.Id
                          select u;

            return Json(sortedQ);
        }

        [HttpPost]
        public JsonResult SendResult(Result[] result)
        {
            foreach (Result el in result)
            {
                Question tmp = db.Questions
                   .Where(c => c.Id == el.QuestionId)
                   .FirstOrDefault();
                el.QuestionText = tmp.Text;
                el.RightAnswer = tmp.RightAnswer;
                el.Date = DateTime.Now;
                el.IsCorrect = el.AnswerNum == el.RightAnswer ? true : false;
                el.UserName = User.Identity.Name;
                db.Results.Add(el);
            };

            db.SaveChanges();
            var json = JsonConvert.SerializeObject(result);
            return Json(json);
        }
    }
}
