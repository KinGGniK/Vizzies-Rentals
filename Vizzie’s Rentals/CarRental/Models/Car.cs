using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Car
    {
        private string cartype;
        private double price = 0.0;
        private double daysTotal = 0.0;

        public string CarType
        {
            get
            {
                return this.cartype;
            }
            set
            {
                this.cartype = value;
            }
        }
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }

        public double DaysTotal
        {
            get
            {
                return this.daysTotal;
            }
            set
            {
                this.daysTotal = value;
            }
        }

        public Car(string Type, double Price, int days)
        {
            this.cartype = Type;
            this.price = Price;
            this.daysTotal = days * Price;
        }

        public Car(string Type, double Price)
        {
            this.cartype = Type;
            this.price = Price;
        }

        public Car(string Type)
        {
            this.cartype = Type + " Vehicle";
            this.price = 0.0;
        }

        public Car()
        {

        }
    }
}