using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {
        private string connectionString;

        public ReservationSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int AddReservation(int siteSelection, string reservationName, DateTime dateOfArrival, DateTime dateOfDeparture, DateTime createDate)
        {
            int reservationConfirmationId = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    string commandText = "insert into reservation values(@siteID, @name, @from_date, @to_date, @create_date);select SCOPE_IDENTITY();";
                    cmd.CommandText = commandText;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@siteID", siteSelection);
                    cmd.Parameters.AddWithValue("@name", reservationName);
                    cmd.Parameters.AddWithValue("@from_date", dateOfArrival);
                    cmd.Parameters.AddWithValue("@to_date", dateOfDeparture);
                    cmd.Parameters.AddWithValue("@create_date", createDate);
                    reservationConfirmationId = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("An error occured while trying to add reservation.");

                throw;
            }
            return reservationConfirmationId;
        }

        public IList<Reservation> GetAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    SqlCommand cmd = new SqlCommand();

                    string command = "Select * from reservation";
                    cmd.CommandText = command;
                    cmd.Connection = connection;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservation reservation = ConvertReaderToReservation(reader);
                        reservations.Add(reservation);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred communicating with the database. ");
                Console.WriteLine(e.Message);
                throw;

            }
            return reservations;
        }

        private Reservation ConvertReaderToReservation(SqlDataReader reader)
        {

            Reservation reservation = new Reservation();
            reservation.reservation_id = Convert.ToInt32(reader["reservation_id"]);
            reservation.site_id = Convert.ToInt32(reader["site_id"]);
            reservation.name = Convert.ToString(reader["name"]);
            reservation.from_date = Convert.ToDateTime(reader["from_date"]);
            reservation.to_date = Convert.ToDateTime(reader["to_date"]);
            reservation.create_date = Convert.ToDateTime(reader["to_date"]);

            return reservation;
        }
    }
}
