using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHelper.Controllers
{
    public class VisitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateVisit()
        {
            return View();
        }

        public IActionResult EditVisit()
        {
            return View();
        }
    }
}