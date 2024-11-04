using Microsoft.AspNetCore.Mvc;
using HspMgmt_asgmt.Models;
using Microsoft.EntityFrameworkCore;

namespace HspMgmt_asgmt.Controllers
{
    public class DoctorController : Controller
    {
        private readonly MyContext _context;

        public DoctorController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var doctors = _context.Doctors.Include(d => d.Hospital).ToList();
            return View(doctors);
        }

        public IActionResult Details(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            _context.Entry(doctor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.Hospitals = _context.Hospitals.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (!ModelState.IsValid)
            {
                return View(doctor);
            }

            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(x => x.DoctorId == id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
