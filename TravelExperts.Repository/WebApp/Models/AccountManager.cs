using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExpertsWebApp.Models
{
    /// <summary>
    /// Data transfer object to pass needed customer data around site
    /// </summary>
    public class CustAccountData
    {
        public int? CustId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
    }

    /// <summary>
    /// Class responsible for authenticating and managing users.
    /// </summary>
    public class AccountManager
    {

        /// <summary>
        /// User is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="login">Login as string</param>
        /// <param name="pass">Password as string</param>
        /// <returns>A user data transfer object or null.</returns>
        public static CustAccountData Authenticate(string login, string pass)
        {
            // Initialize a user object with null reference
            CustAccountData user = null;

            // Connect to the db
            TravelExpertsContext context = new TravelExpertsContext();

            // Search db for Customer with matching credentials
            TravelExperts.Repository.Domain.Customer cust = context.Customers.SingleOrDefault(c =>
                c.UserLogin == login
                && c.UserPass == pass);

            // If a match was found, create a DTO with needed information
            if  (cust != null)
            {
                user = new CustAccountData
                {
                    CustId = cust.CustomerId,
                    Login = cust.UserLogin,
                    FirstName = cust.CustFirstName
                };
            }

            return user; //this will either be null or an object
        }
    }
}
