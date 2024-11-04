using Microsoft.AspNetCore.Mvc;
using HspMgmt_asgmt.Models;
using Microsoft.EntityFrameworkCore;

namespace HspMgmt_asgmt.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly MyContext _context;

        public AppointmentController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor) // Assuming an Appointment has a Doctor
                .ToList();
            return View(appointments);
        }

        public IActionResult Details(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(x => x.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            _context.Entry(appointment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.Patients = _context.Patients.ToList(); // Assuming you want to select a patient
            ViewBag.Doctors = _context.Doctors.ToList(); // Assuming you want to select a doctor
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.FirstOrDefault(x => x.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
