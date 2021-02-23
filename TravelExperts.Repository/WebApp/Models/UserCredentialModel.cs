using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelExpertsWebApp.Models
{
    public class UserCredentialModel
    {
        [Required]
        [Display(Name = "Current Username")]
        public string Existingogin { get; set; }

        [Required]
        [Display(Name = "New Username")]
        public string UserLogin { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{8,}$", ErrorMessage = "Invalid Password Format")]
        public string ExistingPass { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{8,}$", ErrorMessage = "Invalid Password Format")]
        public string UserPass { get; set; }
    }
}
