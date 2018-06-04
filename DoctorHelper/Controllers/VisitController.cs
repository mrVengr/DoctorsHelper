using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHelper.Controllers
{
    public class VisitController : Controller
    {
        public IActionResult Index()
        {
            DataBaseController DB = new DataBaseController();
            Dictionary<Visit, Patient> Visits = DB.GetDoctorsVisits(HttpContext.Session.GetString("user"));
            ViewBag.User = HttpContext.Session.GetString("user");
            return View(Visits);
        }

        [HttpGet]
        public IActionResult CreateVisit()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            Visit visit  = new Visit();
            DataBaseController DB = new DataBaseController();
            ViewBag.patients = DB.GetPatients();
            ViewBag.medicines = DB.GetAllMedicines();
            visit.VisitId = Guid.NewGuid().ToString();
            visit.DoctorId = HttpContext.Session.GetString("user");
            return View(visit);
        }

        [HttpPost]
        public IActionResult SaveVisit(Visit visit)
        {
            DataBaseController DB = new DataBaseController();
            if (visit.PrescribedMedicines == null)
            {
                visit.PrescribedMedicines = new List<string>();
            }
            DB.InsertNewVisit(visit);
            Dictionary<Visit, Patient> Visits = DB.GetDoctorsVisits(HttpContext.Session.GetString("user"));
            ViewBag.User = HttpContext.Session.GetString("user");
            return View("Index",Visits);
        }
    }
}