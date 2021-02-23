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
            // Hashes password for secure storage in the database
            customer.UserPass = AccountManager.HashPassword(customer.UserPass);

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
            if (customer.BusPhone != null && customer.BusPhone != String.Empty)
            {
                cust.CustBusPhone = CustomerManager.tidyPhoneNumber(customer.BusPhone);
            }

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

            customer.CustHomePhone = CustomerManager.FormatPhoneNumber(customer.CustHomePhone);
            if (customer.CustBusPhone != null && customer.CustBusPhone != String.Empty)
            {
                customer.CustBusPhone = CustomerManager.FormatPhoneNumber(customer.CustBusPhone);
            }

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

        public IActionResult ManageUser()
        {

            return View();

        }
        // Post overload that takes the submitted changes to their customer data and saves them to the database
        [Authorize]
        [HttpPost]
        public IActionResult ManageUser(UserCredentialModel newLoginDetails)
        {
            int custID = Convert.ToInt32(User.Identity.Name);
            Customer custDBRecord = CustomerManager.FindById(custID);

            if (!(newLoginDetails.NewPass == newLoginDetails.ConfirmNewPass))
            {
                TempData["PasswordsNotSame"] = "Your existing passwords do  not match";
            }
            else if (AccountManager.Authenticate(newLoginDetails.ExistingLogin, newLoginDetails.ExistingPass) == null)
            {
                TempData["ErrorMessage"] = "Your existing login or password is incorrect.";
            }
            else
            { 

                custDBRecord.UserLogin = newLoginDetails.NewLogin;
                custDBRecord.UserPass = AccountManager.HashPassword(newLoginDetails.NewPass);

                // Attempt to update customer in db
                try
                {
                    CustomerManager.Update(custDBRecord);

                    return RedirectToAction("Index", "Home"); //TODO: maybe this should go elsewhere
                }
                // If valid inputs but update failed, something's up with the database. A special method gets displayed
                catch
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please check your inputs or contact customer service.";
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
