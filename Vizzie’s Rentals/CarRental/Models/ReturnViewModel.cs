using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class ReturnViewModel
    {
        [Required(ErrorMessage = "A reservation number is required")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Must be a valid Reservation Number")]
        public string ReserveNum { get; set; }

        [Required(ErrorMessage = "A first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "A last name is required")]
        public string LastName { get; set; }
    }
}