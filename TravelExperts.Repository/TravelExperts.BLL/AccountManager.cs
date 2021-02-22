using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;
using bcrypt = BCrypt.Net.BCrypt; 

namespace TravelExperts.BLL
{
    /// <summary>
    /// Reduced customer data to function as the model for login
    /// and as a data transfer object to pass needed customer data around site
    /// </summary>
    public class CredentialModel
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
        public static CredentialModel Authenticate(string login, string pass)
        {
            // Initialize a user object with null reference
            CredentialModel user = null;

            // Connect to the db
            TravelExpertsContext context = new TravelExpertsContext();

            // Search db for Customer with matching credentials
            TravelExperts.Repository.Domain.Customer cust = context.Customers.SingleOrDefault(c =>
                c.UserLogin == login);

            // If a match was found, create a DTO with needed information
            if (cust != null // if the login matched in the db
                && bcrypt.Verify(pass, cust.UserPass)) // and bcrypt confirms the password
            {
                user = new CredentialModel
                {
                    CustId = cust.CustomerId,
                    Login = cust.UserLogin,
                    FirstName = cust.CustFirstName
                };
            }

            return user; //this will either be null or an object
        }

        public static string HashPassword(string providedPass)
        {
            // get the hashed version of the password provided
            string hashedPass = bcrypt.HashPassword(providedPass);

            return hashedPass;
        }
    }

}
