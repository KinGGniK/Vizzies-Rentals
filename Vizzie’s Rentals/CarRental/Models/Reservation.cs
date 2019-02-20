using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class Reservation
    {
        private string firstname;
        private string lastname;
        private DateTime pickupdate;
        private DateTime returndate;
        private string carType;
        private int miles;
        private double preTotal;
        private string reserveNum;
        private double rentalTotal;

        public string FirstName
        {
            get
            {
                return this.firstname;
            }
            set
            {
                this.firstname = value;
            }
        }
        public string LastName
        {
            get
            {
                return this.lastname;
            }
            set
            {
                this.lastname = value;
            }
        }
        public DateTime PickUpDate
        {
            get
            {
                return this.pickupdate;
            }
            set
            {
                this.pickupdate = value;
            }
        }
        public DateTime ReturnDate
        {
            get
            {
                return this.returndate;
            }
            set
            {
                this.returndate = value;
            }
        }
        public string CarType
        {
            get
            {
                return this.carType;
            }
            set
            {
                this.carType = value;
            }
        }
        public int Miles
        {
            get
            {
                return this.miles;
            }
            set
            {
                this.miles = value;
            }
        }
        public double PreTotal
        {
            get
            {
                return this.preTotal;
            }
            set
            {
                this.preTotal = value;
            }
        }
        public string ReserveNum
        {
            get
            {
                return this.reserveNum;
            }
            set
            {
                this.reserveNum = value;
            }
        }
        public double RentalTotal
        {
            get
            {
                return this.rentalTotal;
            }
            set
            {
                this.rentalTotal = value;
            }
        }
    }
}