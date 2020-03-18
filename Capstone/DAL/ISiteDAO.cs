using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        IList<Site> GetAllSiteInfo(int selectedCampgroundIndex, DateTime dateOfArrival, DateTime dateOfDeparture);
    }
}
