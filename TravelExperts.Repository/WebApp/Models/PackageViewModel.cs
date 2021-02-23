using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExperts.Repository.Domain;

namespace TravelExpertsWebApp.Models
{
    public class PackageViewModel
    {
        public int PackageId { get; set; }
        public string PkgName { get; set; }
        public DateTime? PkgStartDate { get; set; }

        public string StartDateDisplay
        {
            get
            {
                if (PkgStartDate != null)
                    return ((DateTime)PkgStartDate).ToShortDateString();
                else return null;
            }
        }

        public DateTime? PkgEndDate { get; set; }
        
        public string EndDateDisplay { get {
                if (PkgEndDate != null)
                    return ((DateTime)PkgEndDate).ToShortDateString();
                else return null;
            } }

        public string PkgDesc { get; set; }
        public string PkgBasePrice { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}
