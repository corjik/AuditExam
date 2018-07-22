using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AuditExams.Models;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;

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


        public IActionResult Exam()
        {
            return View(db.Questions.ToList());
        }

        [HttpPost]
        public string SendResult(Result[] result)
        {
            foreach (Result el in result)
            {
                el.Date = DateTime.Now;
                el.RightAnswer = 2; //подтянуть из БД
                el.QuestionText = db.Questions.Include(p => p.QuestionText);
                db.Results.Add(el);
                db.SaveChanges();
            }

            return "Выполнено";

        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
