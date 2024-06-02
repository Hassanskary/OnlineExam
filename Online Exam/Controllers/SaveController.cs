using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Models;
using System;
using System.Linq;
using System.Diagnostics;
namespace Online_Exam.Controllers
{
    public class SaveController : Controller
    {
        private readonly OnlineExammDbContext db;

        public SaveController(OnlineExammDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SaveAnswers(IFormCollection form)
        {
            List<Exam> ListExam = db.Exams.ToList();
            List<Questions> ListQuestions = db.Questions.ToList();
            List<Choices> ListChoices = db.Choices.ToList();
            PrintViewModel obViewModel = new PrintViewModel
            {
                ExamViewModel = ListExam,
                QuestionsViewModel = ListQuestions,
            };
            List<string> Ans = new List<string>();
            List<int> QuestionId = new List<int>();
            var Examid = HttpContext.Session.GetInt32("ExamCode").Value;

            foreach (var y in ListQuestions)
            {
                if (Examid == y.Exam_id)
                {
                    QuestionId.Add(y.Question_id);
                    var User_answer = y.Question_id.ToString();
                    string val = form[User_answer];
                    Ans.Add(val);
                }
            }

            var Name = HttpContext.Session.GetString("U_Email");

            for (int i = 0; i < Ans.Count; i++)
            {
                Answers answersObject = new Answers();

                answersObject.Exam_id = Examid;
                answersObject.Question_id = QuestionId[i];
                answersObject.U_Email = Name;
                answersObject.Answer_text = Ans[i];

                decimal Grade = 0;
                foreach (var q in ListQuestions)
                {
                    if (QuestionId[i] == q.Question_id && Examid == q.Exam_id)
                    {
                        Grade = q.Points;
                    }
                }
                foreach (var ch in ListChoices)
                {
                    if (ch.Exam_id == Examid && ch.Question_id == QuestionId[i] && ch.Is_correct && Ans[i] == ch.Choice_text)
                    {
                        answersObject.Points_Earned = Grade;
                    }
                }

                db.Answers.Add(answersObject);
                db.SaveChanges();
            }
            List<Answers> ListAnswers = db.Answers.ToList();

            decimal grade = 0;
			foreach(var answer in ListAnswers) {
                if(answer.Exam_id == Examid && Name == answer.U_Email)
                {
					grade += answer.Points_Earned;
				}
				
			}
			ViewBag.Grade = grade;
            Console.WriteLine(grade);
			return View("Degree");

        }
    }
}