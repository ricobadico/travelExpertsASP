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
using TravelExpertsWebApp.Models;

namespace TravelExpertsWebApp.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Add(CustomerModel customer)
        {
            var cust = new Customer
            {
                CustAddress = customer.Address,
                CustHomePhone = customer.HomePhone,
                CustBusPhone = customer.BusPhone,
                CustFirstName = customer.FirstName,
                CustLastName = customer.LastName,
                CustCity = customer.City,
                CustCountry = customer.Country,
                CustEmail = customer.Email,
                CustPostal = customer.Postal,
                CustProv = customer.Prov,
                UserLogin = customer.UserLogin,
                UserPass = customer.UserPass
            };
            CustomerManager.Add(cust);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Exist(string username)
        {
            var content = "";
            if (CustomerManager.Exists(username))
            {
                content = "Username already exists, please use a different user name.";
            }

            return Content(content);
        }

        [Authorize]
        public IActionResult Manage()
        {
            // Get customer ID from user indentity (since this is an authorized action, this is safe)
            int custID = Convert.ToInt32(User.Identity.Name);

            // Connect to db
            TravelExpertsContext db = new TravelExpertsContext();

            Customer customer = CustomerManager.FindById(custID);

            return View(customer);
        }



    }
}
