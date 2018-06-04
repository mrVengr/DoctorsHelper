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

        public IActionResult CreatePatient()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult EditPatient(Patient patient)
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }
    }
}