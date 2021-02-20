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
        public static void Add(Customers customer)
        {
            var db = new TravelExpertsContext();
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public static Customers FindById(int custId)
        {
            TravelExpertsContext db = new TravelExpertsContext();
            Customers matchCust = db.Customers.SingleOrDefault(c => c.CustomerId == custId);

            return matchCust;
        }
    }
}
