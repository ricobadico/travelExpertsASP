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
using TravelExperts.BLL;

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

        [Authorize]
        public IActionResult Manage()
        {
            // Get customer ID from user indentity (since this is an authorized action, this is safe)
            int custID = Convert.ToInt32(User.Identity.Name);

            // Connect to db
            TravelExpertsContext db = new TravelExpertsContext();

            Customers customer = CustomerManager.FindById(custID);

            return View(customer);
        }



    }
}
