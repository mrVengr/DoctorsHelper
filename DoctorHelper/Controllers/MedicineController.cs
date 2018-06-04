using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorHelper.Controllers
{
    public class MedicineController : Controller
    {
        public IActionResult Index()
        {
            DataBaseController DB = new DataBaseController();
            List<Medicine> medicines = DB.GetAllMedicines();
            ViewBag.User = HttpContext.Session.GetString("user");
            return View(medicines);
        }
    }
}