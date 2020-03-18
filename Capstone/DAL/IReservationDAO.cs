using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {

        int AddReservation(int siteSelection, string reservationName, DateTime dateOfArrival, DateTime dateOfDeparture, DateTime createDate);
        


    }
}
