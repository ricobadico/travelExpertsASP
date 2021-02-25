using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExperts.BLL
{
    /*
     * Methods for managing customers and their data.
     */

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
        /// Search the database for a customer with a given ID. [Eric]
        /// </summary>
        /// <param name="custId">Customer ID as an INT.</param>
        /// <returns>Matching customer object if one was found, null if no match.</returns>
        public static Customer FindById(int custId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customer matchCust = db.Customers.SingleOrDefault(c => c.CustomerId == custId);

            return matchCust;
        }
        
        /// <summary>
        /// Updates customer data in db based on corresponding customer object [Eric]
        /// </summary>
        /// <param name="editedCustomer"></param>
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

        /*
         * Phone-number formatting methods to ensure nice presentation regardless of customer input format. Started by Eric, debugged and perfected by Ronnie.
         */
        public static string tidyPhoneNumber(string phoneInput)
        {
            // Strip any spaces and special characters from the input
            string tidiedPN = new string(phoneInput.Where(char.IsDigit).ToArray());
            
            return tidiedPN;
        }

        public static string FormatPhoneNumber(string phoneNum)
        {
            if (phoneNum.Length == 10)
            {
                // Put it in a nice, readable format
                phoneNum = $"({phoneNum.Substring(0, 3)}) {phoneNum.Substring(3, 3)}-{phoneNum.Substring(6, 4)}";
            }
            else if (phoneNum.Length == 11)
            {
                phoneNum = $"{phoneNum.Substring(0, 1)}({phoneNum.Substring(1, 3)}) {phoneNum.Substring(4, 3)}-{phoneNum.Substring(7, 4)}";
            }

            return phoneNum;
        }


    }
}
