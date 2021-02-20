using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExperts.BLL
{
    public class CustomerManager
    {
        public static void Add(Customer customer)
        {
            var db = new TravelExpertsContext();
            db.Customers.Add(customer);
            db.SaveChanges();
        }


        public static bool Exists(string username)
        {
            var db = new TravelExpertsContext();
            var custFromContext = db.Customers.SingleOrDefault(s => s.UserLogin == username);
            if (custFromContext == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Search the database for a customer with a given ID.
        /// </summary>
        /// <param name="custId">Customer ID as an INT.</param>
        /// <returns>Matching customer object if one was found, null if no match.</returns>
        public static Customer FindById(int custId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer matchCust = db.Customers.SingleOrDefault(c => c.CustomerId == custId);

            return matchCust;
        }

        public static void Update(Customer editedCustomer)
        {
            // Connect to db
            TravelExpertsContext db = new TravelExpertsContext();

            // Grab the original record
            Customer custRecord = db.Customers.Single(c => c.CustomerId == editedCustomer.CustomerId);

            // Assign updated props to the record
            custRecord.CustFirstName = editedCustomer.CustFirstName;
            custRecord.CustLastName = editedCustomer.CustLastName;
            custRecord.CustAddress = editedCustomer.CustAddress;
            custRecord.CustCity = editedCustomer.CustCity;
            custRecord.CustProv = editedCustomer.CustProv;
            custRecord.CustPostal = editedCustomer.CustPostal;
            custRecord.CustCountry = editedCustomer.CustCountry;
            custRecord.CustHomePhone = editedCustomer.CustHomePhone;
            custRecord.CustBusPhone = editedCustomer.CustBusPhone;
            custRecord.CustEmail = editedCustomer.CustEmail;
            custRecord.UserLogin = editedCustomer.UserLogin;
            custRecord.UserPass = editedCustomer.UserPass;

            // Update DB
            db.SaveChanges();
        }
    }
}
