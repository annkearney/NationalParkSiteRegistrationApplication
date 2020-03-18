using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("npcampground");

            ICampgroundDAO campgroundDAO = new CampgroundSqlDAO(connectionString);
            IParkDAO1 parkDAO = new ParkSqlDAO(connectionString);
            IReservationDAO reservationDAO = new ReservationSqlDAO(connectionString);
            ISiteDAO siteDAO = new SiteSqlDAO(connectionString);



            NationalParkCLI cli = new NationalParkCLI(campgroundDAO, parkDAO, reservationDAO, siteDAO);
            cli.RunCLI();
        }
    }
}
