using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExperts.Repository.Domain
{
    public partial class BookingDetails
    {
        public int BookingDetailId { get; set; }
        public double? ItineraryNo { get; set; }
        public DateTime? TripStart { get; set; }

        // Display-function not used in the database
        [NotMapped]
        public string TripStartDisplay { get {
                if (TripStart != null) //if not null..
                    return ((DateTime)TripStart).ToShortDateString(); //.. cast TripStart to DateTime and use ToShortDateString function
                else return null; // otherwise stay null
            } }
        public DateTime? TripEnd { get; set; }
        [NotMapped]
        public string TripEndDisplay
        {
            get
            {
                if (TripEnd != null) //if not null..
                    return ((DateTime)TripEnd).ToShortDateString(); //.. cast TripStart to DateTime and use ToShortDateString function
                else return null; // otherwise stay null
            }
        }

        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }
        public decimal? AgencyCommission { get; set; }
        public int? BookingId { get; set; }
        public string RegionId { get; set; }
        public string ClassId { get; set; }
        public string FeeId { get; set; }
        public int? ProductSupplierId { get; set; }

        public virtual Bookings Booking { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Fees Fee { get; set; }
        public virtual ProductsSuppliers ProductSupplier { get; set; }
        public virtual Regions Region { get; set; }
    }
}
