using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Rental
    {
        private Car car;
        private int numDays;
        private int mileage;
        private double total;

        public Car Car { get; set; }
        public int NumDays { get; set; }
        public int Mileage { get; set; }
        public double Total { get; set; }

    }
}