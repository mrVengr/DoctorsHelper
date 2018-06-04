﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoctorHelper.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace DoctorHelper.Controllers
{
    public class HomeController : Controller
    {
        //[AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult About()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            Doctor doctor = new Doctor();
            doctor.DoctorId = Guid.NewGuid().ToString();
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Register(Doctor doctor)
        {
            try
            {
                DataBaseController DB = new DataBaseController();
                DB.InsertNewDoctor(doctor);
            }
            catch { }
            finally
            {
                HttpContext.Session.SetString("user", doctor.DoctorId);
                ViewBag.User = HttpContext.Session.GetString("user");
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.User = HttpContext.Session.GetString("user");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewBag.User = HttpContext.Session.GetString("user");
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(string Password, string Email)
        {
            DataBaseController DB = new DataBaseController();
            Doctor Doc=DB.Login(Email, Password);
            if (Doc==null)
            { return View("Login"); }
            HttpContext.Session.SetString("user", Doc.DoctorId);
            ViewBag.User = HttpContext.Session.GetString("user");
            return View("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}