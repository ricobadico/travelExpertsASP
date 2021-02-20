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
    }
}
