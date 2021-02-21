using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExpertsWebApp.Models
{
    public class BookingModel
    {

        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string BookingNo { get; set; }
        public double? TravelerCount { get; set; }
        public int? CustomerId { get; set; }
        public string TripTypeId { get; set; }
        public int? PackageId { get; set; }

        public string ItineraryNo { get; set; }
        public string TripStart { get; set; }
        public string TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal? BasePrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Packages Package { get; set; }
        public virtual TripTypes TripType { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Fees Fee { get; set; }
        public virtual Regions Region { get; set; }

    }
}
