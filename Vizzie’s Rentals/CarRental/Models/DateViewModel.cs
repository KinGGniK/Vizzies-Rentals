using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class DateViewModel
    {
        [Required(ErrorMessage = "A pick-up date is required")]
        [DataType(DataType.Date)]
        [DisplayName("Pick-Up Date")]
        public DateTime PickUpDate { get; set; }

        [Required(ErrorMessage = "A return date is required")]
        [DataType(DataType.Date)]
        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

        public int numDays { get; set; }
    }
}