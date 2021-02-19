using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using TravelExperts.Repository.Domain;


namespace TravelExpertsWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(Customers customer)
        {

            return RedirectToAction("Index", "Home");
        }

    }
}
