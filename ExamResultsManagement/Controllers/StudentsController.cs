using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMongoCollection<Student> _students;

        public StudentsController(IMongoClient client)
        {
            var database = client.GetDatabase("YourDatabaseName"); // Replace with your actual database name
            _students = database.GetCollection<Student>("Students");
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var students = await _students.Find(student => true).ToListAsync();
            return View(students);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = ObjectId.Empty; // Assign empty ObjectId as MongoDB will auto-generate it
                await _students.InsertOneAsync(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            var student = await _students.Find(s => s.Id == objectId).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            if (id != student.Id.ToString())
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var filter = Builders<Student>.Filter.Eq(s => s.Id, student.Id);
                var update = Builders<Student>.Update
                    .Set(s => s.Name, student.Name)
                    .Set(s => s.RollNumber, student.RollNumber);
                await _students.UpdateOneAsync(filter, update);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            var student = await _students.Find(s => s.Id == objectId).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            await _students.DeleteOneAsync(s => s.Id == objectId);
            return RedirectToAction(nameof(Index));
        }
    }
}
