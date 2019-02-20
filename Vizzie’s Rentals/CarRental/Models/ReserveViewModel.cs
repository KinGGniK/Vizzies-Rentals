using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class ReserveViewModel
    {
        public UserViewModel UserView { get; set; }

        public CarViewModel CarView { get; set; }
    }
}