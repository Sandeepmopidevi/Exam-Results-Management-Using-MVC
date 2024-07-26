using Microsoft.AspNetCore.Mvc;
using ExamResultsManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace ExamResultsManagement.Controllers
{
    public class StudentResultsController : Controller
    {
        private static List<StudentResult> results = new List<StudentResult>();

        public IActionResult Index()
        {
            return View(results);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentResult result)
        {
            result.Id = results.Count > 0 ? results.Max(r => r.Id) + 1 : 1;
            results.Add(result);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var result = results.FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Edit(StudentResult result)
        {
            var existingResult = results.FirstOrDefault(r => r.Id == result.Id);
            if (existingResult == null)
            {
                return NotFound();
            }
            existingResult.StudentName = result.StudentName;
            existingResult.Subject1 = result.Subject1;
            existingResult.Marks1 = result.Marks1;
            existingResult.Subject2 = result.Subject2;
            existingResult.Marks2 = result.Marks2;
            existingResult.Subject3 = result.Subject3;
            existingResult.Marks3 = result.Marks3;
            existingResult.Subject4 = result.Subject4;
            existingResult.Marks4 = result.Marks4;
            existingResult.Subject5 = result.Subject5;
            existingResult.Marks5 = result.Marks5;
            existingResult.ExamMonth = result.ExamMonth;
            existingResult.ExamYear = result.ExamYear;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var result = results.FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = results.FirstOrDefault(r => r.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            results.Remove(result);
            return RedirectToAction("Index");
        }
    }
}
