using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class ReservationViewModel
    {
        public UserViewModel UserView { get; set; }

        public CarViewModel CarView { get; set; }

        public string ReserveNum { get; set; }
    }
}