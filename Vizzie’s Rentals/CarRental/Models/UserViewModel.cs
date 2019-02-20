using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(36, MinimumLength = 3, ErrorMessage = "A valid last name is required")]
        public string LastName { get; set; }

        [Phone]
        [Required(ErrorMessage = "A valid U.S. phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "A valid U.S. phone number is required")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "A valid email is address is required. Ex: youremail@domain.com")]
        [Required(ErrorMessage = "An email address is required")]
        public string Email { get; set; }
    }
}