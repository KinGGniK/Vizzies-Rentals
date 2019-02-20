using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class ReserveGrabber
    {
        public int ID { get; set; }

        public bool Result { get; set; }

        public bool Returned { get; set; }
    }
}