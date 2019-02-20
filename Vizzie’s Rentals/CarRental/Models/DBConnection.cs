using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace CarRental.Models
{
    public class DBConnection
    {
        OleDbConnection aConnection = new OleDbConnection();

        //Adds New Reservation into Database
        public void InsertReservation(ReservationViewModel reservationView)
        {
            bool isReturned = false;
            aConnection.ConnectionString = @"Provider=SQLOLEDB;Data Source=NOVINSKII-HP;Initial Catalog=CarRental;Integrated Security=SSPI;";

            using (OleDbConnection aCon = new OleDbConnection(aConnection.ConnectionString))
            {
                using (OleDbCommand aCommand = new OleDbCommand("InsertReservation", aCon))
                {
                    aCon.Open();
                    aCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    aCommand.Parameters.AddWithValue("@fname", SqlDbType.NVarChar).Value = reservationView.UserView.FirstName;
                    aCommand.Parameters.AddWithValue("@lname", SqlDbType.NVarChar).Value = reservationView.UserView.LastName;
                    aCommand.Parameters.AddWithValue("@phone", SqlDbType.NVarChar).Value = reservationView.UserView.Phone;
                    aCommand.Parameters.AddWithValue("@email", SqlDbType.NVarChar).Value = reservationView.UserView.Email;
                    aCommand.Parameters.AddWithValue("@pickupdate", SqlDbType.DateTime).Value = reservationView.CarView.DateView.PickUpDate;
                    aCommand.Parameters.AddWithValue("@returndate", SqlDbType.DateTime).Value = reservationView.CarView.DateView.ReturnDate;
                    aCommand.Parameters.AddWithValue("@numDays", SqlDbType.Int).Value = reservationView.CarView.DateView.numDays;
                    aCommand.Parameters.AddWithValue("@returned", SqlDbType.Bit).Value = isReturned;
                    aCommand.Parameters.AddWithValue("@reservenum", SqlDbType.NVarChar).Value = reservationView.ReserveNum;
                    aCommand.Parameters.AddWithValue("@preTotal", SqlDbType.Decimal).Value = reservationView.CarView.Car.DaysTotal;
                    aCommand.Parameters.AddWithValue("@type", SqlDbType.NVarChar).Value = reservationView.CarView.Car.CarType;

                    aCommand.ExecuteNonQuery();
                    aConnection.Close();
                    return;
                }
            }
        }

        //Checks if Reservation exists
        public ReserveGrabber ReserveChecker(ReturnViewModel returnView)
        {
            ReserveGrabber grabber = new ReserveGrabber { Result = false, ID = 0};
            int found = 0;
            aConnection.ConnectionString = @"Provider=SQLOLEDB;Data Source=NOVINSKII-HP;Initial Catalog=CarRental;Integrated Security=SSPI;";

            using (OleDbConnection aCon = new OleDbConnection(aConnection.ConnectionString))
            {
                using (OleDbCommand aCommand = new OleDbCommand("CheckReservation", aCon))
                {
                    aCon.Open();
                    aCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    aCommand.Parameters.AddWithValue("@num", SqlDbType.NVarChar).Value = returnView.ReserveNum;
                    aCommand.Parameters.AddWithValue("@fname", SqlDbType.NVarChar).Value = returnView.FirstName;
                    aCommand.Parameters.AddWithValue("@lname", SqlDbType.NVarChar).Value = returnView.LastName;

                    OleDbDataReader aReader = aCommand.ExecuteReader();

                    while (aReader.Read())
                    {
                        found = (int)aReader["Result"];
                        grabber.ID = (int)aReader["ReservationID"];
                        grabber.Returned = (bool)aReader["isReturned"];
                    }

                    grabber.Result = found != 0;
                    aConnection.Close();

                    return grabber;
                }
            }
        }

        //Gets Reservation Info
        public Reservation GetReserveInfo(int ID)
        {
            aConnection.ConnectionString = @"Provider=SQLOLEDB;Data Source=NOVINSKII-HP;Initial Catalog=CarRental;Integrated Security=SSPI;";
            Reservation reservation = new Reservation();

            using (OleDbConnection aCon = new OleDbConnection(aConnection.ConnectionString))
            {
                using (OleDbCommand aCommand = new OleDbCommand("GetReservation", aCon))
                {
                    aCon.Open();
                    aCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    aCommand.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = ID;
                    
                    OleDbDataReader aReader = aCommand.ExecuteReader();

                    while (aReader.Read())
                    {
                        reservation.FirstName = (string)aReader["FirstName"];
                        reservation.LastName = (string)aReader["LastName"];
                        reservation.PickUpDate = (DateTime)aReader["PickUpDate"];
                        reservation.ReturnDate = (DateTime)aReader["ReturnDate"];
                        reservation.CarType = (string)aReader["CarType"];
                        reservation.PreTotal = Convert.ToDouble(aReader["PreTotal"]);
                        reservation.ReserveNum = (string)aReader["ReserveNum"];
                    }
                    
                    aConnection.Close();

                    return reservation;
                }
            }
        }

        //Gets Reservation Info
        public void InsertPayment(Reservation reservation, string receipt)
        {
            bool returned = true;
            aConnection.ConnectionString = @"Provider=SQLOLEDB;Data Source=NOVINSKII-HP;Initial Catalog=CarRental;Integrated Security=SSPI;";

            using (OleDbConnection aCon = new OleDbConnection(aConnection.ConnectionString))
            {
                using (OleDbCommand aCommand = new OleDbCommand("InsertPayment", aCon))
                {
                    aCon.Open();
                    aCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    aCommand.Parameters.AddWithValue("@miles", SqlDbType.Int).Value = reservation.Miles;
                    aCommand.Parameters.AddWithValue("@totalCost", SqlDbType.Decimal).Value = reservation.RentalTotal;
                    aCommand.Parameters.AddWithValue("@receipt", SqlDbType.NVarChar).Value = receipt;
                    aCommand.Parameters.AddWithValue("@returned", SqlDbType.Bit).Value = returned;
                    aCommand.Parameters.AddWithValue("@reservenum", SqlDbType.NVarChar).Value = reservation.ReserveNum;

                    aCommand.ExecuteNonQuery();
                    aConnection.Close();

                    return;
                }
            }
        }
    }
}