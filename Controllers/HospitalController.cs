using Microsoft.AspNetCore.Mvc;
using HspMgmt_asgmt.Models;

namespace HspMgmt_asgmt.Controllers
{
    public class HospitalController : Controller
    {
        private readonly MyContext _context;

        public HospitalController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hospitals = _context.Hospitals.ToList();
            return View(hospitals);
        }

        public IActionResult Details(int id)
        {
            var hospital = _context.Hospitals.FirstOrDefault(x => x.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        public IActionResult Edit(int id)
        {
            var hospital = _context.Hospitals.FirstOrDefault(x => x.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                return View(hospital);
            }

            _context.Entry(hospital).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                return View(hospital);
            }

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var hospital = _context.Hospitals.FirstOrDefault(x => x.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hospital = _context.Hospitals.FirstOrDefault(x => x.HospitalId == id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
