﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoctorHelper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoctorHelper.Controllers
{
    public class HomeController : Controller
    {
        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Visit()
        {
            return View();
        }

        public IActionResult Medicine()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            Doctor doctor = new Doctor();
            doctor.DoctorId = Guid.NewGuid().ToString();
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Register(Doctor doctor)
        {
            DataBaseController DB = new DataBaseController();
            DB.InsertNewDoctor(doctor);
            return View("Index");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}