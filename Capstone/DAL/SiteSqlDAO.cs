using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private string connectionString;

        public SiteSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Site> GetAllSiteInfo(int selectedCampgroundIndex, DateTime dateOfArrival, DateTime dateOfDeparture)
        {
            List<Site> siteInfo = new List<Site>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select TOP 5 * from site join campground on campground.campground_id = site.campground_id where site.campground_id = @campground_id and site.site_id not in (select site_id from reservation where @dateOfArrival between from_date and to_date or @dateOfDeparture between from_date and to_date); ", conn);
                    cmd.Parameters.AddWithValue("@campground_id", selectedCampgroundIndex);
                    cmd.Parameters.AddWithValue("@dateOfArrival", dateOfArrival);
                    cmd.Parameters.AddWithValue("@dateOfDeparture", dateOfDeparture);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = ConvertReaderToSite(reader);
                        siteInfo.Add(site);
                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("No campgrounds are available for that date. Please try again.");

                throw;
            }
            return siteInfo;

        }



        private Site ConvertReaderToSite(SqlDataReader reader)
        {
            Site site = new Site();
            site.site_id = Convert.ToInt32(reader["site_id"]);
            site.campground_id = Convert.ToInt32(reader["campground_id"]);
            site.site_number = Convert.ToInt32(reader["site_number"]);
            site.max_occupancy = Convert.ToInt32(reader["max_occupancy"]);
            site.accessible = Convert.ToBoolean(reader["accessible"]);
            site.max_rv_length = Convert.ToInt32(reader["max_rv_length"]);
            site.utilites = Convert.ToBoolean(reader["utilities"]);

            return site;
        }
    }
    
}
