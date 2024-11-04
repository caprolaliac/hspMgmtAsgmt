using Microsoft.AspNetCore.Mvc;
using HspMgmt_asgmt.Models;

namespace HspMgmt_asgmt.Controllers
{
    public class PatientController : Controller
    {
        private readonly MyContext _context;

        public PatientController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }

        public IActionResult Details(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            _context.Entry(patient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId == id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}