using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
                .Where(b => b.CustomerId == id)
                .Include(b=> b.Customer)
                .Include(b => b.Package)
                .Include(b => b.BookingDetails)
                .ToList();
            return pack;
        }

        public static IList GetRecommendations(int custID)
        {
            //open the DB
            TravelExpertsContext db = new TravelExpertsContext();

            // Get a list of all trips the customer has been on
            // This is hard to quantify in the existing data, since no Packages have been ordered..
            // As is, we'll use the Description field for booking details, which typically represents a whole booking

            List<int> previousCustBookings = db.Bookings
                .Where(booking => booking.CustomerId != null && booking.CustomerId == custID)
                .Select(booking => booking.BookingId).ToList(); // taking only the current customer's records

            List<string> previousCustTrips = db.BookingDetails
                .Where(bookingDetail => bookingDetail.BookingId != null
                    && previousCustBookings.Contains(Convert.ToInt32(bookingDetail.BookingId)))
                .Select(detail => detail.Description)
                .Distinct().ToList();

            // TODO: Use the list of trips the customer has gone on, find other customers who have gone on them
            // Then return the most popular trips among those

            return new List<Bookings>();
        }
    }
}
