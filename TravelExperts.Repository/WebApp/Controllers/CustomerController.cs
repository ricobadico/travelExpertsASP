using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using TravelExperts.Repository.Domain;
using TravelExperts.BLL;


namespace TravelExpertsWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        
        public IActionResult Add(Customers customer)
        {
            CustomerManager.Add(customer);
            return RedirectToAction("Index", "Home");
        }

    }
}
