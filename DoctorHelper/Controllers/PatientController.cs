using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHelper.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            DataBaseController DB = new DataBaseController();
            var patients = DB.GetDoctorsPatients(HttpContext.Session.GetString("user"));
            ViewBag.User = HttpContext.Session.GetString("user");
            return View(patients);
        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            Patient patient = new Patient();
            patient.PatientId = Guid.NewGuid().ToString();
            ViewBag.User = HttpContext.Session.GetString("user");
            return View(patient);
        }


        [HttpPost]
        public IActionResult CreatePatient(Patient patient)
        {
            DataBaseController DB = new DataBaseController();
            DB.InsertNewPatient(patient);
            var patients = DB.GetDoctorsPatients(HttpContext.Session.GetString("user"));
            ViewBag.User = HttpContext.Session.GetString("user");
            return View("Index", patients);
        }

        public IActionResult EditPatient(string id)
        {
            DataBaseController DB = new DataBaseController();
            Patient patient = DB.GetPatientById(id);
            ViewBag.User = HttpContext.Session.GetString("user");
            return View(patient);
        }

        public IActionResult SavePatient(Patient patient)
        {
                DataBaseController DB = new DataBaseController();
                DB.UpdatePatient(patient);
                var patients = DB.GetDoctorsPatients(HttpContext.Session.GetString("user"));
            ViewBag.User = HttpContext.Session.GetString("user");
            return View("Index",patients);
        }
    }
}