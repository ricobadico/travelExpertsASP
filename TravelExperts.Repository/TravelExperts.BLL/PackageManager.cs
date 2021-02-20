using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TravelExperts.Repository.Domain;

namespace TravelExperts.BLL
{
    public class PackageManager
    {
        public static List<Packages> GetAll()
        {
            var context = new TravelExpertsContext();
            var pack = context.Packages.ToList();
            return pack;
        }
        public static List<Bookings> GetPackagesByCustId(int id)
        {
            var context = new TravelExpertsContext();
            //var pack = context.Bookings.Where(a => a.CustomerId ==  id).Include(a => a.Package).ToList();
            //return pack;


            var pack = context.Bookings
                .Include(b=> b.Customer)
                .Include(b => b.Package)
                .Include(b => b.BookingDetails)
                .Where(b => b.Customer.CustomerId == id)
                .ToList();
            return pack;
        }

    }
}
