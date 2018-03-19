using DoctorsHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorsHelper.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            //OracleController OC = new OracleController();
           // OC.GetAllDoctors();
            return View();
        }

        [HttpGet]
        public ViewResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RegisterForm(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                // TODO: Email response to the party organizer
                ViewBag.IsLoggedIn = true;
                return View("MainPage",doctor);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

        [HttpGet]
        public ViewResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult LoginForm(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                // TODO: Email response to the party organizer
                ViewBag.IsLoggedIn = true;
                return View("MainPage", doctor);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }

        public ViewResult Logout()
        {
            ViewBag.IsLoggedIn = false;
            return View("Index");
        }

        [HttpGet]
        public ViewResult NewVisit()
        {
            ViewBag.IsLoggedIn = true;
            return View();
        }

        [HttpPost]
        public ViewResult NewVisit(Visit visit)
        {
            if (ModelState.IsValid)
            {
                // TODO: Email response to the party organizer
                ViewBag.IsLoggedIn = true;
                return View("MainPage", visit);
            }
            else
            {
                // there is a validation error
                return View();
            }
        }
    }
}