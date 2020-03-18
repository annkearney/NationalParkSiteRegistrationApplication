using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Globalization;

namespace Capstone
{
    public class NationalParkCLI
    {

        const string Command_Quit = "q";
        int selectedParkIndex = 0;
        int siteSelection = 0;
        DateTime dateOfArrival = DateTime.Now;
        DateTime dateOfDeparture = DateTime.Now;


        private ICampgroundDAO campgroundDAO;
        private IParkDAO1 parkDAO;
        private IReservationDAO reservationDAO;
        private ISiteDAO siteDAO;

        public NationalParkCLI(ICampgroundDAO campgroundDAO, IParkDAO1 parkDAO, IReservationDAO reservationDAO, ISiteDAO siteDAO)
        {
            this.campgroundDAO = campgroundDAO;
            this.parkDAO = parkDAO;
            this.reservationDAO = reservationDAO;
            this.siteDAO = siteDAO;
        }

        public void RunCLI()
        {
            PrintViewParksMenu();

            IList<Park> parks = parkDAO.GetAllParkInfo();

            while (true)
            {

                Console.WriteLine();
                Console.WriteLine("Select a park for campground information:");
                string command = Console.ReadLine();
                bool validEntry = Int32.TryParse(command, out selectedParkIndex);

                if (validEntry && command == "1" || command == "2" || command == "3")
                {
                    Console.Clear();
                    PrintParkInformationScreen(selectedParkIndex);
                }
                else
                {
                    switch (command.ToLower().Trim())
                    {
                        case Command_Quit:
                            Console.WriteLine("Thank you for using the National Park Reservation System!");
                            return;

                        default:
                            Console.WriteLine("The command provided was not a valid command, please try again.");
                            break;

                    }
                }
            }
        }

        public void PrintViewParksMenu()
        {
            IList<Park> parks = parkDAO.GetAllParkInfo();

            Console.WriteLine("WELCOME TO THE NATIONAL PARK RESERVATION SYSTEM");
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine();
            Console.WriteLine("Our Parks:");

            for (int i = 0; i < parks.Count; i++)
            {
                Console.WriteLine(i + 1 + ") " + parks[i].name);
            }
            Console.WriteLine("Q) quit");
        }

        public void PrintParkInformationScreen(int selectedParkIndex)
        {
            IList<Park> parks = parkDAO.GetAllParkInfo();

            Console.WriteLine("PARK INFORMATION");
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine(parks[selectedParkIndex - 1].name + " National Park");
            Console.WriteLine();
            Console.WriteLine("Location:".PadRight(20) + parks[selectedParkIndex - 1].location);
            Console.WriteLine("Established:".PadRight(20) + parks[selectedParkIndex - 1].establish_date.ToString("MM/dd/yyyy"));
            Console.WriteLine("Area:".PadRight(20) + parks[selectedParkIndex - 1].area.ToString("n0") + " sq km");
            Console.WriteLine("Annual Visitors: ".PadRight(20) + parks[0].visitors.ToString("n0"));
            Console.WriteLine();
            Console.WriteLine(parks[selectedParkIndex - 1].desctription);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Return to Previous Screen");
            Console.WriteLine();
            Console.WriteLine("Select a Command:");
            string command = Console.ReadLine();

            const string Command_GetCampgrounds = "1";
            const string Command_Return = "2";

            switch (command.ToLower().Trim())
            {
                case Command_GetCampgrounds:
                    Console.Clear();
                    GetCampground(selectedParkIndex);
                    break;

                case Command_Return:
                    Console.Clear();
                    PrintViewParksMenu();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.WriteLine();
                    PrintParkInformationScreen(selectedParkIndex);
                    break;

            }

        }

        public void GetCampground(int selectedParkIndex)
        {
            IList<Park> parks = parkDAO.GetAllParkInfo();
            IList<Campground> campgrounds = campgroundDAO.GetCampground(parks[selectedParkIndex - 1].park_ID);

            Console.WriteLine("PARK CAMPGROUNDS");
            Console.WriteLine("".PadRight(60, '='));
            Console.WriteLine(parks[selectedParkIndex - 1].name + " National Park");
            Console.WriteLine();
            Console.Write(" ".PadRight(10));
            Console.Write("Name".PadRight(40));
            Console.Write("Open".PadRight(15));
            Console.Write("Close".PadRight(15));
            Console.Write("Daily Fee".PadRight(15));
            Console.WriteLine("");


            for (int i = 0; i < campgrounds.Count; i++)
            {
                string number = "#" + (i + 1);
                string dateOpen = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campgrounds[i].open_from_mm);
                string dateClosed = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(campgrounds[i].open_to_mm);

                Console.WriteLine($"{number.PadRight(10)}{campgrounds[i].name.PadRight(40)}{dateOpen.PadRight(15)}{dateClosed.PadRight(15)}{campgrounds[i].daily_fee:C2}");
            }

