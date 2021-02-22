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
            //********************************
            //TODO: uncomment this line to get passwords hashing. Requires updating SQL to allow for varchar(100) in pass
            //customer.UserPass = AccountManager.HashPassword(customer.UserPass);
            // ********************************

            var cust = new Customer
            {
                CustAddress = customer.Address,
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

            cust.CustHomePhone = CustomerManager.tidyPhoneNumber(customer.HomePhone);
            cust.CustBusPhone = CustomerManager.tidyPhoneNumber(customer.BusPhone);

            CustomerManager.Add(cust);
            
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation()
        {
            return View();
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

        // Allows user to edit their details and send them as a form
        [Authorize]
        public IActionResult Manage()
        {
            // Get customer ID from user indentity (since this is an authorized action, this is safe)
            int custID = Convert.ToInt32(User.Identity.Name);

            Customer customer = CustomerManager.FindById(custID);

            return View(customer);
        }

        // Post overload that takes the submitted changes to their customer data and saves them to the database
        [Authorize]
        [HttpPost]
        public IActionResult Manage(Customer editedCustomer)
        {
            // Calls the validation in the domain (which will catch things the database might not, like phone regex)
            if (TryValidateModel(editedCustomer))
            {
                // Attempt to update customer in db
                try
                {
                    CustomerManager.Update(editedCustomer);

                    return RedirectToAction("Index", "Home"); //TODO: maybe this should go elsewhere
                }
                // If valid inputs but update failed, something's up with the database. A special method gets displayed
                catch
                {
                    TempData["ErrorMessage"] = "Something went wrong when trying to update the database. Please try again later or contact customer service.";
                    return View();
                }
            }
            // If not valid, returning will show the error messages
            return View();
        }



        public IActionResult Record()
        {
            int custId = Convert.ToInt32(User.Identity.Name);
            var context = new TravelExpertsContext();
            var record = PackageManager.GetPackagesByCustId(custId)
    .Select(b => new BookingModel
    {
        BookingId = b.BookingId,
        BookingDate = b.BookingDate, // cast is safe since if the customer booked it, it has a date
        BookingNo = b.BookingNo,
        TravelerCount = b.TravelerCount,
        CustomerId = b.CustomerId,
        TripType = b.TripType,
        BookingDetails = b.BookingDetails
       
    }).ToList();
            decimal? test = record[0].BasePrice;

            //model given to the view
            return View(record);
        }



        public IActionResult RecordTest()
        {
            int custId = Convert.ToInt32(User.Identity.Name);
            TravelExpertsContext context = new TravelExpertsContext();

            List<Bookings> custBookings = PackageManager.GetPackagesByCustId(custId);

            return View(custBookings);
        }
    }
}
