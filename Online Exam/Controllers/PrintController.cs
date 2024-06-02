using Microsoft.AspNetCore.Mvc;
using Online_Exam.Models;

namespace Online_Exam.Controllers
{
    public class PrintController : Controller
    {
        private readonly OnlineExammDbContext db;

        public PrintController(OnlineExammDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("U_Email");
            var Name = HttpContext.Session.GetInt32("ExamCode").Value;
            bool check = db.Answers.Any(q => q.U_Email == x && q.Exam_id == Name);

            if (string.IsNullOrEmpty(x))
            {
                return RedirectToAction("Signin", "Login");
            }
            if (check == true)
            {
                TempData["AlertMessage"] = "You have already entered this exam before.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.mess = Name;
                List<Exam> ListExam = db.Exams.ToList();
                List<Questions> ListQuestions = db.Questions.ToList();
                List<Choices> ListChoices = db.Choices.ToList();
                PrintViewModel obViewModel = new PrintViewModel
                {
                    ExamViewModel = ListExam,
                    QuestionsViewModel = ListQuestions,
                    ChoicesViewModel = ListChoices
                };

                return View(obViewModel);
            }
        }
    
    }
}