            CampgroundMenuOptions();

        }

        public void CampgroundMenuOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            Console.WriteLine();
            Console.WriteLine("Select a Command:");
            string command = Console.ReadLine();

            const string Command_SearchReservations = "1";
            const string Command_Return = "2";

            switch (command)
            {
                case Command_SearchReservations:
                    SelectReservationDetails();
                    break;

                case Command_Return:
                    Console.Clear();
                    PrintParkInformationScreen(selectedParkIndex);
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid selection. Please try again.");
                    Console.WriteLine();
                    GetCampground(selectedParkIndex);
                    break;
            }
        }

        public void SelectReservationDetails()
        {
            IList<Park> parks = parkDAO.GetAllParkInfo();
            IList<Campground> campgrounds = campgroundDAO.GetCampground(parks[selectedParkIndex - 1].park_ID);
            
            Console.WriteLine();
            int selectedCampgroundIndex = CLIHelper.GetInteger("Which campground would you like to select (enter 0 to cancel)?");
            
            try
            {
                if ((selectedCampgroundIndex - 1) == -1)
                {
                    Console.Clear();
                    PrintParkInformationScreen(selectedParkIndex);
                }
                else if(selectedCampgroundIndex > campgrounds.Count){
                    Console.WriteLine("Invalid campground selection. Please try again.");
                    Console.WriteLine();
                    SelectReservationDetails();
                }
                else
                {
                    dateOfArrival = CLIHelper.GetDate("What is the arrival date? (Please enter MM/DD/YYYY) ");
                    dateOfDeparture = CLIHelper.GetDate("What is the departure date? (Please enter MM/DD/YYYY) ");

                    if(dateOfDeparture < dateOfArrival)
                    {
                        Console.WriteLine();
                        Console.WriteLine("The date of departure cannot be earlier than the date of arrival. Please try again.");
                        Console.WriteLine();
                        SelectReservationDetails();
                    }

                    IList<Site> siteReservationInfo = siteDAO.GetAllSiteInfo(campgrounds[selectedCampgroundIndex - 1].campground_id, dateOfArrival, dateOfDeparture);

                    if (siteReservationInfo.Count < 1)
                    {
                        Console.Clear();
                        Console.WriteLine("No sites are available for that date. Please select another option");
                        Console.WriteLine();
                        GetCampground(parks[selectedParkIndex - 1].park_ID);
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.Write("Site No.".PadRight(15));
                        Console.Write("Maximum Occupancy".PadRight(20));
                        Console.Write("Accessible?".PadRight(15));
                        Console.Write("Max RV Length".PadRight(22));
                        Console.Write("Utility".PadRight(20));
                        Console.Write("Cost");
                        Console.WriteLine();

                        double totalDays = (dateOfDeparture - dateOfArrival).TotalDays;
                        decimal totalPrice = Convert.ToDecimal(totalDays) * campgrounds[selectedCampgroundIndex - 1].daily_fee;

                        List<int> siteIDs = new List<int>(); 

                        for (int i = 0; i < siteReservationInfo.Count; i++)
                        {
                            siteIDs.Add(siteReservationInfo[i].site_id);
                            Console.WriteLine($"");
                            Console.WriteLine($"{siteReservationInfo[i].PrintID()}{siteReservationInfo[i].PrintOccupancy()}{siteReservationInfo[i].PrintAccessible()}{siteReservationInfo[i].PrintRVLength()}{siteReservationInfo[i].PrintUtility()} {totalPrice:C2}");
                        }

                        Console.WriteLine();
                        Console.WriteLine();

                        siteSelection = CLIHelper.GetInteger("Which site should be reserved (enter 0 to cancel)? ");
                        if (siteSelection == 0)
                        {
                            Console.Clear();
                            PrintViewParksMenu();
                        }
                        else if (siteIDs.Contains(siteSelection))
                        {
                            AddReservation();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid Site Selection. Please try again.");
                            Console.WriteLine();
                            SelectReservationDetails();
                            //GetCampground(parks[selectedParkIndex - 1].park_ID);
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("Invalid selection. Please try again.");
                Console.WriteLine();
                SelectReservationDetails();
            }

        }

        public void AddReservation()
        {
            string reservationName = CLIHelper.GetString("What name should the reservation be made under? ");

            DateTime createDate = DateTime.Now;

            int idConfirmation = reservationDAO.AddReservation(siteSelection, reservationName, dateOfArrival, dateOfDeparture, createDate);

            Console.WriteLine($"The reservation has been made and the confirmation ID is:{idConfirmation}");
            Console.WriteLine();
            int backToMenuSelection = CLIHelper.GetInteger("Thank you for using the National Park Reservation System! Please press 1 if you would like to return to the main menu.");

            if (backToMenuSelection == 1)
            {
                Console.Clear();
                PrintViewParksMenu();
            }
            else
            {
                Console.WriteLine("Enjoy your trip!");
            }

        }





    }
}
