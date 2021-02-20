using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExpertsWebApp.Models
{
    public class BookingDetailsModel
    {
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }
        public DateTime? TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Fees Fee { get; set; }
        public virtual Regions Region { get; set; }
    }
}
