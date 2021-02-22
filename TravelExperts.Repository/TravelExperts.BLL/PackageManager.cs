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
                .ThenInclude(bd => bd.Fee)
                .ToList();
            return pack;
        }

        public static IEnumerable<BookingDetails> GetRecommendations(int custID)
        {
            //open the DB
            TravelExpertsContext db = new TravelExpertsContext();

            // Get a list of all trips the customer has been on
            // This is hard to quantify in the existing data, since no Packages have been ordered..
            // As is, we'll use the Description field for booking details, which typically represents a whole booking

            List<string> previousCustTrips = db.Bookings // Search bookings..
                .Include(booking => booking.BookingDetails) // .. with associated booking details..
                .Where(booking => booking.CustomerId == custID) // .. with the given customer ID ..
                .SelectMany(booking => booking.BookingDetails) // .. grabbing all booking details in a flattened list ..
                    .Select(bd => bd.Description) // .. getting just Descriptions ..
                    .Distinct().ToList(); // .. removing duplicates and converting to list.



            List<int?> customersWhoTookSameTrip = db.Bookings // Search bookings..
                .Join(db.BookingDetails, b => b.BookingId, bd => bd.BookingId, (b, bd) => new { b, bd }) // .. join associated booking details..
                .Where(join => join.b.CustomerId != custID && join.b.CustomerId!=null) // ..excluding the customer themselves (and nulls) ..
                .Where(join => previousCustTrips.Contains(join.bd.Description)) // ..where the trip is among those that the customer has had..
                .Select(join => (join.b.CustomerId)) // .. grabbing only customers
                .Distinct().ToList();

            IEnumerable<BookingDetails> recommendations = db.Bookings // Search bookings..
                .Include(booking => booking.BookingDetails)  // .. with associated booking details..
                .Where(b => customersWhoTookSameTrip.Contains(b.CustomerId)) // .. get all bookings of the chosen customers ...
                .SelectMany(booking => booking.BookingDetails) // .. grabbing all booking details in a flattened list ..
                .Where(bd => bd.Description != "" && !previousCustTrips.Contains(bd.Description)) // .. removing trips the customer has already gone on and nulls..
                .AsEnumerable().GroupBy(bd => bd.Description) // .. group by description..
                .OrderByDescending(list => list.Count()) // sort by the description with the most bookings (most popular)
                .Select(list => list.First()); // we don't need the groups anymore, so we just take one instance of the booking detail


            return recommendations;
        }
    }
}
