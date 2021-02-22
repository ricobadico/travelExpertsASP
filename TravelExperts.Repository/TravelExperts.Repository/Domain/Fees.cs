using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExperts.Repository.Domain
{
    public partial class Fees
    {
        public Fees()
        {
            BookingDetails = new HashSet<BookingDetails>();
        }

        public string FeeId { get; set; }
        public string FeeName { get; set; }
        public decimal FeeAmt { get; set; }
        [NotMapped]
        public string FeeAmtDisplay
        {
            get
            {
                if (FeeAmt != null) //if not null..
                    return ((Decimal)FeeAmt).ToString("c"); //.. cast TripStart to DateTime and use ToShortDateString function
                else return null; // otherwise stay null
            }
        }
        public string FeeDesc { get; set; }

        public virtual ICollection<BookingDetails> BookingDetails { get; set; }
    }
}
