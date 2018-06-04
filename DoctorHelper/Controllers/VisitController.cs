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
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult CreateVisit()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult EditVisit()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }
    }
}