using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using TravelExperts.Repository.Domain;
using Microsoft.AspNetCore.Authorization;

namespace TravelExpertsWebApp.Controllers
{
    public class CustomerController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

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
