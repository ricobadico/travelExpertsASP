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
        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z][ -] ?[0-9][A-Z][0-9]$", ErrorMessage = "Invalid Postal Code Format")]
        [Display(Name = "Postal Code")]
        public string Postal { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [Display(Name ="Telephone")]
        [RegularExpression(@"^[0-9]?[-. ]?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string HomePhone { get; set; }

        [Display(Name ="Business Phone")]
        [RegularExpression(@"^[0-9]?[-. ]?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string BusPhone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Username")]
        public string UserLogin { get; set; }

        [Required]
        [Display(Name ="Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage ="Invalid Password Format")]
        public string UserPass { get; set; }
    }
}
