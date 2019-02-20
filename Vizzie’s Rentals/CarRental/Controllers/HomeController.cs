using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        //Returning a Rental Vehicle

        public ActionResult ReturnVehicleForm()
        {

            return View();
        }

        public ActionResult ReturnVehicle(ReturnViewModel returnView)
        {
            if (!ModelState.IsValid)
            {
                return View("ReturnVehicleForm", returnView);
            }

            DBConnection aConnection = new DBConnection();
            ReserveGrabber grabber = aConnection.ReserveChecker(returnView);

            if (grabber.Result)
            {
                if (!grabber.Returned)
                {
                    Reservation reservation = aConnection.GetReserveInfo(grabber.ID);
                    Session["Reservation"] = reservation;
                }
                else
                {
                    TempData["Returned"] = "Reservation has been paid and closed.";
                    return RedirectToAction("ReturnVehicleForm");
                }
            }
            else
            {
                TempData["NoReserve"] = "Reservation does not exist";
                return RedirectToAction("ReturnVehicleForm");
            }

            ViewBag.User = returnView;
            return View();
        }

        public ActionResult RentalCharge(DateMilesViewModel dateMiles)
        {
            if (!ModelState.IsValid)
            {
                return View("ReturnVehicle");
            }

            Reservation reservation = (Reservation)Session["Reservation"];

            reservation.Miles = dateMiles.Miles;

            reservation.RentalTotal = reservation.PreTotal + (dateMiles.Miles * .32);

            DateDisplayViewModel dateDisplay = new DateDisplayViewModel
            {
                PickUp = reservation.PickUpDate.ToShortDateString(),
                Return = reservation.ReturnDate.ToShortDateString()
            };

            Session["Reservation"] = reservation;
            Session["Dates"] = dateDisplay;
            ViewBag.Reservation = reservation;
            ViewBag.Dates = dateDisplay;

            return View();
        }

        public ActionResult ChargeConfirmed()
        {
            DBConnection aConnection = new DBConnection();
            Reservation reservation = (Reservation)Session["Reservation"];
            string receipt = GenerateReceipt();

            aConnection.InsertPayment(reservation, receipt);

            ViewBag.Receipt = receipt;

            return View();
        }
        //Reserving a Vehicle


        [HttpGet]
        public ActionResult CarRentalForm()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarRental(DateViewModel carRental)
        {
           
            if(!ModelState.IsValid)
            {
                return View("CarRentalForm", carRental);
            }

            //Gets the number of days the car will be rented by subtracting dates
            TimeSpan difference = carRental.ReturnDate.Subtract(carRental.PickUpDate);
            carRental.numDays = (int)difference.TotalDays;

            //Create List of Car Options
            List<Car> cars = new List<Car>
            {
                new Car
                {
                    CarType = "Compact",
                    Price = 19.95,
                    DaysTotal = (carRental.numDays * 19.95)
                },
                new Car
                {
                    CarType = "Standard",
                    Price = 24.95,
                    DaysTotal = (carRental.numDays * 24.95)
                },
                new Car
                {
                    CarType = "Luxury",
                    Price = 39.00,
                    DaysTotal = (carRental.numDays * 39.00)
                }
            };

            Session["CRVM"] = carRental;

            ViewBag.Cars = cars;
            Session["Cars"] = cars;
            return View();
        }
        public ActionResult EstimatePage(string choice)
        {
            DateViewModel carRental = (DateViewModel)Session["CRVM"];

            Car chosenCar = new Car();

            switch (choice)
            {
                case "Compact":
                    chosenCar = new Car("Compact", 19.95, carRental.numDays);
                    break;

                case "Standard":
                    chosenCar = new Car("Standard", 24.95, carRental.numDays);
                    break;

                case "Luxury":
                    chosenCar = new Car("Luxury", 39.00, carRental.numDays);
                    break;

                default:
                    TempData["BadCar"] = "A car selection is required.";
                    ViewBag.Cars = (List<Car>)Session["Cars"];
                    return View("CarRental");
                    
            }

            CarViewModel carView = new CarViewModel
            {
                DateView = carRental,
                Car = chosenCar
            };

            DateDisplayViewModel dateDisplay = new DateDisplayViewModel
            {
                PickUp = carRental.PickUpDate.ToShortDateString(),
                Return = carRental.ReturnDate.ToShortDateString()
            };

            Session["Dates"] = dateDisplay;
            Session["CVM"] = carView;
            ViewBag.CarView = carView;
            ViewBag.Dates = dateDisplay;

            return View();
        }

        public ActionResult ReserveCar()
        {
            CarViewModel carView = (CarViewModel)Session["CVM"];

            ViewBag.CarView = carView;
            ViewBag.Dates = (DateDisplayViewModel)Session["Dates"];

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReserveConfirm(UserViewModel reserveView)
        {
            CarViewModel carView = (CarViewModel)Session["CVM"];

            if (!ModelState.IsValid)
            { 
                ViewBag.CarView = carView;
                ViewBag.Dates = (DateDisplayViewModel)Session["Dates"];

                return View("ReserveCar", reserveView);
            }

            DBConnection aConnection = new DBConnection();

            Random random = new Random(); 
            int confirmnum = random.Next(11111, 99999);

            ReservationViewModel reservationView = new ReservationViewModel
            {
                UserView = reserveView,
                CarView = carView,
                ReserveNum = confirmnum.ToString()
            };

            aConnection.InsertReservation(reservationView);
            ViewBag.ReserveNum = confirmnum;

            return View();
        }

        //Misc Methods

        private string GenerateReceipt()
        {
            string receipt;
            Random random = new Random();
            int recnum = random.Next(11111, 99999);

            receipt = "VRS#" + recnum.ToString();

            return receipt;
        }
    }
}