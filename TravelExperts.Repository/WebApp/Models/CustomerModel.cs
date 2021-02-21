using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExpertsWebApp.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }

        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name ="Province")]
        public string Prov { get; set; }

        [Required]
        public string Postal { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name ="Phone")]
        [Phone]
        public string HomePhone { get; set; }

        [Display(Name ="Business Phone")]
        [Phone]
        public string BusPhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Username")]
        public string UserLogin { get; set; }

        [Required]
        [Display(Name ="Password")]
        public string UserPass { get; set; }
    }
}
