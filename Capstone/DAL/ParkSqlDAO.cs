using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO1
    {
        private string connectionString;
        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetAllParkInfo()
        {
            List<Park> infoOfParksList = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select park_id, name, location, establish_date, area, visitors, description from park;", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = ConvertReaderToPark(reader);
                        infoOfParksList.Add(park);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("An error occured reading parks by ID.");

                throw;
            }
            return infoOfParksList;

        }

       
        private Park ConvertReaderToPark(SqlDataReader reader)
        {
            Park park = new Park();
            park.park_ID = Convert.ToInt32(reader["park_id"]);
            park.name = Convert.ToString(reader["name"]);
            park.location = Convert.ToString(reader["location"]);
            park.establish_date = Convert.ToDateTime(reader["establish_date"]);
            park.area = Convert.ToInt32(reader["area"]);
            park.visitors = Convert.ToInt32(reader["visitors"]);
            park.desctription = Convert.ToString(reader["description"]);

            return park;
        }


    }
}
