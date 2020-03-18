using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;

namespace Capstone.Tests.DAL
{

    public class POTestInitialize
    {
        //protected TransactionScope transaction;
        protected string ConnectionString
        {
            get
            {
                return "Server=.\\SQLEXPRESS;Database=npcampground;Trusted_Connection=True;";
            }
        }
        protected TransactionScope info;
        protected int numCampgrounds = 0;
        protected int numParks = 0;
        protected int numSites = 0;
        protected int numReservations = 0;
        protected const string newCampgroundName = "CampTest";
        protected const string newParkName = "ParkTest";
        protected const string newSiteName = "SiteTest";
        protected const string newReservationName = "ReservationTest";
        protected const string name = "Test";
        protected const string location = "TestLocation";
        protected  DateTime StartDate = DateTime.Parse("03-28-2020");
        protected DateTime establish_date = DateTime.Parse("04-29-2020");
        protected const int area = 0;
        protected const int visitors = 0;
        protected const string Description = "TestDescription";
        protected int park_ID = 2;
        protected int open_from_mm = 2;
        protected int open_to_mm = 11;
        protected const decimal daily_fee = 0;
        protected string site_id = "1000";
        protected const int Site_number = 0;
        protected int campground_id = 4;
        protected const int max_occupancy = 0;
        protected const bool accessible = false;
        protected const int max_rv_length = 0;
        protected const bool utilities = false;
        protected DateTime to_date = DateTime.Parse("02-05-2020");
        protected DateTime from_date = DateTime.Parse("03-05-2020");
        protected DateTime create_date = DateTime.Parse("03-05-2020");

        [TestInitialize]
        public void setup()
        {
            info = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT COUNT(*) FROM park WHERE name = '{name}' and location = '{location}'", connection);
                if(Convert.ToInt32(sqlCommand.ExecuteScalar()) == 0)
                {
                    sqlCommand = new SqlCommand($"insert into park(name, location, establish_date, area, visitors, description) values('{name}','{location}','{establish_date}','{area}',{visitors},'{Description}')", connection);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlCommand = new SqlCommand($"select count(*) from site where site_id = '{site_id}'", connection);
                if (Convert.ToInt32(sqlCommand.ExecuteScalar()) == 0)
                {
                    sqlCommand = new SqlCommand($"insert into site(campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) values('{campground_id}', '{Site_number}','{max_occupancy}','{accessible}','{max_rv_length}','{utilities}')", connection);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlCommand = new SqlCommand($"select count(*) from reservation where name = '{name}'", connection);
                if (Convert.ToInt32(sqlCommand.ExecuteScalar()) == 0)
                {
                    sqlCommand = new SqlCommand($"insert into reservation(site_id, name, from_date, to_date, create_date) values(1,'{name}','{from_date}','{to_date}','{create_date}')", connection);
                    sqlCommand.ExecuteNonQuery();
                }

                sqlCommand = new SqlCommand($"select count(*) from campground where name = '{name}'", connection);
                if (Convert.ToInt32(sqlCommand.ExecuteScalar()) == 0)
                {
                    sqlCommand = new SqlCommand($"insert into campground(park_id, name, open_from_mm, open_to_mm, daily_fee) values(1,'{name}','{open_from_mm}','{open_to_mm}','{daily_fee}')", connection);
                    sqlCommand.ExecuteNonQuery();
                }

            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            info.Dispose();
        }
    }
}
