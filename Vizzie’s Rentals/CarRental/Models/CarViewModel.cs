using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class CarViewModel
    {
        [Required]
        public DateViewModel DateView { get; set; }

        [Required]
        public Car Car { get; set; }
    }
}