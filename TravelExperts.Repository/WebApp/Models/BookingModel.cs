using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExpertsWebApp.Models
{
    public class BookingModel
    {

        public int BookingId { get; set; }
        [Display(Name ="Booking Date")]
        public DateTime? BookingDate { get; set; }

        public string BookingDateDisplay { get {
                if (BookingDate != null)
                    return ((DateTime)BookingDate).ToShortDateString();
                else return null;
            } }

        [Display(Name = "Booking Number")]
        public string BookingNo { get; set; }
        [Display(Name = "Traveler Count")]
        public double? TravelerCount { get; set; }
        public int? CustomerId { get; set; }
        public string TripTypeId { get; set; }
        public int? PackageId { get; set; }
        [Display(Name = "Itinerary Number")]
        public string ItineraryNo { get; set; }
        [Display(Name = "Trip Start Date")]
        public string TripStart { get; set; }
        [Display(Name = "Trip End Date")]
        public string TripEnd { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        [Display(Name = "Base Pice")]
        public decimal? BasePrice { get {
                decimal? sum = 0;
                foreach (BookingDetails bd in BookingDetails)
                {
                    if (bd.BasePrice != null)
                    {
                        //Get fee totals for all booking details
                        sum += bd.Fee.FeeAmt;
                        sum += bd.BasePrice;
                    }
                }
                return sum;
            } set { BasePrice = value; } }
        [NotMapped]
        public string BasePriceDisplay
        {
            get
            {
                if (BasePrice != null) //if not null..
                    return ((Decimal)BasePrice).ToString("c"); //.. cast TripStart to DateTime and use ToShortDateString function
                else return null; // otherwise stay null
            }
        }


        public virtual Customer Customer { get; set; }
        public virtual Packages Package { get; set; }
        public virtual TripTypes TripType { get; set; }
        public virtual Classes Class { get; set; }
        public virtual Fees Fee { get; set; }
        public virtual Regions Region { get; set; }
        public virtual ICollection<BookingDetails> BookingDetails { get; set; }

    }
}
