using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHelper.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePatient()
        {
            return View();
        }

        public IActionResult EditPatient()
        {
            return View();
        }
    }
}