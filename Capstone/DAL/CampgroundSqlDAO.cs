using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO : ICampgroundDAO 
    {
        private string connectionString;

        public CampgroundSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Campground> GetCampground(int selectedParkIndex)
        {
            List<Campground> campgrounds = new List<Campground>();
            
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    SqlCommand cmd = new SqlCommand();

                    string command = "Select * from campground where park_id = @park_id";
                    cmd.CommandText = command;
                    cmd.Parameters.AddWithValue("@park_id", selectedParkCode);
                    cmd.Connection = connection;


                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground = ConvertReaderToCampground(reader);

                        campgrounds.Add(campground);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred communicating with the database. ");
                Console.WriteLine(e.Message);
                throw;

            }
            return campgrounds;
        }
        private Campground ConvertReaderToCampground(SqlDataReader reader)
        {
            Campground campground = new Campground();
            campground.campground_id = Convert.ToInt32(reader["campground_id"]);
            campground.park_id = Convert.ToInt32(reader["park_id"]);
            campground.name = Convert.ToString(reader["name"]);
            campground.open_from_mm = Convert.ToInt32(reader["open_from_mm"]);
            campground.open_to_mm = Convert.ToInt32(reader["open_to_mm"]);
            campground.daily_fee = Convert.ToDecimal(reader["daily_fee"]);
            return campground;


        }
    }
}
