using Microsoft.AspNetCore.Mvc;
using Online_Exam.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Web;
public class ExamController : Controller
{
    private readonly OnlineExammDbContext _db;

    public ExamController(OnlineExammDbContext context)
    {
        _db = context;
    }

    public IActionResult Create()
    {
        var userEmail = HttpContext.Session.GetString("U_Email");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Signin", "Login");
        }

        Exam exam = new Exam();
        return View(exam);
    }

    [HttpPost]
    public IActionResult Create(Exam exam)
    {
        string adminEmail = HttpContext.Session.GetString("U_Email");
        exam.Adminstration_Email = adminEmail;
        _db.Exams.Add(exam);
        _db.SaveChanges();
        HttpContext.Session.SetInt32("Exam_id", exam.Exam_id);
        return RedirectToAction("Create", "Question");
    }

    public IActionResult Index()
    {
        var userEmail = HttpContext.Session.GetString("U_Email");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Signin", "Login");
        }

        var exams = _db.Exams
            .Where(e => e.Adminstration_Email == userEmail)
            .ToList();

        return View(exams);
    }

    public async Task<IActionResult> Details(int id)
    {
        var exam = await _db.Exams.FirstOrDefaultAsync(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    public IActionResult Edit(int id)
    {
        var exam = _db.Exams.FirstOrDefault(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    [HttpPost]
    public IActionResult Edit(int id, Exam updatedExam)
    {
        if (id != updatedExam.Exam_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingExam = _db.Exams.Find(id);
            if (existingExam != null)
            {
                existingExam.Exam_title = updatedExam.Exam_title;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        return View(updatedExam);
    }

    public IActionResult Delete(int id)
    {
        var exam = _db.Exams.FirstOrDefault(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var exam = _db.Exams.Find(id);

        if (exam == null)
        {
            return NotFound();
        }

        _db.Exams.Remove(exam);
        _db.SaveChanges();

        return RedirectToAction("Index");
    }

   
    [HttpPost]
    public IActionResult SaveExamCode(int examCode)
    {
        HttpContext.Session.SetInt32("ExamCode", examCode);
        return NoContent();
    }


    public IActionResult Finish()
    {
    HttpContext.Session.Remove("Exam_id");
    return RedirectToAction("Index", "Home");
    }
}
