using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class DateMilesViewModel
    {
        public DateTime ReturnDate { get; set; }

        [Required]
        [Range(150,300000, ErrorMessage = "The mileage must be between 150 and 300,000 miles")]
        public int Miles { get; set; }
    }
}