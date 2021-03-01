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
        //[Holly]
        public static IEnumerable<Packages> GetAll()
        {
            var context = new TravelExpertsContext();
            var pack = context.Packages.ToList();
            return pack;
        }
        //[Chris]
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

        /// <summary>
        /// Uses customer's past trips to recommend new trips for them, based on other customers who have gone on the same trips. [Eric]
        /// NOTE: This should eventually only return recommendations for trips with future start dates. We don't have the data for that yet, so ommitting it.
        /// </summary>
        /// <param name="custID">The customer's ID</param>
        /// <param name="numberOfRecs"> The number of recommendations to provide</param>
        /// <returns>A list of (numberOfRecs) recommendations</returns>
        public static IEnumerable<BookingDetails> GetRecommendations(int custID, int numberOfRecs)
        {
            //open the DB
            TravelExpertsContext db = new TravelExpertsContext();

            // Get a list of all trips the customer has been on
            // This is hard to quantify in the existing data, since no Packages have been ordered in the database (otherwise this method would search them).
            // And a single product/supplier can represent a wide array of things (Hotel - MARKETING AHEAD has a record for a past victoria, autstralia, and toronto trip)
            // As is, we'll use the Description field for booking details, which typically represents a whole booking
            List<string> previousCustTrips = db.Bookings // Search bookings..
                .Include(booking => booking.BookingDetails) // .. with associated booking details..
                .Where(booking => booking.CustomerId == custID) // .. with the given customer ID ..
                .SelectMany(booking => booking.BookingDetails) // .. grabbing all booking details in a flattened list ..
                .Where(bd => !(bd.Description.Contains("cancellation"))) // .. flesh out non-fun products ..
                    .Select(bd => bd.Description) // .. getting just Descriptions ..
                    .Distinct().ToList(); // .. removing duplicates and converting to list.

            // Next, find all customers who have been on the same trips
            List<int?> customersWhoTookSameTrip = db.Bookings // Search bookings..
                .Join(db.BookingDetails, b => b.BookingId, bd => bd.BookingId, (b, bd) => new { b, bd }) // .. join associated booking details..
                .Where(join => join.b.CustomerId != custID && join.b.CustomerId!=null) // ..excluding the customer themselves (and nulls) ..
                .Where(join => previousCustTrips.Contains(join.bd.Description)) // ..where the trip is among those that the customer has had..
                .Select(join => (join.b.CustomerId)) // .. grabbing only customers
                .Distinct().ToList();

            // Finally, find all other trips those customers have been on, sorted by most popular
            IEnumerable<BookingDetails> recommendations = db.Bookings // Search bookings..
                .Include(booking => booking.BookingDetails)  // .. with associated booking details..
                .Where(b => customersWhoTookSameTrip.Contains(b.CustomerId)) // .. get all bookings of the chosen customers ...
                .SelectMany(booking => booking.BookingDetails) // .. grabbing all booking details in a flattened list ..
                .Where(bd => bd.Description != "" && !previousCustTrips.Contains(bd.Description)) // .. removing trips the customer has already gone on and nulls..
                .Where(bd => !(bd.Description.Contains("cancellation"))) // .. flesh out non-fun products ..
                .AsEnumerable().GroupBy(bd => bd.Description) // .. group by description..
                .OrderByDescending(bdGroup => bdGroup.Count()) // sort by the description with the most bookings (most popular)
                .Select(bdGroup => bdGroup.First()) // we don't need the groups anymore, so we just take one instance of the booking detail
                .Take(numberOfRecs); // Just need top X

            // This works for a demonstration, but with the data we have in the database we can't arrive at a proper future-trip recommendation.
            // This is because all existing customer records store trip details in bookingdetails (no customers have packages recorded),
            // while packages are the only entity with a description that can reflect a future trip.
            // On the other hand, the current method as-is could be a good means of gathering past data, to be used to inform future packages.

            //If we don't get enough recommendations, we pad them with the general most popular ones
            int recordsDearth = numberOfRecs - recommendations.ToList().Count;
            if(recordsDearth > 0)
            {
                // get the top x most popular trips
                var recPadding = GetMostPopularTrips(recordsDearth);
                
                // add them to the list so we reach the number of recommendations
                recommendations = recommendations.Concat(recPadding);
            }

            return recommendations;
        }

        /// <summary>
        /// Get the top X most popular trips from the database [Eric]
        /// </summary>
        /// <param name="numberOfRecs"></param>
        /// <returns></returns>
        public static IEnumerable<BookingDetails>GetMostPopularTrips(int numberOfRecs)
        {
            //open the DB
            TravelExpertsContext db = new TravelExpertsContext();

            // Get all booking details, group them by description, and order them by popularity
            IEnumerable<BookingDetails> mostPopular = db.Bookings // Search bookings..
                .Include(booking => booking.BookingDetails)  // .. with associated booking details..
                .SelectMany(booking => booking.BookingDetails) // .. grabbing all booking details in a flattened list ..
                .AsEnumerable().GroupBy(bd => bd.Description) // .. group by description..
                .OrderByDescending(list => list.Count()) // sort by the description with the most bookings (most popular)
                .Select(list => list.First()) // we don't need the groups anymore, so we just take one instance of the booking detail
                .Take(numberOfRecs); // Just need top X

            return mostPopular;
        }
    }
}
