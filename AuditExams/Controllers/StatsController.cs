using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuditExams.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore.Internal;

namespace AuditExams.Controllers
{
    public class StatsController : Controller
    {
        ExamContext db;
        public StatsController(ExamContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetStatistic()
        {
            //IEnumerable<Result> resultList;
            //resultList = db.Results
            //             .Where(p => p.UserName == User.Identity.Name);

            var resultList = db.Results.Join(db.Questions, // второй набор
                            p => p.QuestionId, // свойство-селектор объекта из первого набора
                            c => c.Id,
                            (p, c) => new // результат
                            {
                                QId = p.QuestionId,
                                IsCorrect = p.IsCorrect,
                                Course = c.Course,
                                UserName = p.UserName,
                            });
            ResultReview stat = new ResultReview();

            stat.length = resultList.Count();
            stat.correctNumber = resultList.Where(c => c.IsCorrect == true).Count();
            stat.incorrectNumber = resultList.Where(c => c.IsCorrect == false).Count();
            stat.c1 = resultList.Where(c => c.Course == "C1").Count();
            stat.c2 = resultList.Where(c => c.Course == "C2").Count();
            stat.c3 = resultList.Where(c => c.Course == "C3").Count();
            return Json(stat);
        }
        public class ResultReview
        {
            public int length;
            public int correctNumber;
            public int incorrectNumber;
            public int c1;
            public int c2;
            public int c3;
        }
    }
}